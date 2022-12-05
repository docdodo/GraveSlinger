using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    protected NavMeshAgent agent;
    [SerializeField]
    private Vector3 goal;
    private bool Slow;
    public float speed;
    // Start is called before the first frame update
    virtual protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
     
        agent.speed = speed;
       
        
    }
    virtual public void ApplySlow(bool shouldSlow_)
    {
        if (shouldSlow_==true)
        {
            if (!Slow)
            {
                Slow = true;
                SetSpeed(speed / 2);
            }
        }
        else
        {
            if(Slow)
            {
                SetSpeed(speed);
            }
        }
    }
    virtual public void SetSpeed(float speed_)
    {
        
        agent.speed = speed_;
    }
    virtual public void SetPermanentSpeed(float speed_)
    {
        speed = speed;
        ApplySlow(false);
        agent.speed = speed;
    }



}
