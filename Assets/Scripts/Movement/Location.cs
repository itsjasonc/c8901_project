using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Code converted to C# from https://github.com/idmillington/aicore
 */
public class Location : MonoBehaviour
{
    public Vector3 position;
    public float orientation;

    public void Start()
    {
        position = transform.position;
    }

    public Location()
    {
        orientation = 0;
    }

    public Location(Vector3 _position)
    {
        position = _position;
        orientation = 0;
    }

    public Location(Vector3 _position, float _orientation)
    {
        position = _position;
        orientation = _orientation;
    }

    public Location(float x, float y, float z, float _orientation)
    {
        position = new Vector3(x, y, z);
        orientation = _orientation;
    }

    public Location(Location loc)
    {
        position = loc.position;
        orientation = loc.orientation;
    }

    public virtual void clear()
    {
        position = Vector3.zero;
        orientation = 0;
    }

    public bool Equals(Location l)
    {
        if (ReferenceEquals(null, l))
        {
            return false;
        }
        if (ReferenceEquals(this, l))
        {
            return true;
        }

        return position.Equals(l.position)
            && orientation.Equals(l.orientation);
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

        return obj.GetType() == GetType() && Equals((Location)obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    private float fmodf(float a, float b)
    {
        return a - b * Mathf.Floor(a / b);
    }

    public void SIMPLE_INTEGRATION(float duration, Vector3 _velocity, float _rotation)
    {
        position.x += _velocity.x * duration;
        position.y += _velocity.y * duration;
        position.z += _velocity.z * duration;
        orientation += _rotation * duration;
        orientation = fmodf(orientation, Mathf.PI * 2.0f);
    }

    public virtual void integrate(SteeringOutput steer, float duration)
    {
        SIMPLE_INTEGRATION(duration, steer.linear, steer.angular);
    }

    public void setOrientationFromVelocity(Vector3 velocity)
    {
        if (velocity.sqrMagnitude > 0)
        {
            orientation = Mathf.Atan2(velocity.y, velocity.x);
        }
    }

    public Vector3 getOrientationAsVector()
    {
        return new Vector3(Mathf.Sin(orientation),
            0,
            Mathf.Cos(orientation));
    }
}
