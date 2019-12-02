using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Code converted to C# from https://github.com/idmillington/aicore
 */
public class KinematicSeek : TargetedKinematicMovement
{

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Location>();
        character.position = transform.position;
        character.orientation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override SteeringOutput getSteering(SteeringOutput input)
    {
        SteeringOutput output = input;
        output.linear = target;
        output.linear -= character.position;

        if (output.linear.sqrMagnitude > 0)
        {
            output.linear.Normalize();
            output.linear *= maxSpeed;
        }

        return output;
    }
}
