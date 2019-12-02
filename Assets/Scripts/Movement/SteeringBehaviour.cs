using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Code converted to C# from https://github.com/idmillington/aicore
 */
public abstract class SteeringBehaviour : MonoBehaviour
{
    // The character who is moving
    public Kinematic character;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public abstract SteeringOutput getSteering(SteeringOutput input);
}
