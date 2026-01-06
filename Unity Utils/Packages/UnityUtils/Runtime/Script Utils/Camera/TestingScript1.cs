using UnityEngine;
using UnityEngine.UI;
using UnityUtils.ScriptUtils;
using UnityUtils.ScriptUtils.Objects;

public class TestingScript1 : MonoBehaviour
{
    public float testingValue;
    public Vector3 testingVector3;
    // Starter is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ObjectAnimations.AnimateImageOpacity(GetComponent<Image>(), 0.5f, 1, 2);

        // Animates the testingVector3 variable's value from (0, 0, 0) to (0, 1, 4) in 2 seconds. 
        ObjectAnimations.AnimateValue<Vector3>(new Vector3(0, 0, 0), new Vector3(2, 2, 2), 2, (a, b, t) => Vector3.Lerp(a, b, t), value => testingVector3 = value);

        // Animates the testingValue variable's value from 1 to 3 in 2 seconds.
        ObjectAnimations.AnimateValue<float>(1, 3, 2, (a, b, t) => Mathf.Lerp(a, b, t), value => testingValue = value);
    }
}
