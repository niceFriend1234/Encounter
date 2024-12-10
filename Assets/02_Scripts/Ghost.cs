using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public float eatDistance = 0.3f;
    public Animator animator;
    public NavMeshAgent agent;
    public float speed = 1;
    void Start()
    {
        
    }

    public GameObject GetClosestOrb()
    
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        List<GameObject> orbs = OrbSpawner.instance.spawnedOrbs;

        foreach (var item in orbs)
        {
            Vector3 ghostPosition = transform.position;
            ghostPosition.y = 0;
            Vector3 orbPosition = item.transform.position;
            orbPosition.y = 0;
            
            float d = Vector3.Distance(ghostPosition, orbPosition);

            if (d < minDistance)
            {
                minDistance = d;
                closest = item;
            }
        }

        if (minDistance < eatDistance)
        {
            OrbSpawner.instance.DestroyOrb(closest);
        }

        return closest;
    }
    void Update()
    {
        if (!agent.enabled) return;

        GameObject closest = GetClosestOrb();

        if (closest)
        {
            Vector3 targetPosition = closest.transform.position;
            agent.SetDestination(targetPosition);
            agent.speed = speed;
        }
    }
    
    public void Kill()
    {
        agent.enabled = false;
        animator.SetTrigger("Death");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
