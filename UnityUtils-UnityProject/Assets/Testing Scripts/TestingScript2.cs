using UnityEngine;
using UnityEngine.UI;
using UnityUtils.ScriptUtils;
using UnityUtils.ScriptUtils.Objects;
using UnityEngine.SceneManagement;
using UnityUtils.ScriptUtils.Cameras;
using JetBrains.Annotations;
using UnityEngine.InputSystem;

public class TestingScript2 : MonoBehaviour
{
    private void Start()
    {
        ObjectModifiers<float> modifierTest = new ObjectModifiers<float>();
        modifierTest.AddModifier(new ObjectModifierData<float>(ModifierType.Flat, 1));
        modifierTest.AddModifier(new ObjectModifierData<float>(ModifierType.Multiply, 5));
        modifierTest.AddModifier(new ObjectModifierData<float>(ModifierType.Flat, 14));
        modifierTest.AddModifier(new ObjectModifierData<float>(ModifierType.Divide, 3));
        modifierTest.AddModifier(new ObjectModifierData<float>(ModifierType.Flat, 19));
        modifierTest.PrintModifierOrder();
        modifierTest.PrintModifiers();

        modifierTest.SortModifiers();

        modifierTest.PrintModifiers();

        Debug.Log("value of 1: " + modifierTest.CalculateModifiers(1));
        Debug.Log("value of 5: " + modifierTest.CalculateModifiers(5));

        modifierTest.AddTemporaryModifier(new ObjectModifierData<float>(ModifierType.Flat, 3), 1);

        ObjectDelays.Delay(() => CameraShake.Screenshake(intensity: 5), 3);
        ObjectDelays.Delay(() => CameraShake.Screenshake(intensity: 5), 3.2f);
        ObjectDelays.Delay(() => CameraShake.Screenshake(intensity: 5), 4);
        ObjectDelays.Delay(() => GetComponent<ObjectColorFlash>().Flash(Color.blue, 2), 3);
    }

    private void Update()
    {
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            CameraShake.Screenshake();
        }
    }
}
