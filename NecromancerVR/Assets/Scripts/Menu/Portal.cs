using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public LevelSelect selector;

    private void OnTriggerEnter(Collider other)
    {
       
      if(other.gameObject.GetComponentInParent<Player>()!=null)
        {
            selector.SelectALevel();
            Debug.Log("EnterPlayer");
        }
            
    }
}
