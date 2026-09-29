#if NETSTANDARD2_1
// netstandard2.1 polyfill for the .NET 5+ attribute. Compile-time only: it feeds nullable analysis
// and has no runtime behavior. Internal, and visible to the runtimes through InternalsVisibleTo.
namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
internal sealed class MemberNotNullAttribute : Attribute
{
    public MemberNotNullAttribute(string member) => Members = new[] { member };

    public MemberNotNullAttribute(params string[] members) => Members = members;

    public string[] Members { get; }
}
#endif
