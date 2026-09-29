#if NETSTANDARD2_1
// netstandard2.1 polyfill for the .NET 5+ type the compiler requires for init accessors and records.
// Compile-time only: it has no members and no runtime behavior.
namespace System.Runtime.CompilerServices;

internal static class IsExternalInit
{
}
#endif
