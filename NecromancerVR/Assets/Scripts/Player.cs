using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public OVRInput.Button useButton;
    public OVRInput.Button altuseButton;
    private InputDevice rightHand;
    private InputDevice leftHand;
    public UseableObject rightObject;
    [SerializeField]
    private List<UseableObject> Objects;
    [SerializeField]
    private List<UseableObject> BuildObjects;
    [SerializeField]
    private List<UseableObject> ObjectsRight;
    [SerializeField]
    private List<UseableObject> BuildObjectsRight;
    [SerializeField]
    private List<UseableObject> ObjectsLeft;
    [SerializeField]
    private List<UseableObject> BuildObjectsLeft;
    [SerializeField]
    private GameObject LeftTeleport;
    [SerializeField]
    private GameObject RightTeleport;
    private bool BuildPhase;
    private bool rightHanded;
    private int weaponNumber;
    private bool SwapPressed;
    private int currentMoney;
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private Text moneyText;
    [SerializeField]
    private Text moneyTextLeft;
    [SerializeField]
    private Text moneyTextRight;
    public List<EnemySpawner> spawners;
    public List<GameObject> enemies1;
    private bool WaveSpawnPressed;
    bool RtriggerPressed;
    bool RprimaryPressed;
    bool RgripPressed;
    bool RsecondaryPressed;
    bool LprimaryPressed;
    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller, devices);
        if (devices.Count > 0)
        {


            rightHand = devices[1];
            leftHand = devices[0];
        }
        startHands();
        rightObject = BuildObjects[0];
        rightObject.gameObject.SetActive(true);
        BuildPhase = true;
        //currentMoney = 400;
       // EndBuildPhase();
        moneyText.text = "Money: " + currentMoney;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightObject)
        {
            if (rightHanded)
            {
                Debug.Log("right");
                rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out  RtriggerPressed);
                rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out  RprimaryPressed);
                rightHand.TryGetFeatureValue(CommonUsages.gripButton, out  RgripPressed);
                rightHand.TryGetFeatureValue(CommonUsages.secondaryButton, out  RsecondaryPressed);
                leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out  LprimaryPressed);
            }
            else
            {
                Debug.Log("left");
                leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out  RtriggerPressed);
                leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out  RprimaryPressed);
                leftHand.TryGetFeatureValue(CommonUsages.gripButton, out  RgripPressed);
                leftHand.TryGetFeatureValue(CommonUsages.secondaryButton, out  RsecondaryPressed);
                rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out  LprimaryPressed);
            }
            
            if (RtriggerPressed)
            {
                
                    rightObject.Use();
            }
            else if (RprimaryPressed&&BuildPhase||RgripPressed)
            {
                
                    rightObject.AltUse();
            }
            else if (RsecondaryPressed&&!SwapPressed)
            {
                SwitchWeaponUp();
                SwapPressed = true;
            }
            else if(!RsecondaryPressed)
            {
                SwapPressed = false;
            }
            if(LprimaryPressed)
            {
                Time.timeScale = 2.0f;
            }
            else
            {
                Time.timeScale = 1.0f;
            }
           
            

        }
    }
    void SwitchWeaponUp()
    {
        if (!BuildPhase)
        {
            if (rightObject == Objects.Last())
            {
                rightObject.gameObject.SetActive(false);
                weaponNumber = 0;
                rightObject = Objects[0];
                rightObject.gameObject.SetActive(true);
            }
            else
            {
                rightObject.gameObject.SetActive(false);
                weaponNumber += 1;
                rightObject = Objects[weaponNumber];
                rightObject.gameObject.SetActive(true);
            }
        }
        else
        {
            if(BuildObjects[0]==rightObject)
            {
                rightObject.gameObject.SetActive(false);
                
                rightObject = BuildObjects[1];
                rightObject.gameObject.SetActive(true);

            }
            else
            {
                rightObject.gameObject.SetActive(false);

                rightObject = BuildObjects[0];
                rightObject.gameObject.SetActive(true);
            }
        }
    }
   public void AddMoney(int money_)
    {
        currentMoney += money_;
        moneyText.text = "Money: " + currentMoney;
    }
   public  int GetMoney()
    {
        return currentMoney;
    }
    public void StartBuildPhase()
    {
        BuildPhase = true;
        rightObject.gameObject.SetActive(false);
        rightObject = BuildObjects[0];
        rightObject.gameObject.SetActive(true);
    }
    public void EndBuildPhase()
    {
        BuildPhase = false;
        rightObject.gameObject.SetActive(false);
        rightObject = Objects[weaponNumber];
        rightObject.gameObject.SetActive(true);
    }
    public void ChangeHands()
    {
        if (PlayerPrefs.GetInt("RightHanded") == 1)
            PlayerPrefs.SetInt("RightHanded", 0);
        else
            PlayerPrefs.SetInt("RightHanded", 1);
        SceneManager.LoadScene("MenuLevel");

    }
    private void startHands()
    {
        if (PlayerPrefs.GetInt("RightHanded")==1)
        {
            
            for (int i = 0; i <= Objects.Count-1; i++)
            {
                Objects[i] = ObjectsLeft[i];

            }
            for (int i = 0; i <= BuildObjects.Count-1; i++)
            {
                BuildObjects[i] = BuildObjectsLeft[i];

            }
            LeftTeleport.SetActive(false);
            RightTeleport.SetActive(true);
            rightHanded = false;
            moneyText = moneyTextLeft;
        }
        else
        {
            for (int i = 0; i <= Objects.Count-1; i++)
            {
                Objects[i] = ObjectsRight[i];
            }
            for (int i = 0; i <= BuildObjects.Count-1; i++)
            {
                BuildObjects[i] = BuildObjectsRight[i];

            }
            RightTeleport.SetActive(false);
            LeftTeleport.SetActive(true);
            rightHanded = true;
            moneyText = moneyTextRight;
        }
    }
}
