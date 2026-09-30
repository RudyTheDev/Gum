using System;
using System.Linq;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Localization;
using Gum.Managers;
using Gum.ProjectServices.CodeGeneration;
using Moq;
using Shouldly;

namespace Gum.ProjectServices.Tests;

/// <summary>
/// File-scoped namespaces need C# 10, so projects compiling with an older language version
/// (e.g. Unity's C# 9) get a block namespace instead.
/// </summary>
public class CodeGeneratorNamespaceStyleTests : BaseTestClass
{
    private static CodeGenerator CreateCodeGenerator(int? languageVersion)
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
            .Setup(d => d.DetectCSharpLanguageVersion(It.IsAny<CodeOutputProjectSettings>(), It.IsAny<string?>()))
            .Returns(languageVersion);

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
        AppendFolderToNamespace = true,
    };

    private string GenerateHost(int? languageVersion)
    {
        ComponentSave host = new ComponentSave { Name = "Host", BaseType = "Container" };
        host.States.Add(new StateSave { Name = "Default", ParentContainer = host });
        host.Instances.Add(new InstanceSave { Name = "Label", BaseType = "Text", ParentContainer = host });
        Project.Components.Add(host);
        ObjectFinder.Self.GumProjectSave = Project;

        return CreateCodeGenerator(languageVersion).GetGeneratedCodeForElement(host, new CodeOutputElementSettings(), Settings);
    }

    private static void ShouldHaveBalancedBraces(string code) =>
        code.Count(c => c == '{').ShouldBe(code.Count(c => c == '}'));

    [Theory]
    [InlineData(null)]
    [InlineData(10)]
    [InlineData(14)]
    public void GetGeneratedCodeForElement_CSharp10OrUnknown_UsesFileScopedNamespace(int? languageVersion)
    {
        string code = GenerateHost(languageVersion);

        code.ShouldContain("namespace MyGame.Components;" + Environment.NewLine);
        ShouldHaveBalancedBraces(code);
    }

    [Theory]
    [InlineData(7)]
    [InlineData(9)]
    public void GetGeneratedCodeForElement_BelowCSharp10_UsesBlockNamespace(int languageVersion)
    {
        string code = GenerateHost(languageVersion);

        code.ShouldNotContain("namespace MyGame.Components;");
        code.ShouldContain("namespace MyGame.Components" + Environment.NewLine + "{" + Environment.NewLine + "    partial class HostRuntime");
        code.TrimEnd().ShouldEndWith("    }" + Environment.NewLine + "}");
        ShouldHaveBalancedBraces(code);
    }

    [Fact]
    public void GenerateStandardElementsFallbackCode_BelowCSharp10_UsesBlockNamespace()
    {
        string? code = CreateCodeGenerator(languageVersion: 9).GenerateStandardElementsFallbackCode(Project, Settings);

        code.ShouldNotBeNull();
        code.ShouldNotContain("namespace MyGame;");
        code.ShouldContain("namespace MyGame" + Environment.NewLine + "{" + Environment.NewLine + "    internal static class StandardElementsCodeGenRegistration");
        code.TrimEnd().ShouldEndWith("    }" + Environment.NewLine + "}");
    }

    [Fact]
    public void GenerateStandardElementsFallbackCode_CSharp10_UsesFileScopedNamespace()
    {
        string? code = CreateCodeGenerator(languageVersion: 10).GenerateStandardElementsFallbackCode(Project, Settings);

        code.ShouldNotBeNull();
        code.ShouldContain("namespace MyGame;" + Environment.NewLine);
    }
}
