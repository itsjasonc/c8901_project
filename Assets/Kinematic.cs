using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Code converted to C# from https://github.com/idmillington/aicore
 */
public class Kinematic : Location
{
    // The linear velocity
    public Vector3 velocity;

    // The angular velocity
    public float rotation;

    public Kinematic() : base()
    {
        velocity = Vector3.zero;
        rotation = 0.0f;
    }

    public Kinematic(Vector3 _position, Vector3 _velocity) : base(_position)
    {
        velocity = _velocity;
        rotation = 0.0f;
    }

    public Kinematic(Location loc, Vector3 _velocity) : base(loc)
    {
        velocity = _velocity;
        rotation = 0.0f;
    }

    public Kinematic(Location loc) : base(loc)
    {
        velocity = Vector3.zero;
        rotation = 0.0f;
    }

    public Kinematic(Vector3 _position, float _orientation, Vector3 _velocity, float _avel) : base(_position, _orientation)
    {
        velocity = _velocity;
        rotation = _avel;
    }

    public override void clear()
    {
        base.clear();
        velocity = Vector3.zero;
        rotation = 0.0f;
    }

    public bool Equals(Kinematic k)
    {
        if (ReferenceEquals(null, k))
        {
            return false;
        }
        if (ReferenceEquals(this, k))
        {
            return true;
        }

        return position.Equals(k.position)
            && orientation.Equals(k.orientation)
            && velocity.Equals(k.velocity)
            && rotation.Equals(k.rotation);
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

        return obj.GetType() == GetType() && Equals((Kinematic)obj);
    }

    public static bool operator <(Kinematic k1, Kinematic k2)
    {
        return k1.position.x < k2.position.x;
    }

    public static bool operator >(Kinematic k1, Kinematic k2)
    {
        return k1.position.x > k2.position.x;
    }

    public static Kinematic operator +(Kinematic k1, Kinematic k2)
    {
        return new Kinematic(k1.position + k2.position, k1.orientation + k2.orientation, k1.velocity + k2.velocity, k1.rotation + k2.rotation);
    }

    public static Kinematic operator -(Kinematic k1, Kinematic k2)
    {
        return new Kinematic(k1.position - k2.position, k1.orientation - k2.orientation, k1.velocity - k2.velocity, k1.rotation - k2.rotation);
    }

    public static Kinematic operator *(Kinematic k1, Kinematic k2)
    {
        return new Kinematic(Vector3.Scale(k1.position, k2.position), k1.orientation * k2.orientation, Vector3.Scale(k1.velocity, k2.velocity), k1.rotation * k2.rotation);
    }

    public void trimMaxSpeed(float maxSpeed)
    {
        if (velocity.sqrMagnitude > maxSpeed * maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }
    }

    public void setOrientationFromVelocity()
    {
        if (velocity.sqrMagnitude > 0)
        {
            orientation = Mathf.Atan2(velocity.x, velocity.z);
        }
    }

    public void integrate(float duration)
    {
        SIMPLE_INTEGRATION(duration, velocity, rotation);
    }

    public override void integrate(SteeringOutput steer, float duration)
    {
        SIMPLE_INTEGRATION(duration, velocity, rotation);
        velocity.x += steer.linear.x * duration;
        velocity.y += steer.linear.y * duration;
        velocity.z += steer.linear.z * duration;
        rotation += steer.angular * duration;
    }

    public void integrate(SteeringOutput steer, float drag, float duration)
    {
        SIMPLE_INTEGRATION(duration, velocity, rotation);

        drag = Mathf.Pow(drag, duration);
        velocity *= drag;
        rotation *= drag * drag;

        velocity.x += steer.linear.x * duration;
        velocity.y += steer.linear.y * duration;
        velocity.z += steer.linear.z * duration;
        rotation += steer.angular * duration;
    }

    public void integrate(SteeringOutput steer, SteeringOutput drag, float duration)
    {
        SIMPLE_INTEGRATION(duration, velocity, rotation);

        velocity.x *= Mathf.Pow(drag.linear.x, duration);
        velocity.y *= Mathf.Pow(drag.linear.y, duration);
        velocity.z *= Mathf.Pow(drag.linear.z, duration);
        rotation *= Mathf.Pow(drag.angular, duration);

        velocity.x += steer.linear.x * duration;
        velocity.y += steer.linear.y * duration;
        velocity.z += steer.linear.z * duration;
        rotation += steer.angular * duration;
    }
}
