using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    private float time;
    public float speed;
   
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time = Time.deltaTime * speed;
        transform.Rotate(Vector3.up, time);
    }
}
