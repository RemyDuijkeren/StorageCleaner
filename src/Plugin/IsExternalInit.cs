// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

// Records rely on IsExternalInit (for init-only setters and record machinery).
// By using a regular class with settable properties,
// the compiler no longer needs IsExternalInit, eliminating CS0518.
public static class IsExternalInit {}
