using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Code converted to C# from https://github.com/idmillington/aicore
 */
public class SteeringOutput
{
    // The linear component of the steering action
    public Vector3 linear;

    // The angular component of the steering action
    public float angular;

    public SteeringOutput()
    {
        angular = 0.0f;
    }

    public SteeringOutput(Vector3 _linear, float _angular = 0.0f)
    {
        linear = _linear;
        angular = _angular;
    }

    public virtual void clear()
    {
        linear = Vector3.zero;
        angular = 0;
    }

    public bool Equals(SteeringOutput so)
    {
        if (ReferenceEquals(null, so))
        {
            return false;
        }
        if (ReferenceEquals(this, so))
        {
            return true;
        }

        return linear.Equals(so.linear)
            && angular.Equals(so.angular);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((SteeringOutput)obj);
    }
    public float squareMagnitude()
    {
        return linear.sqrMagnitude + angular * angular;
    }

    public float magnitude()
    {
        return Mathf.Sqrt(squareMagnitude());
    }
}
