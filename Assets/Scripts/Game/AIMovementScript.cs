using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementScript : MonoBehaviour
{
    public enum AI_STATE
    {
        HEALTH_LOW,
        HEALTH_HIGH
    };
    public GameObject target;
    public Vector3 heading; // The current facing direction
    public KinematicSeek seekComponent;
    public KinematicFlee fleeComponent;
    public KinematicArrive arriveComponent;
    public PlayerScript playerScript;

    public GameObject projectile;
    public float timeLeftBeforeFiring = 50.0f;
    public float lastFired = 0.0f;
    public float PROJECTILE_SPEED = 10.0f;

    public AI_STATE currentState;

    public TargetNearbyDecision lowHealthTargetNearby;
    public IncomingProjectileDecision lowHealthTargetNearIncomingProjectile;
    public IncomingProjectileDecision lowHealthTargetFarIncomingProjectile;

    public TargetNearbyDecision highHealthTargetNearby;
    public IncomingProjectileDecision highHealthTargetNearIncomingProjectile;
    public IncomingProjectileDecision highHealthTargetFarIncomingProjectile;

    public ArrivePlayerAction arrivePlayerAction;
    public ShootPlayerAction shootPlayerAction;
    public SeekHealthAction seekHealthAction;
    public AvoidPlayerAction avoidPlayerAction;
    public AvoidProjectileAction avoidProjectileAction;

    // Start is called before the first frame update
    void Start()
    {
        currentState = AI_STATE.HEALTH_HIGH;
        seekComponent = GetComponent<KinematicSeek>();
        fleeComponent = GetComponent<KinematicFlee>();
        arriveComponent = GetComponent<KinematicArrive>();
        playerScript = GetComponent<PlayerScript>();

        lowHealthTargetNearby = new TargetNearbyDecision(5.0f);
        lowHealthTargetNearIncomingProjectile = new IncomingProjectileDecision();
        lowHealthTargetFarIncomingProjectile = new IncomingProjectileDecision();

        highHealthTargetNearby = new TargetNearbyDecision(5.0f);
        highHealthTargetNearIncomingProjectile = new IncomingProjectileDecision();
        highHealthTargetFarIncomingProjectile = new IncomingProjectileDecision();

        arrivePlayerAction = new ArrivePlayerAction();
        shootPlayerAction = new ShootPlayerAction();
        seekHealthAction = new SeekHealthAction();
        avoidPlayerAction = new AvoidPlayerAction();
        avoidProjectileAction = new AvoidProjectileAction();

        avoidProjectileAction.kinematicComponent = fleeComponent;
        avoidProjectileAction.character = gameObject;
        arrivePlayerAction.kinematicComponent = arriveComponent;
        arrivePlayerAction.character = gameObject;
        arrivePlayerAction.target = target;
        seekHealthAction.kinematicComponent = seekComponent;
        seekHealthAction.character = gameObject;
        avoidPlayerAction.kinematicComponent = fleeComponent;
        avoidPlayerAction.character = gameObject;
        avoidPlayerAction.target = target;
        shootPlayerAction.kinematicComponent = arriveComponent;
        shootPlayerAction.character = gameObject;
        shootPlayerAction.target = target;

        lowHealthTargetNearby.character = gameObject;
        lowHealthTargetNearby.target = target;
        lowHealthTargetNearby.trueBranch = lowHealthTargetNearIncomingProjectile;
        lowHealthTargetNearby.falseBranch = lowHealthTargetFarIncomingProjectile;

        lowHealthTargetNearIncomingProjectile.character = gameObject;
        lowHealthTargetNearIncomingProjectile.trueBranch = avoidProjectileAction;
        lowHealthTargetNearIncomingProjectile.falseBranch = avoidPlayerAction;

        lowHealthTargetFarIncomingProjectile.character = gameObject;
        lowHealthTargetFarIncomingProjectile.trueBranch = avoidProjectileAction;
        lowHealthTargetFarIncomingProjectile.falseBranch = seekHealthAction;

        highHealthTargetNearby.character = gameObject;
        highHealthTargetNearby.target = target;
        highHealthTargetNearby.trueBranch = highHealthTargetNearIncomingProjectile;
        highHealthTargetNearby.falseBranch = highHealthTargetFarIncomingProjectile;

        highHealthTargetNearIncomingProjectile.character = gameObject;
        highHealthTargetNearIncomingProjectile.trueBranch = avoidProjectileAction;
        highHealthTargetNearIncomingProjectile.falseBranch = shootPlayerAction;

        highHealthTargetFarIncomingProjectile.character = gameObject;
        highHealthTargetFarIncomingProjectile.trueBranch = avoidProjectileAction;
        highHealthTargetFarIncomingProjectile.falseBranch = arrivePlayerAction;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.health <= playerScript.LOW_HEALTH)
        {
            currentState = AI_STATE.HEALTH_LOW;
        }
        else
        {
            currentState = AI_STATE.HEALTH_HIGH;
        }
    }

    private void FixedUpdate()
    {
        float duration = Time.deltaTime;
        SteeringOutput steer = new SteeringOutput();
        float angle = 0.0f;

        DecisionTreeNode node = null;

        GameObject[] healthPacks = GameObject.FindGameObjectsWithTag("Health");

        if (healthPacks.Length > 0)
        {
            seekHealthAction.target = healthPacks[0];
            foreach (var hp in healthPacks)
            {
                float currentDistance = (seekHealthAction.target.transform.position - transform.position).magnitude;
                float indexDistance = (hp.transform.position - transform.position).magnitude;

                if (indexDistance < currentDistance)
                {
                    seekHealthAction.target = hp;
                }
            }
        }

        if (currentState == AI_STATE.HEALTH_LOW)
        {
            node = lowHealthTargetNearby.makeDecision();
        }
        else if (currentState == AI_STATE.HEALTH_HIGH)
        {
            node = highHealthTargetNearby.makeDecision();
        }
        if (node is DecisionTreeAction)
        {
            ((DecisionTreeAction)node).doSomething();

            if (node is ShootPlayerAction)
            {
#if DEBUG
                heading.x = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
                heading.y = Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad);
                Vector3 headingLine = new Vector3(heading.x, heading.y, 0);
                Debug.DrawLine(transform.position, transform.position + (headingLine * 2), Color.red);
#endif

                if (lastFired <= 0)
                {
                    GameObject bullet = Instantiate(projectile, transform.position + (heading * 1.5f), transform.rotation);
                    bullet.GetComponent<Rigidbody2D>().AddForce(heading * PROJECTILE_SPEED, ForceMode2D.Impulse);
                    lastFired = timeLeftBeforeFiring;
                }
            }
        }
        if (lastFired > 0)
        {
            lastFired -= Time.deltaTime * 100;
        }
    }
}
