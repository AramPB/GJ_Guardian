using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject cityUI;//"Escena" de inspeccionar
    [SerializeField] GameObject queueUI;//"Escena" transició client
    [SerializeField] GameObject InspectUI;//"Escena" transició nit
    [SerializeField] GameObject gameOverUI;//"Escena" Final
    public ScannerController scannerController;

    //--- CIUTAT ---
    [SerializeField] GameObject dialogBackgroundPanel;
    [SerializeField] TextMeshProUGUI cityDialog;

    // -- INSPECT --
    // --- Night Specification UI ---
    [SerializeField] TextMeshProUGUI restriction_Text;
    [SerializeField] TextMeshProUGUI permitedCrimes_Text;

    //--- District list UI ---
    [SerializeField] TextMeshProUGUI district_NamesList_Text;
    [SerializeField] TextMeshProUGUI district_NumberList_Text;

    //--- Criminal Prove UI ---
    [SerializeField] TextMeshProUGUI crimes_Text;
    [SerializeField] TextMeshProUGUI crimes_Name_Text;
    [SerializeField] TextMeshProUGUI crimes_Age_Text;
    [SerializeField] TextMeshProUGUI crimes_Serial_Text;
    [SerializeField] Image crimes_Foto_Image;

    //--- DNI ---
    [SerializeField] Image dni_Foto_Image;
    [SerializeField] TextMeshProUGUI dni_Name_Text;
    [SerializeField] TextMeshProUGUI dni_Caducity_Text;
    [SerializeField] TextMeshProUGUI dni_Serial_Text;
    [SerializeField] TextMeshProUGUI dni_District_Text;
    [SerializeField] TextMeshProUGUI dni_Age_Text;
    [SerializeField] TextMeshProUGUI dni_Implants_Name_Text;
    [SerializeField] TextMeshProUGUI dni_Implants_number_Text;
    [SerializeField] Image dni_Criminal_Stamp_Image;

    //--- Implant Scanner ---
    [SerializeField] Image implants_Foto_Image;
    [SerializeField] TextMeshProUGUI implants_Name_Text;
    [SerializeField] TextMeshProUGUI implants_Number_Text;

    //--- Game Over ---
    [SerializeField] TextMeshProUGUI gameOver_Text;

    // --- --- STRINGS --- ---
    //--- CIUTAT ---
    [SerializeField] string cityDialog_String;

    // -- INSPECT --
    // --- Night Specification UI ---
    [SerializeField] string restriction_String;
    [SerializeField] string permitedCrimes_String;

    //--- Criminal Prove UI ---
    [SerializeField] string crimes_String;
    [SerializeField] string crimes_Name_String;
    [SerializeField] string crimes_Age_String;
    [SerializeField] string crimes_Serial_String;
    [SerializeField] Sprite crimes_Foto_Sprite;

    //--- DNI ---
    [SerializeField] Sprite dni_Foto_Sprite;
    [SerializeField] string dni_Name_String;
    [SerializeField] string dni_Caducity_String;
    [SerializeField] string dni_Serial_String;
    [SerializeField] string dni_District_String;
    [SerializeField] string dni_Age_String;
    [SerializeField] string dni_Implants_Name_String;
    [SerializeField] string dni_Implants_number_String;
    [SerializeField] Sprite dni_Criminal_Stamp_Sprite;

    //--- Implant Scanner ---
    [SerializeField] Sprite implants_Foto_Sprite;
    [SerializeField] string implants_Name_String;
    [SerializeField] string implants_Number_String;

    //--- Game Over ---
    [SerializeField] string gameOver_String;


    //---- Pages ----
    private int currentPage = 1;
    private bool hasCriminalProof = false;
    [SerializeField] private int maxPages = 5;
    [SerializeField] private GameObject docNightSpecifications;
    [SerializeField] private GameObject docDistrict;
    [SerializeField] private GameObject docImplant;
    [SerializeField] private GameObject docCriminalProof;
    [SerializeField] private GameObject docInstructions;

    #region Getters & Setters
    public static UIManager Instance { get; private set; }
    public string CityDialog_String { get => cityDialog_String; set => cityDialog_String = value; }
    public string Restriction_String { get => restriction_String; set => restriction_String = value; }
    public string Crimes_String { get => crimes_String; set => crimes_String = value; }
    public string Crimes_Name_String { get => crimes_Name_String; set => crimes_Name_String = value; }
    public string Crimes_Age_String { get => crimes_Age_String; set => crimes_Age_String = value; }
    public string Crimes_Serial_String { get => crimes_Serial_String; set => crimes_Serial_String = value; }
    public Sprite Crimes_Foto_Sprite { get => crimes_Foto_Sprite; set => crimes_Foto_Sprite = value; }
    public Sprite Dni_Foto_Sprite { get => dni_Foto_Sprite; set => dni_Foto_Sprite = value; }
    public string Dni_Name_String { get => dni_Name_String; set => dni_Name_String = value; }
    public string Dni_Caducity_String { get => dni_Caducity_String; set => dni_Caducity_String = value; }
    public string Dni_Serial_String { get => dni_Serial_String; set => dni_Serial_String = value; }
    public string Dni_District_String { get => dni_District_String; set => dni_District_String = value; }
    public string Dni_Age_String { get => dni_Age_String; set => dni_Age_String = value; }
    public string Dni_Implants_Name_String { get => dni_Implants_Name_String; set => dni_Implants_Name_String = value; }
    public string Dni_Implants_number_String { get => dni_Implants_number_String; set => dni_Implants_number_String = value; }
    public Sprite Dni_Criminal_Stamp_Sprite { get => dni_Criminal_Stamp_Sprite; set => dni_Criminal_Stamp_Sprite = value; }
    public Sprite Implants_Foto_Sprite { get => implants_Foto_Sprite; set => implants_Foto_Sprite = value; }
    public string Implants_Name_String { get => implants_Name_String; set => implants_Name_String = value; }
    public string Implants_Number_String { get => implants_Number_String; set => implants_Number_String = value; }
    public string PermitedCrimes_String { get => permitedCrimes_String; set => permitedCrimes_String = value; }
    public string GameOver_String { get => gameOver_String; set => gameOver_String = value; }

    #endregion

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

    public void ActivateCityUI(bool activate)
    {
        cityUI.SetActive(activate);
    }

    public void ActivateQueueUI(bool activate)
    {
        queueUI.SetActive(activate);
    }

    public void ActivateInspectUI(bool activate)
    {
        InspectUI.SetActive(activate);
    }

    public void updateUI()
    {
        //City
        cityDialog.SetText(cityDialog_String);
        
        //Night specification
        restriction_Text.SetText(restriction_String);
        permitedCrimes_Text.SetText(PermitedCrimes_String);

        //Implant scanner
        if (implants_Foto_Sprite && implants_Foto_Sprite != null)
        {
            try
            {
                implants_Foto_Image.sprite = implants_Foto_Sprite;
                implants_Foto_Image.SetNativeSize();
                

                //Ficar variable al night system per veure quin implante tenim seleccionat actualemnt
                if(NightSystem.Instance.currentSelectedImplant.ImplantType == ImplantType.ArmL || NightSystem.Instance.currentSelectedImplant.ImplantType == ImplantType.ArmR)
                {
                    implants_Foto_Image.gameObject.GetComponent<RectTransform>().sizeDelta /= new Vector2(1.6f, 1.6f);
                    implants_Foto_Image.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                }
                else
                {
                    implants_Foto_Image.gameObject.GetComponent<RectTransform>().sizeDelta /= new Vector2(1.3f, 1.3f);
                    implants_Foto_Image.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        implants_Name_Text.SetText(implants_Name_String);
        implants_Number_Text.SetText(Implants_Number_String);

        //DNI
        if (dni_Foto_Sprite && dni_Foto_Sprite != null)
        {
            try
            {
                dni_Foto_Image.sprite = dni_Foto_Sprite;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        dni_Name_Text.SetText(dni_Name_String);
        dni_Caducity_Text.SetText(dni_Caducity_String);
        dni_Serial_Text.SetText(dni_Serial_String);
        dni_District_Text.SetText("D-"+dni_District_String);
        dni_Age_Text.SetText(dni_Age_String);
        dni_Implants_Name_Text.SetText(dni_Implants_Name_String);
        dni_Implants_number_Text.SetText(dni_Implants_number_String);
        if (dni_Criminal_Stamp_Sprite && dni_Criminal_Stamp_Sprite != null)
        {
            try
            {
                dni_Criminal_Stamp_Image.sprite = dni_Criminal_Stamp_Sprite;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        //Crime Doc
        crimes_Name_Text.SetText(crimes_Name_String);
        crimes_Age_Text.SetText(crimes_Age_String);
        if (crimes_Foto_Sprite && crimes_Foto_Sprite != null)
        {
            try
            {
                crimes_Foto_Image.sprite = crimes_Foto_Sprite;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        crimes_Serial_Text.SetText(crimes_Serial_String);
        crimes_Text.SetText(crimes_String);
    }

    #region Pages
    public void ResetPages()
    {
        hasCriminalProof = false;
        UpdatePage(1);
    }

    public void ObtainCP()
    {
        hasCriminalProof = true;
        UpdatePage(5);
    }

    public void PreviousPage()
    {
        if (currentPage != 1)
        {
            UpdatePage(currentPage - 1);
            SoundsController.Instance.buttonSoundPlay();
        }
        else
        {
            SoundsController.Instance.declineSoundPlay();
        }
    }

    public void NextPage()
    {
        if (!(currentPage == maxPages || (currentPage == (maxPages-1) && !hasCriminalProof)))
        {
            SoundsController.Instance.buttonSoundPlay();
            UpdatePage(currentPage + 1);
        }
        else
        {
            //So de negar passar pagina
            SoundsController.Instance.declineSoundPlay();
        }
    }

    public void ActivateGameOverUI(bool goodEnding)
    {
        if (goodEnding)
        {
            
        }
        gameOverUI.SetActive(true);
    }

    private void UpdatePage(int newPage)
    {
        switch (currentPage)
        {
            case 1:
                docInstructions.SetActive(false);
                break;
            case 2:
                docNightSpecifications.SetActive(false);
                break;
            case 3:
                docDistrict.SetActive(false);
                break;
            case 4:
                docImplant.SetActive(false);
                break;
            case 5:
                docCriminalProof.SetActive(false);
                break;
        }

        switch (newPage)
        {
            case 1:
                docInstructions.SetActive(true);
                break;
            case 2:
                docNightSpecifications.SetActive(true);
                break;
            case 3:
                docDistrict.SetActive(true);
                break;
            case 4:
                docImplant.SetActive(true);
                break;
            case 5:
                docCriminalProof.SetActive(true);
                break;
        }
        currentPage = newPage;
    }
    #endregion


}
