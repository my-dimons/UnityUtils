namespace UnityUtils.ScriptUtils.Objects
{
    public class ObjectModifierData
    {
        public ObjectModifiers.ModifierType modifierType;
        public float modifierValue;

        public ObjectModifierData(ObjectModifiers.ModifierType modifierType, float modifierValue)
        {
            this.modifierType = modifierType;
            this.modifierValue = modifierValue;
        }
    }
}