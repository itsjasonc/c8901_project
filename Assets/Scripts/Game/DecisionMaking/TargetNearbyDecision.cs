using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNearbyDecision : Decision
{
    public GameObject character;
    public GameObject target;
    public float maxDistance;
    public float DEFAULT_MAX_DISTANCE = 10.0f;

    public TargetNearbyDecision()
    {
        maxDistance = DEFAULT_MAX_DISTANCE;
    }

    public TargetNearbyDecision(float _maxDistance)
    {
        maxDistance = _maxDistance;
    }

    public override bool getBranch()
    {
        Vector3 direction = target.transform.position - character.transform.position;

        if (direction.magnitude < maxDistance)
        {
            return true;
        }

        return false;
    }
}
