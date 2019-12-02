using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Code converted to C# from https://github.com/idmillington/aicore
 */
public class RandomDecisionWithTimeout : RandomDecision
{
    public float firstDecisionFrame;
    public float timeOutDuration;

    RandomDecisionWithTimeout() : base() { }

    public override bool getBranch()
    {
        float thisFrame = Time.deltaTime;

        if (thisFrame > lastDecisionFrame + Time.fixedDeltaTime ||
            thisFrame > firstDecisionFrame + timeOutDuration)
        {
            lastDecision = (Random.value > 0.5f);

            firstDecisionFrame = thisFrame;
        }

        lastDecisionFrame = thisFrame;

        return lastDecision;
    }
}
