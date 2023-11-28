using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightProgress : MonoBehaviour
{

    [SerializeField] private GameObject clientImage;
    [SerializeField] private GameObject dialogGameObject;
    [SerializeField] private GameObject DNIGameObject;

    private int _currentClientNumber;

    private Customer currentCustomer;

    private bool actualPass;

    private int maxClients;
    private List<Customer> clientsList;


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

    // Start is called before the first frame update
    void Start()
    {
        if (clientsList.Count != 0)
        {
            if (clientImage.GetComponent<Sprite>())
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
        else
        {
            Debug.Log("Clients List Empty");
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
        currentCustomer = clientsList[_currentClientNumber - 1];
        Sprite photo = clientImage.GetComponent<Sprite>();
        photo = currentCustomer.GetPhoto;

    }
    private void UpdateClientApparition()
    {
        //Animaión de la aparición
        //Al acabar:
        //SwitchState(State.Documentation);
    }
    private void EndClientApparition()
    {

    }
    public void StartLoop(int maxCustomers, List<Customer> customersList)
    {
        maxClients = maxCustomers;
        _currentClientNumber = 1;
        clientsList = customersList;
        SwitchState(State.Apparition);
    }
    #endregion

    //----------DOCUMENTATION ASK----------
    #region Documentation
    private void StartDocumentationAsk()
    {

    }
    private void UpdateDocumentationAsk()
    {
        //Animaión de la documentación y dialogos
        //Al acabar:
        //SwitchState(State.DNI);
    }
    private void EndDocumentationAsk()
    {

    }
    #endregion

    //---------OBTAINING DNI----------
    #region DNI
    private void StartObtainingDNI()
    {
        if (currentCustomer.GetDNIToGive)
        {
            //Setear la nueva info de DNI
            DNIGameObject.SetActive(true);
        }
    }
    private void UpdateObtainingDNI()
    {
        //Animacion de DNI
        //Al acabar:
        //SwitchState(State.DataCheck);
    }
    private void EndObtainingDNI()
    {

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
    }
    private void UpdateCriminalProof()
    {
        //Animacion de CriminalProof
        //Al acabar:
        //SwitchState(State.FinalDecision)
    }
    private void EndCriminalProof()
    {

    }
    public void obtainCriminalProof()
    {
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
    public void pulsedDecision(bool pass)
    {
        actualPass = pass;
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
        if (_currentClientNumber <= maxClients)
        {
            _currentClientNumber++;
            currentCustomer = clientsList[_currentClientNumber - 1];
            SwitchState(State.Apparition);
        }
        else
        {
            //END LOOP
            SwitchState(State.Waiting);
        }
    }
    private void EndEndDialogue()
    {
        //Desactivar cosas
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
