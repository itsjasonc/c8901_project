using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingProjectileDecision : Decision
{
    public GameObject character;

    public IncomingProjectileDecision()
    {

    }

    public override bool getBranch()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");

        foreach(GameObject projectile in projectiles)
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            BoxCollider2D bc = projectile.GetComponent<BoxCollider2D>();
            RaycastHit2D[] results = new RaycastHit2D[50];
            int numHits = Physics2D.BoxCastNonAlloc(
                    projectile.transform.position,
                    bc.size,
                    0,
                    rb.velocity,
                    results
                );

            foreach (var result in results)
            {
                if (character != null && result.collider != null && result.collider.gameObject != null)
                {
                    if (result.collider.gameObject.name == character.name)
                    {
                        character.GetComponent<AIMovementScript>().avoidProjectileAction.target = projectile;
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
