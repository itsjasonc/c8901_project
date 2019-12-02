using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Code converted to C# from https://github.com/idmillington/aicore
 */
public class RandomDecision : Decision
{
    protected bool lastDecision;

    protected float lastDecisionFrame;

    protected RandomDecision()
    {
        lastDecisionFrame = 0;
        lastDecision = false;
    }

    public override bool getBranch()
    {
        float thisFrame = Time.deltaTime;

        if (thisFrame > lastDecisionFrame + Time.fixedDeltaTime)
        {
            lastDecision = (Random.value > 0.5f);
        }

        lastDecisionFrame = thisFrame;

        return lastDecision;
    }
}
