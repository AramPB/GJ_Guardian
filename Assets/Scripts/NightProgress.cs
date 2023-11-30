using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightProgress : MonoBehaviour
{
    [SerializeField] private GameObject DNIGameObject;
    [SerializeField] private GameObject docGO;
    [SerializeField] private GameObject scannerGO;
    [SerializeField] private GameObject acceptButton;
    [SerializeField] private GameObject declineButton;
    [SerializeField] private GameObject buttonCP;
    
    [SerializeField] private float tmpWaitTime;

    private float tmpStartWait;

    private int _currentClientNumber;

    private Customer currentCustomer;
    private GameObject instantiatedCustomer;

    private bool actualPass;

    private int maxClients;
    private List<Customer> clientsList;

    private bool inProgress = false;
    private bool inTransition = false;
    private bool transitionHasToEnd = false;
    private bool isInInspect = false;


    private enum State
    {
        Apparition,
        Documentation,
        DNI,
        DataCheck,
        CriminalProof,
        FinalDecision,
        EndDialogue,
        Transition,
        Waiting
    }
    private State currentState = State.Waiting;

    public Customer CurrentCustomer { get => currentCustomer; set => currentCustomer = value; }

    // Start is called before the first frame update
    void Start()
    {



       SwitchState(State.Waiting);


    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.scannerController)
        {
            if (!isInInspect)
            {
                UIManager.Instance.scannerController.changeButtonVisibility(false);
            }
        }

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
            //Transicion entre clientes
            case State.Transition:
                UpdateTransition();
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
        Debug.Log("NEW CLIENT!!");
        if (NightSystem.Instance.MusicController.State == "Normal") {
            NightSystem.Instance.MusicController.startFilteredMusic();
            SoundsController.Instance.closeDoorSoundPlay();
        }
        CurrentCustomer = clientsList[_currentClientNumber - 1];
        instantiatedCustomer = Instantiate(currentCustomer.GetCustomerPrefab, NightSystem.Instance.CharacterContainer.transform);
        UIManager.Instance.scannerController.hideScannerUI();

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
            docGO.SetActive(false);
            scannerGO.SetActive(false);
            acceptButton.SetActive(false);
            declineButton.SetActive(false);
            buttonCP.SetActive(false);
            isInInspect = false;
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
        DialogManager.Instance.SetLines(currentCustomer.GetDialogLines);
        DialogManager.Instance.startDialogLines();
    }
    private void UpdateDocumentationAsk()
    {
        //Animaión de la documentación y dialogos
        //Al acabar:
        if (DialogManager.Instance.hasEnded)
        {
            SwitchState(State.DNI);
        }
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
            isInInspect = true;
            UIManager.Instance.scannerController.changeButtonVisibility(true);
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
        docGO.SetActive(true);
        acceptButton.SetActive(true);
        declineButton.SetActive(true);
        UIManager.Instance.Dni_Age_String = CurrentCustomer.GetAge.ToString();
        UIManager.Instance.Dni_Name_String = CurrentCustomer.GetName;
        UIManager.Instance.Dni_Serial_String = CurrentCustomer.GetId;
        UIManager.Instance.Dni_Foto_Sprite = CurrentCustomer.GetPhoto;
        UIManager.Instance.Dni_Caducity_String = CurrentCustomer.GetDocumentExpiryDate;
        UIManager.Instance.Dni_District_String = CurrentCustomer.GetDistrictNumber.ToString();
        UIManager.Instance.Dni_Implants_Name_String = "";
        UIManager.Instance.Dni_Implants_number_String = "";
        foreach (Implant i in CurrentCustomer.GetImplantsRegistered)
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
        UIManager.Instance.Crimes_Age_String = currentCustomer.GetAge.ToString();
        UIManager.Instance.Crimes_Name_String = currentCustomer.GetName;
        UIManager.Instance.Crimes_Serial_String = currentCustomer.GetId;
        UIManager.Instance.Crimes_Foto_Sprite = CurrentCustomer.GetPhoto;
        string crimesString = "";
        foreach (var crime in currentCustomer.GetCrimes)
        {
            crimesString += "- " + crime.CrimeName + ": " + crime.CrimeDescription + " " + "\n- Condena: " + crime.CrimeSentenceTime + " años\n\n";
        }
        UIManager.Instance.Crimes_String = crimesString;
        UIManager.Instance.updateUI();


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
        SoundsController.Instance.criminalSoundPlay();
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
        bool isApt = NightSystem.Instance.CustomerContol.ControlOneCustomer(currentCustomer);
        DNIGameObject.SetActive(false);
        docGO.SetActive(false);
        scannerGO.SetActive(false);
        acceptButton.SetActive(false);
        declineButton.SetActive(false);
        isInInspect = false;

        if (actualPass == isApt)
        {
            //Debug.Log("Acertaste");
            NightSystem.Instance.CurrentNight.Successes++;
        }
        else
        {
            //Debug.Log("Cagaste");
            NightSystem.Instance.CurrentNight.Fails++;
        }
        SwitchState(State.EndDialogue);
    }
    #endregion

    //--------END DIALOGUE--------
    #region EndDialogue
    private void StartEndDialogue()
    {

        if (actualPass)
        {
            //Animacion y dialogos de Sí
            SoundsController.Instance.acceptSoundPlay();
            if (NightSystem.Instance.MusicController.State == "Filtered")
            {
                NightSystem.Instance.MusicController.startNormalMusic();
                SoundsController.Instance.openDoorSoundPlay();
            }
            DialogManager.Instance.SetLines(currentCustomer.GetAcceptDialogLines);
            DialogManager.Instance.startDialogLines();
            if (currentCustomer.GetDialogType.Equals(DialogType.Beg))
            {
                NightSystem.Instance.ModifyMoneyNight(-currentCustomer.GetMoney);
            }
            if (currentCustomer.GetDialogType.Equals(DialogType.Bribe))
            {
                NightSystem.Instance.ModifyMoneyNight(+currentCustomer.GetMoney);
            }
            QueueController.Instance.AcceptCustomer();
        }
        else
        {
            //Animacion y dialogos de No
            SoundsController.Instance.declineSoundPlay();
            DialogManager.Instance.SetLines(currentCustomer.GetDeclineDialogLines);
            DialogManager.Instance.startDialogLines();
            QueueController.Instance.DeclineCustomer();

        }
    }
    private void UpdateEndDialogue()
    {
        //Animacion y dialogo resultado
        //Al acabar:
        if (DialogManager.Instance.hasEnded)
        {
            _currentClientNumber++;
            if (_currentClientNumber <= maxClients)
            {
                CurrentCustomer = clientsList[_currentClientNumber - 1];
                SwitchState(State.Transition);
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
        docGO.SetActive(false);
        scannerGO.SetActive(false);
        acceptButton.SetActive(false);
        declineButton.SetActive(false);
        buttonCP.SetActive(false);
        isInInspect = false;
        UIManager.Instance.ResetPages();
        UIManager.Instance.updateUI();
    }
    public void ResetCustomer()
    {

    }
    #endregion

    //--------TRANSITION-----
    #region Transition
    private void StartTransition()
    {
        inTransition = true;
        transitionHasToEnd = false;
    }
    private void UpdateTransition()
    {
        if (transitionHasToEnd)
        {

            SwitchState(State.Apparition);

        }
    }
    private void EndTransition()
    {
        inTransition = false;
    }
    public void InMiddleTransition()
    {
        //TODO: BORRAR CLIENTE (imagen y botones)
        Destroy(instantiatedCustomer);
    }
    public void TransitionHasToEnd()
    {
        transitionHasToEnd = true;
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

    public bool IsInProgress()
    {
        return inProgress;
    }

    public bool IsInTransition()
    {
        return inTransition;
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
            case State.Transition:
                EndTransition();
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
            case State.Transition:
                StartTransition();
                break;
            case State.Waiting:
                StartWaiting();
                break;
        }

        currentState = state;
    }
}
