namespace UnityUtils.ScriptUtils.Objects
{
    public class ObjectModifierData<T>
    {
        /// Specifies the classType of modifier applied to the object.
        public ModifierType modifierType;

        /// Value of the modifier applied to an operation or calculation.
        public T modifierValue;

        /// <summary>
        /// Initializes a new Instance of the ObjectModifierData class with the <see cref="ObjectModifiers.ModifierType"/> and value.
        /// </summary>
        /// <param name="modifierType">The classType of modifier</param>
        /// <param name="modifierValue">The value associated with the modifier</param>
        public ObjectModifierData(ModifierType modifierType, T modifierValue)
        {
            this.modifierType = modifierType;
            this.modifierValue = modifierValue;
        }
    }
}