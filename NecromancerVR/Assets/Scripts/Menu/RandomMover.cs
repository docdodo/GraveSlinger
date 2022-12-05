using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMover : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    BoxCollider box;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider>();
        agent.SetDestination(GetRandomNavmeshLocation(20.0f));
        //agent.updatePosition = false;  agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.enabled==true)
        {
            if (agent.isOnNavMesh)
            {
                if (agent.remainingDistance < 2.0f)
                    agent.SetDestination(GetRandomNavmeshLocation(20.0f));
                anim.SetFloat("Speed", agent.velocity.magnitude);
            }
            
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }
    public Vector3 GetRandomNavmeshLocation(float radius)
    {
        Vector3 finalPos= Vector3.zero;
        for (int i = 0; i < 60; i++)
        {
            Vector3 randomDir = Random.insideUnitSphere * radius;
            randomDir += gameObject.transform.position;
            NavMeshHit hit;
             finalPos = Vector3.zero;
            if (NavMesh.SamplePosition(randomDir, out hit, radius, 1))
            {
                finalPos = hit.position;
             
                break;
            }

        }
            return finalPos;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Block")
        agent.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Hand")
            Debug.Log("Collide player");
    }
}
