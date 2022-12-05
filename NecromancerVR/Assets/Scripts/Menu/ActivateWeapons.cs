using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateWeapons : MonoBehaviour
{
    public bool activate;
    // Start is called before the first frame update
    void Start()
    {
        if (activate)
        {
            StartCoroutine(DelayActivate());
        }
    }
    IEnumerator DelayActivate()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
            player.GetComponent<Player>().EndBuildPhase();
    }
    
}
