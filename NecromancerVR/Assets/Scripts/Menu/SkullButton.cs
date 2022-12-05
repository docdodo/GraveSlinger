using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkullButton : MonoBehaviour
{
    private bool entered;
    [SerializeField]
    private GameObject particle;
    [SerializeField]
    Button button;
    [SerializeField]
    Text text;
    [SerializeField]
    private AudioSource audioSource;
    private void Start()
    {
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
     
    }
    
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
        Instantiate(particle, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        audioSource.Play();


    }
    public void setSkullText(int skulls_)
    {
        text.text = "skulls remaining: " + skulls_;
    }
}
