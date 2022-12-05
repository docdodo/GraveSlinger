using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PortalToMenu : MonoBehaviour
{
    private bool load;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponentInParent<Player>() != null)
        {
            if (!load)
            {
                
                load = true;
                SceneManager.LoadScene("MenuLevel");
            }
        }

    }
}
