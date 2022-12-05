using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class SkullData
{
    public int skulls;
    public string name;
}

public class SaveManager : MonoBehaviour
{
    
    [SerializeField]
    public SkullData[] skullDataArray;
    public int unusedSkulls;
    private static SaveManager _instance;

    public static SaveManager Instance { get { return _instance; } }
    public bool endless;
    public bool undeadMode;

   

private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("Awake saveManager");
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSkulls();
            SceneManager.sceneLoaded += LoadSkullsOnMenu;
        }
        
      
    }
    public void SetSkulls(string tag_, int skulls_)
    {
        foreach (SkullData data in skullDataArray)
        {
            if(data.name==tag_)
            {
                data.skulls = skulls_;

                break;
            }
        }
      
    }
    public int GetSkulls(string tag_)
    {
        foreach (SkullData data in skullDataArray)
        {
            if (data.name == tag_)
            {
                return data.skulls;

              
            }
        }
        return 0;
    }
    public void SaveSkulls()
    {
        foreach (SkullData data in skullDataArray)
        {
            PlayerPrefs.SetInt(data.name, data.skulls);
        }
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Skull");
        unusedSkulls = gos.Length;
        if (gos.Length > 0)
        {
            foreach (GameObject go in gos)
            {
                Destroy(go);
            }
        }
        GameObject obj = GameObject.FindGameObjectWithTag("UnUsedSkulls");
        if(obj!=null)
        {
          unusedSkulls+=  obj.GetComponent<UnusedSkulls>().GetSkulls();
            
        }
        PlayerPrefs.SetInt("UnusedSkulls",unusedSkulls);
        PlayerPrefs.Save();
    }
    public void LoadSkulls()
    {
        foreach (SkullData data in skullDataArray)
        {
           data.skulls= PlayerPrefs.GetInt(data.name);
        }
        unusedSkulls= PlayerPrefs.GetInt("UnusedSkulls");
       
    }
    public void LoadSkullsOnMenu(Scene scene_, LoadSceneMode mode_)
    {
        if (scene_.name == "MenuLevel")
        {


            foreach (SkullData data in skullDataArray)
            {
                data.skulls = PlayerPrefs.GetInt(data.name);
            }
            unusedSkulls = PlayerPrefs.GetInt("UnusedSkulls");
          

        }
    }
    
}
