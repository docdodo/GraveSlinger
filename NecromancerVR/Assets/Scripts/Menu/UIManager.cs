using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int skulls;
    [SerializeField]
    private float damage;
    [SerializeField]
    private string name;
    [SerializeField]
    private GameObject skullObj;
    [SerializeField]
    private GameObject spawn;
    [SerializeField]
    private int skill1;
    [SerializeField]
    private int skill2;
    [SerializeField]
    private GameObject skill1IconOn;
    [SerializeField]
    private GameObject skill1IconOff;
    [SerializeField]
    private GameObject skill2IconOn;
    [SerializeField]
    private GameObject skill2IconOff;
    [SerializeField]
    private Text damageText;
    [SerializeField]
    private string damageString;
    [SerializeField]
    private Text skullText;
    [SerializeField]
    private string skullString;
    public void Start()
    {
        LoadSkulls();
    }
    public void AddSkull(int skulls_)
    {
        skulls += skulls_;
        if(skulls>=skill1)
        {
            skill1IconOn.SetActive(true);
            skill1IconOff.SetActive(false);
        }
        if(skulls==skill2)
        {
            skill2IconOn.SetActive(true);
            skill2IconOff.SetActive(false);
        }
        else if (skulls > skill2)
        {
            skulls = skill2;
            GameObject obj = GameObject.FindGameObjectWithTag("UnUsedSkulls");
            if (obj != null)
            {
                obj.GetComponent<UnusedSkulls>().AddSkull();
            }
        }
        skullText.text = skullString + skulls;
        SetSkulls();
    }
    public void AddDamage(float damage_)
    {
        damage += damage_;
        damageText.text = damageString + damage;
    }
    public void SubTractSkull()
    {
        if (skulls > 0)
        {
            skulls--;
            skullText.text = skullString + skulls;
            Instantiate(skullObj, spawn.transform.position, spawn.transform.rotation);
          
            if (skulls >= skill1)
            {
                skill1IconOn.SetActive(true);
                skill1IconOff.SetActive(false);
            }
            if (skulls == skill2)
            {
                skill2IconOn.SetActive(true);
                skill2IconOff.SetActive(false);
            }
           
           
            SetSkulls();
        }
    }
    public void SetSkulls()
    {
      GameObject obj=  GameObject.FindGameObjectWithTag("SaveManager");
        if(obj!=null)
        {
            obj.GetComponent<SaveManager>().SetSkulls(name, skulls);
        }
    }
    public void LoadSkulls()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("SaveManager");
        if (obj != null)
        {
            skulls = 0;
            AddSkull(obj.GetComponent<SaveManager>().GetSkulls(name));
        }
    }
    public void ResetSkull()
    {
        skulls = 0;
        if (skulls >= skill1)
        {
            skill1IconOn.SetActive(true);
            skill1IconOff.SetActive(false);
        }
        if (skulls >= skill2)
        {
            skill2IconOn.SetActive(true);
            skill2IconOff.SetActive(false);
        }
        skullText.text = skullString + skulls;
        SetSkulls();
    }
    public int GetSkulls()
    {
        return skulls;
    }
    

}
