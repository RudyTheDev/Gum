#if NETSTANDARD2_1
// netstandard2.1 polyfill for the .NET 5+ attribute. Metadata for the trim analyzer only: it has no
// runtime behavior. Internal, and visible to the runtimes through InternalsVisibleTo.
namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Class, Inherited = false)]
internal sealed class RequiresUnreferencedCodeAttribute : Attribute
{
    public RequiresUnreferencedCodeAttribute(string message) => Message = message;

    public string Message { get; }

    public string? Url { get; set; }
}
#endif
