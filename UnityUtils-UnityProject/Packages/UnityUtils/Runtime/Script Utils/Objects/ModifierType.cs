using System.Collections;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Objects
{
    /// <summary>
    /// Enum with different modifier types that get used in <see cref="ObjectModifiers{T}"/> when applying modifiers to an input value.
    /// </summary>
    public enum ModifierType
    {
        /// <summary> Addition/Subtraction modifier </summary>
        Flat,
        /// <summary> Multiplication modifier </summary>
        Multiply,
        /// <summary> Division modifier </summary>
        Divide
    }
}