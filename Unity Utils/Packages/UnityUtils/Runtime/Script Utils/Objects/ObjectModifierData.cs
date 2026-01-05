namespace UnityUtils.ScriptUtils.Objects
{
    public class ObjectModifierData
    {
        /// Specifies the type of modifier applied to the object.
        public ObjectModifiers.ModifierType modifierType;

        /// Value of the modifier applied to an operation or calculation.
        public float modifierValue;

        /// <summary>
        /// Initializes a new Instance of the ObjectModifierData class with the <see cref="ObjectModifiers.ModifierType"/> and value.
        /// </summary>
        /// <param name="modifierType">The type of modifier</param>
        /// <param name="modifierValue">The value associated with the modifier</param>
        public ObjectModifierData(ObjectModifiers.ModifierType modifierType, float modifierValue)
        {
            this.modifierType = modifierType;
            this.modifierValue = modifierValue;
        }
    }
}