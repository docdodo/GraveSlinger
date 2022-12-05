using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool open;
    private bool turningOpen;
    private float turningTime;
    public GameObject doorR;
    public GameObject doorL;
    public string doorName;

    // Start is called before the first frame update
    private void Start()
    {
       if( PlayerPrefs.GetInt(doorName)==1)
        {
            open = true;
            turningOpen = true;
            turningTime = 2;
        }
    }
    public void Open()
    {
        if(!open)
        {
            open = true;
            turningOpen=true;
            turningTime = 2;
            PlayerPrefs.SetInt(doorName, 1);
        }
    }
    private void Update()
    {
        if(turningOpen)
        {
            turningTime -= Time.deltaTime;
            if(turningTime<=0)
            {
                turningOpen = false;
            }
            else
            {
               
               doorL.transform.RotateAround(doorL.transform.position, doorL.transform.up, Time.deltaTime * 45f);
                doorR.transform.RotateAround(doorR.transform.position, doorR.transform.up, Time.deltaTime * -45f);
                
            }
        }
    }
}
