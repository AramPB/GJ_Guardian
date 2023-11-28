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
    [SerializeField] private GameObject noseRButton;
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

    }
    public void ClickEyeLButton()
    {

    }
    public void ClickArmRButton()
    {

    }
    public void ClickArmLButton()
    {

    }
    public void ClickJawButton()
    {

    }
    public void ClickNoseRButton()
    {

    }
    public void ClickBodyButton()
    {

    }
    public void ClickCraniumButton()
    {

    }
}
