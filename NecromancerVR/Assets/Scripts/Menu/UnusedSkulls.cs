using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnusedSkulls : MonoBehaviour
{
    [SerializeField]
    private Text skullText;
    [SerializeField]
    private string skullString;
    private int skulls;
    [SerializeField]
    private GameObject spawn;
    [SerializeField]
    private GameObject skullObj;
   
    private void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        if (obj != null)
        {
            
            SetSkulls(obj.GetComponent<SaveManager>().unusedSkulls);

            Debug.Log("unused skulls from save manager" + obj.GetComponent<SaveManager>().unusedSkulls);
            Debug.Log("unused skulls from playerprefs" + PlayerPrefs.GetInt("UnusedSkulls"));
           
        }
    }


    public void SetSkulls(int skullS_)
    {
        skulls = skullS_;
        skullText.text = skullString + skulls;
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("SkullButton");
        foreach (GameObject obj in objs)
        {
            obj.GetComponent<SkullButton>().setSkullText(skulls);
        }
    }
    public void SubTractSkull()
    {
        if (skulls > 0)
        {
            skulls--;
            skullText.text = skullString + skulls;
            Instantiate(skullObj, spawn.transform.position, spawn.transform.rotation);

            GameObject[] objs;
            objs = GameObject.FindGameObjectsWithTag("SkullButton");
            foreach (GameObject obj in objs)
            {
                obj.GetComponent<SkullButton>().setSkullText(skulls);
            }
        }
    }
    public void ResetSkulls()
    {
        GameObject[] objects;
       objects= GameObject.FindGameObjectsWithTag("UiManager");
        foreach(GameObject obj in objects)
        {
           skulls+= obj.GetComponent<UIManager>().GetSkulls();
            obj.GetComponent<UIManager>().ResetSkull();
        }
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Skull");
        skulls += gos.Length;
        if (gos.Length > 0)
        {
            foreach (GameObject go in gos)
            {
                Destroy(go);
            }
        }
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("SkullButton");
        foreach (GameObject obj in objs)
        {
            obj.GetComponent<SkullButton>().setSkullText(skulls);
        }
        skullText.text = skullString + skulls;

    }
    public void spawnSkull(Transform transform_)
    {
        if (skulls > 0)
        {
            skulls--;
            skullText.text = skullString + skulls;
            Instantiate(skullObj, transform_.position, transform_.rotation);

            GameObject[] objs;
            objs = GameObject.FindGameObjectsWithTag("SkullButton");
            foreach (GameObject obj in objs)
            {
                obj.GetComponent<SkullButton>().setSkullText(skulls);
            }
        }
    }
    public int GetSkulls()
    {
        return skulls;
    }
    public void AddSkull()
    {
        skulls++;
             skullText.text = skullString + skulls;
        GameObject[] objs;
        objs = GameObject.FindGameObjectsWithTag("SkullButton");
        foreach (GameObject obj in objs)
        {
            obj.GetComponent<SkullButton>().setSkullText(skulls);
        }
        
    }
}
