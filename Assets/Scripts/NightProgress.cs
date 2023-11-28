using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightProgress : MonoBehaviour
{

    [SerializeField] private GameObject clientImage;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject DNIGameObject;
    [SerializeField] private GameObject buttonCP;
    
    [SerializeField] private float tmpWaitTime;
    private float tmpStartWait;

    private int _currentClientNumber;

    private Customer currentCustomer;

    private bool actualPass;

    private int maxClients;
    private List<Customer> clientsList;

    private bool inProgress = false;


    private enum State
    {
        Apparition,
        Documentation,
        DNI,
        DataCheck,
        CriminalProof,
        FinalDecision,
        EndDialogue,
        Waiting
    }
    private State currentState = State.Waiting;

    public Customer CurrentCustomer { get => currentCustomer; set => currentCustomer = value; }

    // Start is called before the first frame update
    void Start()
    {

        if (clientImage.GetComponent<Image>())
        {
            if (dialogGameObject) {
                if (DNIGameObject) {

                    SwitchState(State.Waiting);
                }
                else
                {
                    Debug.Log("No encuentra el DNI GO");
                }
            }
            else
            {
                Debug.Log("No encuentra el Dialog GO");
            }
        }
        else
        {
            Debug.Log("No encuentra sprite de Cliente");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //NIGHT LOOP
        switch (currentState)
        {
            //Llega el cliente
            case State.Apparition:
                UpdateClientApparition();
                break;
            //Guardia Pide Identificación -> Cliente - Guardia diálogo
            case State.Documentation:
                UpdateDocumentationAsk();
                break;
            //Obtención de DNI(o no)
            case State.DNI:
                UpdateObtainingDNI();
                break;
            //Revisar datos
            case State.DataCheck:
                UpdateDataCheck();
                break;
            //Si tiene antecedentes te tiene que dar el justificante penal
            case State.CriminalProof:
                UpdateCriminalProof();
                break;
            //Decisión(Dejar entrar o no)
            case State.FinalDecision:
                UpdateFinalDecision();
                break;
            //Dialogo Final
            case State.EndDialogue:
                UpdateEndDialogue();
                break;
             //waiting
            case State.Waiting:
                UpdateWaiting();
                break;
        }
        
    }

    //------------CLIENT APPARITION-----------
    #region ClientApparition
    private void StartClientApparition()
    {

        ScannerController.Instance.hideScannerUI();
        CurrentCustomer = clientsList[_currentClientNumber - 1];
        clientImage.GetComponent<Image>().sprite = CurrentCustomer.GetSprite;
        tmpStartWait = Time.time;
    }
    private void UpdateClientApparition()
    {
        //Animaión de la aparición
        //Al acabar:
        if (Time.time >= tmpWaitTime + tmpStartWait)
        {
            SwitchState(State.Documentation);
        }
        //
    }
    private void EndClientApparition()
    {

    }
    public void StartLoop(int maxCustomers, List<Customer> customersList)
    {
        maxClients = maxCustomers;
        _currentClientNumber = 1;
        clientsList = customersList;
        if (clientsList.Count <= 0)
        {
            Debug.Log("Clients List Empty");
        }
        else
        {
            inProgress = true;
            DNIGameObject.SetActive(false);
            buttonCP.SetActive(false);
            UIManager.Instance.ResetPages();
            UIManager.Instance.updateUI();
            SwitchState(State.Apparition);
        }
    }
    #endregion

    //----------DOCUMENTATION ASK----------
    #region Documentation
    private void StartDocumentationAsk()
    {
        tmpStartWait = Time.time;
    }
    private void UpdateDocumentationAsk()
    {
        //Animaión de la documentación y dialogos
        //Al acabar:
        if (Time.time >= tmpWaitTime + tmpStartWait)
        {
            SwitchState(State.DNI);
        }
        //
    }
    private void EndDocumentationAsk()
    {

    }
    #endregion

    //---------OBTAINING DNI----------
    #region DNI
    private void StartObtainingDNI()
    {
        if (CurrentCustomer.GetDNIToGive)
        {
            //Setear la nueva info de DNI
            DNIUpdateInfo();
        }
        else
        {
            Debug.Log("No DNI");
        }
        tmpStartWait = Time.time;
    }
    private void UpdateObtainingDNI()
    {
        //Animacion de DNI
        //Al acabar:
        if (Time.time >= tmpWaitTime + tmpStartWait)
        {
            SwitchState(State.DataCheck);
        }
        //
    }
    private void EndObtainingDNI()
    {

    }
    private void DNIUpdateInfo()
    {
        DNIGameObject.SetActive(true);
        UIManager.Instance.Dni_Age_String = CurrentCustomer.GetAge.ToString();
        UIManager.Instance.Dni_Name_String = CurrentCustomer.GetName;
        UIManager.Instance.Dni_Serial_String = CurrentCustomer.GetId;
        UIManager.Instance.Dni_Foto_Sprite = CurrentCustomer.GetPhoto;
        UIManager.Instance.Dni_Caducity_String = CurrentCustomer.GetDocumentExpiryDate;
        UIManager.Instance.Dni_District_String = CurrentCustomer.GetDistrictNumber.ToString();
        UIManager.Instance.Dni_Implants_Name_String = "";
        UIManager.Instance.Dni_Implants_number_String = "";
        foreach (Implant i in CurrentCustomer.GetImplants)
        {
            UIManager.Instance.Dni_Implants_Name_String += i.ImplantName + "\n";
            UIManager.Instance.Dni_Implants_number_String += i.ImplantManufacterNumber + "\n";
        }
        if (CurrentCustomer.GetCrimes.Count != 0)
        {
            buttonCP.SetActive(true);
        }
        else
        {
            buttonCP.SetActive(false);

        }
        UIManager.Instance.updateUI();
    }
    #endregion

    //----------DATA CHECK---------
    #region DataCheck
    private void StartDataCheck()
    {
        
    }
    private void UpdateDataCheck()
    {
        //Estara aqui hasta que no pulse Crimiinal proof o final decision
    }
    private void EndDataCheck()
    {

    }
    #endregion

    //----------CRIMINAL PROOF-----------
    #region CriminalProof
    private void StartCriminalProof()
    {
        //Pondra las cosas de UI de Criminal Proof
        tmpStartWait = Time.time;
        //UIManager.Instance.
    }
    private void UpdateCriminalProof()
    {
        //Animacion de CriminalProof
        //Al acabar:
        if (Time.time >= tmpWaitTime + tmpStartWait)
        {
            SwitchState(State.FinalDecision);
        }
        //
    }
    private void EndCriminalProof()
    {

    }
    public void ObtainCriminalProof()
    {
        buttonCP.SetActive(false);
        UIManager.Instance.ObtainCP();
        SwitchState(State.CriminalProof);
    }
    #endregion

    //---------FINAL DECISION----------
    #region FinalDecision
    private void StartFinalDecision()
    {

    }
    private void UpdateFinalDecision()
    {
        //Estara aqui hasta que no pulse final decision
    }
    private void EndFinalDecision()
    {

    }
    public void PulsedDecision(bool pass)
    {
        actualPass = pass;
        SwitchState(State.EndDialogue);
    }
    #endregion

    //--------END DIALOGUE--------
    #region EndDialogue
    private void StartEndDialogue()
    {
        tmpStartWait = Time.time;
        if (actualPass)
        {
            //Animacion y dialogos de Sí
        }
        else
        {
            //Animacion y dialogos de No
        }
    }
    private void UpdateEndDialogue()
    {
        //Animacion y dialogo resultado
        //Al acabar:
        if (Time.time >= tmpWaitTime + tmpStartWait)
        {
            _currentClientNumber++;
            if (_currentClientNumber <= maxClients)
            {
                CurrentCustomer = clientsList[_currentClientNumber - 1];
                SwitchState(State.Apparition);
            }
            else
            {
                //END LOOP
                inProgress = false;
                SwitchState(State.Waiting);
            }
        }
    }
    private void EndEndDialogue()
    {
        //Desactivar cosas
        DNIGameObject.SetActive(false);
        buttonCP.SetActive(false);
        UIManager.Instance.ResetPages();
    }
    public void ResetCustomer()
    {
        clientImage.GetComponent<Image>().sprite = null;
    }
    #endregion

    //---------WAITING----------
    #region Waiting
    private void StartWaiting()
    {

    }
    private void UpdateWaiting()
    {

    }
    private void EndWaiting()
    {

    }
    #endregion

    public bool isInProgress()
    {
        return inProgress;
    }

    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Apparition:
                EndClientApparition();
                break;
            case State.Documentation:
                EndDocumentationAsk();
                break;
            case State.DNI:
                EndObtainingDNI();
                break;
            case State.DataCheck:
                EndDataCheck();
                break;
            case State.CriminalProof:
                EndCriminalProof();
                break;
            case State.FinalDecision:
                EndFinalDecision();
                break;
            case State.EndDialogue:
                EndEndDialogue();
                break;
            case State.Waiting:
                EndWaiting();
                break;
        }

        switch (state)
        {
            case State.Apparition:
                StartClientApparition();
                break;
            case State.Documentation:
                StartDocumentationAsk();
                break;
            case State.DNI:
                StartObtainingDNI();
                break;
            case State.DataCheck:
                StartDataCheck();
                break;
            case State.CriminalProof:
                StartCriminalProof();
                break;
            case State.FinalDecision:
                StartFinalDecision();
                break;
            case State.EndDialogue:
                StartEndDialogue();
                break;
            case State.Waiting:
                StartWaiting();
                break;
        }

        currentState = state;
    }
}
