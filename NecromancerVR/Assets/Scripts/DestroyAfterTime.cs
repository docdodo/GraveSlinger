using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public int time;
    private int currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = time;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime--;
        if(currentTime<=0)
        {
            Destroy(gameObject);
        }
    }
}
