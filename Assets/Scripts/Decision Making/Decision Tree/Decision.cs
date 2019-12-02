using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Code converted to C# from https://github.com/idmillington/aicore
 */
public abstract class Decision : DecisionTreeNode
{
    public DecisionTreeNode trueBranch;
    public DecisionTreeNode falseBranch;

    public abstract bool getBranch();

    public override DecisionTreeNode makeDecision()
    {
        if (getBranch())
        {
            if (trueBranch == null)
                return null;
            else
                return trueBranch.makeDecision();
        }
        else
        {
            if (falseBranch == null)
                return null;
            else
                return falseBranch.makeDecision();
        }
    }
}
