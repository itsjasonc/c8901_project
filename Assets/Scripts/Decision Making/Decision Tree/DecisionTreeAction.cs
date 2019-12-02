using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Code converted to C# from https://github.com/idmillington/aicore
 */
public class DecisionTreeAction : DecisionTreeNode
{

    public override DecisionTreeNode makeDecision()
    {
        return this;
    }

    public virtual void doSomething()
    {

    }
}
