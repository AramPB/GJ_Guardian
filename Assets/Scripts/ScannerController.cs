using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScannerController : MonoBehaviour
{
    [SerializeField] private GameObject scannerUI;
    [SerializeField] private GameObject eyeRButton;
    [SerializeField] private GameObject eyeLButton;
    [SerializeField] private GameObject armRButton;
    [SerializeField] private GameObject armLButton;
    [SerializeField] private GameObject jawButton;
    [SerializeField] private GameObject noseButton;
    [SerializeField] private GameObject bodyButton;
    [SerializeField] private GameObject craniumButton;

    private bool found = false;

    private void Awake()
    {
        scannerUI = NightSystem.Instance.scanner;
        UIManager.Instance.scannerController = this;
        scannerUI.SetActive(false);
    }

    private void OnEnable()
    {
        scannerUI = NightSystem.Instance.scanner;
        UIManager.Instance.scannerController = this;
        scannerUI.SetActive(false);
    }

    private void Start()
    {
        scannerUI = NightSystem.Instance.scanner;
        UIManager.Instance.scannerController = this;
        scannerUI.SetActive(false);
    }

    public void changeButtonVisibility(bool visibility)
    {
        if (!visibility)
        {
            scannerUI.SetActive(visibility);
        }
        if(eyeRButton) eyeRButton.SetActive(visibility);
        if (eyeLButton) eyeLButton.SetActive(visibility);
        if (armRButton) armRButton.SetActive(visibility);
        if (armLButton) armLButton.SetActive(visibility);
        if (jawButton) jawButton.SetActive(visibility);
        if (noseButton) noseButton.SetActive(visibility);
        if (bodyButton) bodyButton.SetActive(visibility);
        if (craniumButton) craniumButton.SetActive(visibility);
    }

    public void hideScannerUI()
    {
        scannerUI.SetActive(false);
    }

    public void ClickEyeRButton()
    {
        found = false;
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.EyeR))
            {
                found = true;
                scannerUI.SetActive(true);
                updateUIInfo(implant);
                return;
            }
        }
        if (!found) scannerUI.SetActive(false);
    }
    public void ClickEyeLButton()
    {
        found = false;
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.EyeL))
            {
                found = true;
                scannerUI.SetActive(true);
                updateUIInfo(implant);
                return;
            }
        }
        if (!found) scannerUI.SetActive(false);
    }
    public void ClickArmRButton()
    {
        found = false;
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.ArmR))
            {
                found = true;
                scannerUI.SetActive(true);
                updateUIInfo(implant);
                return;
            }
        }
        if (!found) scannerUI.SetActive(false);
    }
    public void ClickArmLButton()
    {
        found = false;
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.ArmL))
            {
                found = true;
                scannerUI.SetActive(true);
                updateUIInfo(implant);
                return;
            }
        }
        if (!found) scannerUI.SetActive(false);
    }
    public void ClickJawButton()
    {
        found = false;
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.Jaw))
            {
                found = true;
                scannerUI.SetActive(true);
                updateUIInfo(implant);
                return;
            }
        }
        if (!found) scannerUI.SetActive(false);
    }
    public void ClickNoseButton()
    {
        found = false;
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.Nose))
            {
                found = true;
                scannerUI.SetActive(true);
                updateUIInfo(implant);
                return;
            }
        }
        if (!found) scannerUI.SetActive(false);
    }
    public void ClickBodyButton()
    {
        found = false;
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.Torso))
            {
                found = true;
                scannerUI.SetActive(true);
                updateUIInfo(implant);
                return;
            }
        }
        if (!found) scannerUI.SetActive(false);
    }

    public void ClickCraniumButton()
    {
        found = false;
        foreach (var implant in NightSystem.Instance.NightProgress.CurrentCustomer.GetImplants)
        {
            if (implant.ImplantType.Equals(ImplantType.Cranium))
            {
                found = true;
                scannerUI.SetActive(true);
                updateUIInfo(implant);
                return;
            }
        }
        if (!found) scannerUI.SetActive(false);
    }

    private void updateUIInfo(Implant implant)
    {
        SoundsController.Instance.scannSoundPlay();
        NightSystem.Instance.currentSelectedImplant = implant;
        UIManager.Instance.Implants_Name_String = implant.ImplantName;
        UIManager.Instance.Implants_Number_String = implant.ImplantManufacterNumber;
        UIManager.Instance.Implants_Foto_Sprite = implant.ImplantImage;
        UIManager.Instance.updateUI();
    }
}
