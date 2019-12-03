using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivePlayerAction : TargetedDecisionTreeAction
{
    public KinematicArrive kinematicComponent;

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
