using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallerPanel : MonoBehaviour
{
    [SerializeField] WaiterCaller waiterCaller;
    [SerializeField] Gradient progressionGradient;
    [SerializeField] Image callImage;
    [SerializeField] Image counterImage;


    void Update()
    {
        if (waiterCaller.Calling)
        {
            counterImage.enabled = true;
            counterImage.color = progressionGradient.Evaluate(GetCounterProgression());

            callImage.enabled = true;
        }
        else
        {
            counterImage.enabled = false;
            callImage.enabled = false;
        }
    }

    private float GetCounterProgression()
    {
        float t = 0;

        t = Mathf.InverseLerp(0, waiterCaller.Counter.TimeToComplete, waiterCaller.Counter.TimeLeft);

        return t;
    }
}
