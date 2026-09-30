using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Localization;
using Gum.Managers;
using Gum.ProjectServices.CodeGeneration;
using Moq;
using Shouldly;

namespace Gum.ProjectServices.Tests;

/// <summary>
/// Unity doesn't run module initializers, so startup registration in a Unity project uses
/// <c>RuntimeInitializeOnLoadMethod</c> instead of <c>ModuleInitializer</c>.
/// </summary>
public class CodeGeneratorUnityStartupTests : BaseTestClass
{
    private const string UnityAttribute =
        "[UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]";

    private static CodeGenerator CreateCodeGenerator(bool isUnityProject)
    {
        Mock<INameVerifier> mockNameVerifier = new Mock<INameVerifier>();
        string? whyNotValid;
        CommonValidationError error;
        mockNameVerifier
            .Setup(v => v.IsValidCSharpName(It.IsAny<string>(), out whyNotValid, out error))
            .Returns(true);
        FixedProjectDirectoryProvider directoryProvider = new FixedProjectDirectoryProvider(projectDirectory: null);

        Mock<ISyntaxVersionDetectionService> detection = new Mock<ISyntaxVersionDetectionService>();
        detection
            .Setup(d => d.Detect(It.IsAny<CodeOutputProjectSettings>(), It.IsAny<string?>()))
            .Returns(new SyntaxVersionResult { Version = 4 });
        detection
            .Setup(d => d.DetectIsUnityProject(It.IsAny<CodeOutputProjectSettings>(), It.IsAny<string?>()))
            .Returns(isUnityProject);

        return new CodeGenerator(
            new CodeGenerationNameVerifier(mockNameVerifier.Object),
            new LocalizationService(),
            new CodeOutputElementSettingsManager(directoryProvider),
            directoryProvider,
            syntaxVersionDetectionService: detection.Object);
    }

    private static CodeOutputProjectSettings Settings => new CodeOutputProjectSettings
    {
        OutputLibrary = OutputLibrary.MonoGame,
        RootNamespace = "MyGame",
    };

    private string GenerateHost(bool isUnityProject)
    {
        ComponentSave host = new ComponentSave { Name = "Host", BaseType = "Container" };
        host.States.Add(new StateSave { Name = "Default", ParentContainer = host });
        Project.Components.Add(host);
        ObjectFinder.Self.GumProjectSave = Project;

        return CreateCodeGenerator(isUnityProject).GetGeneratedCodeForElement(host, new CodeOutputElementSettings(), Settings);
    }

    [Fact]
    public void GetGeneratedCodeForElement_UnityProject_RegistersWithRuntimeInitializeOnLoadMethod()
    {
        string code = GenerateHost(isUnityProject: true);

        code.ShouldContain(UnityAttribute);
        code.ShouldNotContain("ModuleInitializer");
    }

    [Fact]
    public void GetGeneratedCodeForElement_NonUnityProject_RegistersWithModuleInitializer()
    {
        string code = GenerateHost(isUnityProject: false);

        code.ShouldContain("[System.Runtime.CompilerServices.ModuleInitializer]");
        code.ShouldNotContain("UnityEngine");
    }

    [Fact]
    public void GenerateStandardElementsFallbackCode_UnityProject_RegistersWithRuntimeInitializeOnLoadMethod()
    {
        string? code = CreateCodeGenerator(isUnityProject: true).GenerateStandardElementsFallbackCode(Project, Settings);

        code.ShouldNotBeNull();
        code.ShouldContain(UnityAttribute);
        code.ShouldNotContain("[ModuleInitializer]");
    }
}
