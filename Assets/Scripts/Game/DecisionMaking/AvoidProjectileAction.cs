using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidProjectileAction : TargetedDecisionTreeAction
{
    public KinematicSeek kinematicComponent;
    public Vector3 targetPosition;

    public override void doSomething()
    {
        float duration = Time.deltaTime;
        SteeringOutput steer = new SteeringOutput();

        steer = kinematicComponent.getSteering(steer);
        kinematicComponent.target = targetPosition;

        kinematicComponent.character.integrate(steer, duration);
        kinematicComponent.character.setOrientationFromVelocity(steer.linear);

        character.transform.position = kinematicComponent.character.position;
        float angle = kinematicComponent.character.orientation * Mathf.Rad2Deg;

        character.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
