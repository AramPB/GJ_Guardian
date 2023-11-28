using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScannerController : MonoBehaviour
{
    [SerializeField] private GameObject eyeRButton;
    [SerializeField] private GameObject eyeLButton;
    [SerializeField] private GameObject armRButton;
    [SerializeField] private GameObject armLButton;
    [SerializeField] private GameObject jawButton;
    [SerializeField] private GameObject noseButton;
    [SerializeField] private GameObject bodyButton;
    [SerializeField] private GameObject craniumButton;

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }
    #endregion

    public static ScannerController Instance { get; private set; }

    public void UpdateUI()
    {

    }

    public void ClickEyeRButton()
    {
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.EyeR))
            {
                UIManager.Instance.Implants_Name_String = implant.ImplantName;
                UIManager.Instance.Implants_Number_String = implant.ImplantManufacterNumber;
                UIManager.Instance.Implants_Foto_Sprite = implant.ImplantImage;
            }
        }
    }
    public void ClickEyeLButton()
    {
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.EyeL))
            {
                
            }
        }
    }
    public void ClickArmRButton()
    {
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.ArmR))
            {
                
            }
        }
    }
    public void ClickArmLButton()
    {
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.ArmL))
            {
                
            }
        }
    }
    public void ClickJawButton()
    {
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.Jaw))
            {

            }
        }
    }
    public void ClickNoseButton()
    {
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.Nose))
            {

            }
        }
    }
    public void ClickBodyButton()
    {
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.Torso))
            {

            }
        }
    }

    public void ClickCraniumButton()
    {
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.Cranium))
            {

            }
        }
    }
}
