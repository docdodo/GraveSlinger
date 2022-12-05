using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float damage;
    public float baseDamage;
    public int range;
    public int speed;
    public int BaseSpeed;
    public int value;
    private int timer;
    protected bool fullValue;
    public Tile myTile;
    protected virtual void Start()
    {
        timer = 2000;
        fullValue = true;
        CalculateDamage();
    }
    protected virtual void Update()
    {
        if(fullValue)
        {
            if(timer>0)
            timer--;
            else 
            {

                fullValue = false;
            }
        }
    }
    protected virtual void Attack()
    {

    }
    public virtual int Sell()
    {
        if (fullValue)
        {
            return value;
        }
        return value/2;
    }
    protected virtual void CalculateDamage()
    {
        damage = baseDamage;
        speed = BaseSpeed;
    }
}
