using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.ScriptUtils.Objects {
  public class ObjectModifiers<T> {
    /// The default modifier order
    public static readonly ModifierType[] DEFAULT_MODIFIER_ORDER = new ModifierType[] { ModifierType.Flat, ModifierType.Multiply, ModifierType.Divide };

    /// The applied modifier order
    public ModifierType[] ModifierOrder = DEFAULT_MODIFIER_ORDER;

    /// Modifiers to be applied when calculating modifiers, must be modified via <see cref="AddModifier(ObjectModifierData)"/>, or temporarily modified via <see cref="AddTemporaryModifier(ObjectModifierData, float, bool)"/>
    public List<ObjectModifierData<T>> Modifiers = new();

    /// <summary>
    /// Adds the <see cref="ObjectModifierData"/> modifier to <see cref="Modifiers"/>
    /// </summary>
    /// <param name="modifier">The modifier to add to the object</param>
    public void AddModifier(ObjectModifierData<T> modifier) { Modifiers.Add(modifier); }

    /// <summary>
    /// Creates a new <see cref="ObjectModifierData"/> with the specified <see cref="ModifierType"/> and value and adds it to <see cref="Modifiers"/>
    /// </summary>
    /// <param name="modifierType">The classType of modifier to add</param>
    /// <param name="modifierValue">The value associated with the modifier</param>
    public void AddModifier(ModifierType modifierType, T modifierValue) { Modifiers.Add(new ObjectModifierData<T>(modifierType, modifierValue)); }

    /// <summary>
    /// Temporarily adds the <see cref="ObjectModifierData"/> modifier to <see cref="Modifiers"/>. Once the time is over, it will remove the <see cref="ObjectModifierData"/> from <see cref="Modifiers"/>
    /// </summary>
    /// <param name="modifier">The modifier to apply to the object.</param>
    /// <param name="time">(In seconds) how long the <see cref="ObjectModifierData"/> stays in <see cref="Modifiers"/> for</param>
    /// <param name="useRealtime">Indicates whether the duration should be measured using real time instead of game time</param>
    public void AddTemporaryModifier(ObjectModifierData<T> modifier, float time, bool useRealtime = false) {
      CoroutineHelper.Starter.StartCoroutine(AddTemporaryModifierCoroutine(modifier, time, useRealtime));
    }

    private IEnumerator AddTemporaryModifierCoroutine(ObjectModifierData<T> modifier, float time, bool useRealtime) {
      AddModifier(modifier);
      yield return useRealtime ? new WaitForSecondsRealtime(time) : new WaitForSeconds(time);
      RemoveModifier(modifier);
    }

    /// <summary>
    /// Removes the specified modifier from <see cref="Modifiers"/>
    /// </summary>
    /// <param name="modifierType">The modifier to remove from the object</param>
    public void RemoveModifier(ObjectModifierData<T> modifierType) { Modifiers.Remove(modifierType); }

    /// <summary>
    /// Calculates the result of applying all <see cref="Modifiers"/> to the input value.
    /// </summary>
    /// <param name="inputValue">The initial value to which modifiers will be applied.</param>
    /// <param name="sortModifiers">true to sort the modifiers before applying them based on <see cref="ModifierOrder"/>; otherwise, false</param>
    /// <returns>Value calculated based on the <see cref="Modifiers"/></returns>
    public T CalculateModifiers(T inputValue, bool sortModifiers = true) {
      if (sortModifiers)
        SortModifiers();

      // convert values to double (get changed back later)
      double finalValue = Convert.ToDouble(inputValue);

      foreach (ObjectModifierData<T> modifier in Modifiers) {
        double modifierValue = Convert.ToDouble(modifier.modifierValue);


        switch (modifier.modifierType) {
          case ModifierType.Flat:
            finalValue += modifierValue;
            break;
          case ModifierType.Multiply:
            finalValue *= modifierValue;
            break;
          case ModifierType.Divide:
            if (modifierValue != 0)
              finalValue /= modifierValue;
            else
              Debug.LogWarning("Attempted to divide by zero in ObjectModifiers.CalculateModifiers. Modifier skipped.");
            break;
          case ModifierType.Root:
            finalValue = Math.Pow(finalValue, 1 / modifierValue);
            break;
          case ModifierType.Exponent:
            finalValue = Math.Pow(finalValue, modifierValue);
            break;
        }
      }

      return (T)Convert.ChangeType(finalValue, typeof(T));
    }

    /// <summary>
    /// Sorts <see cref="Modifiers"/> according to the specified order of <paramref name="sort"/>.
    /// </summary>
    /// <param name="sort"><see cref="ModifierType"/> array that defines the desired sort order. Modifiers are ordered to match the sequence
    /// of types in this array.</param>
    public void SortModifiers(ModifierType[] sort) {
      List<ObjectModifierData<T>> SortedModifiers = Modifiers;

      SortedModifiers.Sort((a, b) => {
        int indexA = System.Array.IndexOf(sort, a.modifierType);
        int indexB = System.Array.IndexOf(sort, b.modifierType);

        return indexA.CompareTo(indexB);
      });

      Modifiers = SortedModifiers;
    }

    /// <summary>
    /// Sorts <see cref="Modifiers"/> using <see cref="ModifierOrder"/>.
    /// </summary>
    public void SortModifiers() {
      SortModifiers(ModifierOrder);
    }

    /// <summary>
    /// Prints the classType and value of each <see cref="Modifiers"/> to the debug log.
    /// </summary>
    public void PrintModifiers() {
      string printOutput = "";

      foreach (ObjectModifierData<T> modifier in Modifiers) {
        string modifierType = "Modifier Type: " + modifier.modifierType.ToString();
        string modifierValue = "Modifier Value: " + modifier.modifierValue.ToString();

        printOutput += modifierType + ", " + modifierValue + "\n";

      }

      Debug.Log(printOutput);
    }

    /// <summary>
    /// Outputs the current order of <see cref="ModifierOrder"/> to the debug log.
    /// </summary>
    public void PrintModifierOrder() {
      string printOutput = "Modifier Order: ";

      foreach (ModifierType modifier in ModifierOrder) {
        printOutput += modifier + " ";
      }

      Debug.Log(printOutput);
    }
  }
}