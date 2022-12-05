using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EffectPillar : MonoBehaviour
{
    public ParticleSystem sys;
    public float rangeMax;
    public float rangeMin;
    private float timer;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer<=0)
        {
            timer = Random.RandomRange(rangeMin, rangeMax);
            sys.Play();
        }
    }
}
