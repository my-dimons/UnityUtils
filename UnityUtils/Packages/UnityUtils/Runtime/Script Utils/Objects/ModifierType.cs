namespace UnityUtils.ScriptUtils.Objects {
  /// <summary>
  /// Enum with different modifier types that get used in <see cref="ObjectModifiers{T}"/> when applying modifiers to an input value.
  /// </summary>
  public enum ModifierType {
    /// Addition/Subtraction modifier
    Flat,
    /// Multiplication modifier
    Multiply,
    /// Division modifier, divides the input value by the modifier value. If value is 0, it will be ignored to avoid dividing by zero errors.
    Divide,
    /// Root Modifier, gets the root of the input value based on the modifier value. 
    Root,
    /// Exponent Modifier, raises the input value to the power of the modifier value.
    Exponent
  }
}