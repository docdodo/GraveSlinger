using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PhysicalButton : MonoBehaviour
{
    private bool entered;
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
    }
    //private void OnCollisionEnter(Collision collision)
    // {
    //     if(collision.gameObject.tag=="Hand")
    //     {
    //          if(!entered)
    //          {
    //               entered = true;
    //           Enter();
    //           }

    //       }
    //   }
    //    private void OnCollisionExit(Collision collision)
    //{
    //       if (collision.gameObject.tag == "Hand")
    //      {
    //          if (entered)
    //         {
    //            entered = false;
    //            Enter();
    //       }
    //    }
    //  }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ButtonBase")
        {
            Pressed();
        }
    }

    protected virtual void Exit()
    {
        Debug.Log("Exit");
    }
    protected virtual void Enter()
    {
        Debug.Log("Enter");
    }
    protected virtual void Pressed()
    {
        Debug.Log("Pressed");
        button.onClick.Invoke();
        Instantiate(particle,gameObject.transform.position,new Quaternion(0,0,0,0));
        audioSource.Play();
    }
}
