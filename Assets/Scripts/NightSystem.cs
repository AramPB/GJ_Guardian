using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class NightSystem : MonoBehaviour
{
    [SerializeField] private List<string> reflectionDialogLines;

    [SerializeField] private MusicController musicController;
    [SerializeField] private TextMeshPro nightDialog;

    [SerializeField] private string currentDate;

    [SerializeField] private GameObject uiQueue;
    [SerializeField] private GameObject uiInspect;
    [SerializeField] private GameObject characterContainer;
    [SerializeField] private GameObject uiCity;

    [SerializeField] private Image fadeOut;

    [SerializeField] private Night night1;
    [SerializeField] private Night night2;
    [SerializeField] private Night night3;
    [SerializeField] private Night night4;
    [SerializeField] private Night night5;

    [SerializeField] private Night currentNight;
    [SerializeField] private int currentNightNumber;
    [SerializeField] private NightProgress nightProgress;

    [SerializeField] private CustomerControl customerContol;

    [SerializeField] private int goalMoney;
    [SerializeField] private int moneyEarned;
    private int moneyEarnedThisNight;

    private float firstTransStartTime;
    private float firstTransStartTime2;
    private float transitionStartTime;
    private float transitionStartTime2;
    private float transitionClientStartTime;
    private float transitionClientStartTime2;
    [SerializeField] private float firstTransTime;
    [SerializeField] private float firstTransTime2;
    [SerializeField] private float transitionClientTime;
    [SerializeField] private float transitionClientTime2;
    [SerializeField] private float transitionDuration;
    [SerializeField] private float transitionDuration2;
    [SerializeField] private float cityDuration;
    [SerializeField] private float queueDuration;
    private bool endLoopTrigger = false;
    private bool endLoopTrigger2 = false;
    private bool endLoopTrigger3 = false;
    private bool dialogueTrigger = false;
    private bool dialogueTrigger2 = false;
    private bool isEndGame = false;
    private bool endLoopGame = false;

    private string endGameMessage;

    public Implant currentSelectedImplant;
    public GameObject scanner;

    public TextMeshPro NightDialog { get => NightDialog; set => NightDialog = value; }
    public GameObject UIInspect { get => uiInspect; set => uiInspect = value; }
    public GameObject UIQueue { get => UIQueue; set => UIQueue = value; }
    public Night Night1 { get => night1; set => night1 = value; }
    public Night Night2 { get => night2; set => night2 = value; }
    public Night Night3 { get => night3; set => night3 = value; }
    public Night Night4 { get => night4; set => night4 = value; }
    public Night Night5 { get => night5; set => night5 = value; }
    public TextMeshPro NightDialog1 { get => nightDialog; set => nightDialog = value; }
    public Night CurrentNight { get => currentNight; set => currentNight = value; }
    public string CurrentDate { get => currentDate; set => currentDate = value; }

    public static NightSystem Instance { get; private set; }
    public NightProgress NightProgress { get => nightProgress; set => nightProgress = value; }
    public CustomerControl CustomerContol { get => customerContol; set => customerContol = value; }
    public int MoneyEarned { get => moneyEarned; set => moneyEarned = value; }
    public GameObject CharacterContainer { get => characterContainer; set => characterContainer = value; }
    public MusicController MusicController { get => musicController; set => musicController = value; }

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



    private void Start()
    {
        CurrentNight = night1;
        currentNightNumber = 0;
        currentDate = "27/11/2076";
        firstTransStartTime = Time.time;
        endLoopTrigger3 = true;
        moneyEarned = 0;
        isEndGame = false;
        endGameMessage = "";
        endLoopGame = false;
        goalMoney = 1000;
    }
    private void Update()
    {
        if (!endLoopGame) {
            if (currentNightNumber != 0) {
                if (NightProgress.IsInProgress())
                {
                    transitionStartTime = Time.time;
                    endLoopTrigger = true;
                    endLoopTrigger2 = true;

                    if (NightProgress.IsInTransition())
                    {
                        BetweenCustomersTransitionController();
                    }
                    else
                    {
                        transitionClientStartTime = Time.time;
                        endLoopTrigger3 = true;
                    }
                }
                else
                {
                    //Despues de terminar una noche, hace el sistema de transición
                    BetweenNightsTransitionController();
                }
            }
            else
            {
                FirstTransitionControler();

            }
        }
        else
        {
            //estaria acabat el joc
        }
    }

    public void ModifyMoneyNight(int newMoney)
    {
        moneyEarned += newMoney;
    }

    public int NightResume()
    {
        //Debug.Log("RESUME");
        currentNightNumber++; //endGame?

        //Player Goal = 1000€
        moneyEarnedThisNight = 60 * currentNight.Successes - 10 * currentNight.Fails;
        moneyEarned += moneyEarnedThisNight;

        if (currentNightNumber >= 6)
        {
            endGame();
            return 0;
        }

        return CurrentNight.NightNumber;

    }

    private void endGame()
    {
        isEndGame = true;
        dialogueTrigger2 = true;
    }

    private string mapDistrictName(int districtNumber){
        switch (districtNumber)
        {
            case 1:
                return "Valle Quantico";
            case 2:
                return "Liks";
            case 3:
                return "Cromolit";
            case 4:
                return "Kabuki";
            case 5:
                return "F-Central";
            case 6:
                return "Hoster";
            default:
                return "";
        }
    }

    private void formatNightSpecifications()
    {
        String formattedString = "";
        String permitedCrimes = "";

        if (currentNight != null && currentNight.NightSpecifications != null && currentNight.NightSpecifications.SpecificationDistricNumber != null && currentNight.NightSpecifications.SpecificationDistricNumber.Count > 0)
        {
            foreach (var district in currentNight.NightSpecifications.SpecificationDistricNumber)
            {
                if (district < 7)
                {
                    formattedString += "- Personas del distrito " + mapDistrictName(district);
                    formattedString += "\n";
                }
            }
        }
        if (currentNight != null && currentNight.NightSpecifications != null && !currentNight.NightSpecifications.SpecificationRegisteredImplants)
        {
            formattedString += "- Personas CON implantes registrados";
            formattedString += "\n";
        }
        if (currentNight != null && currentNight.NightSpecifications != null && !currentNight.NightSpecifications.SpecificationUnregisteredImplants)
        {
            formattedString += "- Personas SIN implantes registrados";
            formattedString += "\n";
        }
        if (currentNight != null && currentNight.NightSpecifications != null && !currentNight.NightSpecifications.SpecificationNoImplants)
        {
            formattedString += "- Personas SIN implantes";
            formattedString += "\n";
        }
        if (currentNight != null && currentNight.NightSpecifications != null && !currentNight.NightSpecifications.SpecificationIlegalImplants)
        {
            formattedString += "- Personas con implantes ILEGALES";
            formattedString += "\n";
        }
        if (currentNight != null && currentNight.NightSpecifications != null && currentNight.NightSpecifications.SpecificationMinimumAge != 0)
        {
            formattedString += "- Personas MENORES de " + currentNight.NightSpecifications.SpecificationMinimumAge;
            formattedString += "\n";
        }
        if (currentNight != null && currentNight.NightSpecifications != null && !currentNight.NightSpecifications.SpecificationNoCrime)
        {
            formattedString += "- Gente SIN crimenes";
            formattedString += "\n";
        }
        if (currentNight != null && currentNight.NightSpecifications != null && !currentNight.NightSpecifications.SpecificationJustifiedCrime)
        {
            formattedString += "- Gente CON justificante penal";
            formattedString += "\n";
        }
        if (currentNight != null && currentNight.NightSpecifications != null && !currentNight.NightSpecifications.SpecificationUnjustifiedCrime)
        {
            formattedString += "- Gente SIN justificante penal";
            formattedString += "\n";
        }
        if (currentNight != null && currentNight.NightSpecifications != null && currentNight.NightSpecifications.SpecificationsPermitedCrimes != null && currentNight.NightSpecifications.SpecificationsPermitedCrimes.Count > 0)
        {
            foreach (var permitedCrime in currentNight.NightSpecifications.SpecificationsPermitedCrimes)
            {
                permitedCrimes += "- " + permitedCrime.CrimeName;
                permitedCrimes += "\n";
            }
        }
        if(formattedString == "")
        {
            formattedString = "Sin información disponible!";
        }
        if (permitedCrimes == "")
        {
            permitedCrimes = "No se permite ningún crimen!";
        }
        UIManager.Instance.Restriction_String = formattedString;
        UIManager.Instance.PermitedCrimes_String = permitedCrimes;
        UIManager.Instance.updateUI();
    }

    private void NextNight()
    {
        if (currentNightNumber == 1)
        {
            CurrentDate = "27/11/2076";
            CurrentNight = night1;
        }
        else if (currentNightNumber == 2)
        {
            CurrentDate = "28/11/2076";
            CurrentNight = night2;
        }
        else if (currentNightNumber == 3)
        {
            CurrentDate = "29/11/2076";
            CurrentNight = night3;
        }
        else if (currentNightNumber == 4)
        {
            CurrentDate = "30/11/2076";
            CurrentNight = night4;
        }
        else if(currentNightNumber == 5)
        {
            CurrentDate = "1/12/2076";
            CurrentNight = night5;
        }
        currentNight.Successes = 0;
        currentNight.Fails = 0;
        CustomerContol.UpdateCurrentNight(currentNight);

        nightProgress.StartLoop(CurrentNight.NightsCustomers.Count, CurrentNight.NightsCustomers);
        formatNightSpecifications();
    }

    #region NightsTransition
    private void BetweenNightsTransitionController()
    {
        if (endLoopTrigger)
        {
            int a = NightResume();
            endLoopTrigger = false;
            dialogueTrigger = true;
            reflectionDialogLines.Clear();
        }
        NightTransition();
        if (Time.time >= transitionStartTime + transitionDuration)
        {
            //city
            if (dialogueTrigger)
            {
                reflectionDialogLines.Add("Dinero ganado esta noche: " + moneyEarnedThisNight);
                reflectionDialogLines.Add("Dinero total: " + moneyEarned);
                DialogManager.Instance.SetLines(reflectionDialogLines);
                DialogManager.Instance.startDialogLines();
                dialogueTrigger = false;
            }

            if (DialogManager.Instance.hasEnded)
            {

                //nextnight
                if (endLoopTrigger2)
                {
                    MusicController.Instance.ChangeSong(currentNightNumber);
                    transitionStartTime2 = Time.time;
                    endLoopTrigger2 = false;
                    reflectionDialogLines.Clear();
                    nightProgress.InMiddleTransition();
                }
                NextNightTransition();
                if (Time.time >= transitionStartTime2 + transitionDuration2)
                {
                    if (!isEndGame) {
                        NextNight();
                    }
                    else
                    {
                        //Dialeg final
                        Debug.Log("!!!FINAL!!!");
                        if (dialogueTrigger2) {
                            Debug.Log("Trigger");
                            if (moneyEarned >= goalMoney)
                            {
                                Debug.Log("Good");

                                //Play Good Ending Animation & Return to Menu
                                reflectionDialogLines.Add("Al final, he conseguido el dinero para enviar a Spark a la universidad...");
                                reflectionDialogLines.Add("Al menos espero que que esto le sirva para tener un futuro brillante.");
                                reflectionDialogLines.Add("Con estos " + moneyEarned + "€ Spark podrá vivir tranquila al menos un tiempo, parece ser que mis días de portero han acabado.");
                                reflectionDialogLines.Add("Al menos de momento…");
                            }
                            else
                            {
                                Debug.Log("Bad");

                                //Play Bad Ending Animation & Return to Menu
                                reflectionDialogLines.Add("Mierda, solo con " + moneyEarned + "€ no me da para pagar la universidad a Spark.");
                                reflectionDialogLines.Add("Joder solo si hubiera sido mejor, o estado más atento...");
                                reflectionDialogLines.Add("Mierda, Mierda, no quiero condenarla a esta vida, rebuscando por las esquinas cualquier trabajo...");
                                reflectionDialogLines.Add("Parece ser que aun no puedo descansar...");
                            }
                            DialogManager.Instance.SetLines(reflectionDialogLines);
                            DialogManager.Instance.startDialogLines();
                            dialogueTrigger2 = false;
                        }
                        if (DialogManager.Instance.hasEnded)
                        {
                            Debug.Log("Scene");
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
                            endLoopGame = true;
                        }
                    }
                }
            }

        }
    }

    private void NightTransition()
    {
        Color aux = fadeOut.color;
        fadeOut.gameObject.SetActive(true);

        if (Time.time >= transitionStartTime + transitionDuration / 2)
        {
            if (Time.time >= transitionStartTime + transitionDuration)
            {
                fadeOut.gameObject.SetActive(false);
            }
            else
            {
                aux.a = LerpFunction.Lerp(1, 0, transitionStartTime + transitionDuration / 2, transitionDuration / 2);

                UIManager.Instance.ActivateInspectUI(false);
                nightProgress.ResetCustomer();
                UIManager.Instance.ActivateCityUI(true);

            }
        }
        else
        {
            aux.a = LerpFunction.Lerp(0, 1, transitionStartTime, transitionDuration / 2);
        }
        fadeOut.color = aux;
    }

    private void NextNightTransition()
    {
        Color aux = fadeOut.color;
        fadeOut.gameObject.SetActive(true);

        if (Time.time >= transitionStartTime2 + transitionDuration2 / 2)
        {
            if (Time.time >= transitionStartTime2 + transitionDuration2)
            {
                fadeOut.gameObject.SetActive(false);
            }
            else
            {
                aux.a = LerpFunction.Lerp(1, 0, transitionStartTime2 + transitionDuration2 / 2, transitionDuration2 / 2);
                if (!isEndGame) {
                    UIManager.Instance.ActivateInspectUI(true);
                    UIManager.Instance.ActivateCityUI(false);
                }
                else
                {
                    //Activar final
                    if (moneyEarned >= 1000)
                    {
                        UIManager.Instance.ActivateGameOverUI(true);
                    }
                    else
                    {
                        UIManager.Instance.ActivateGameOverUI(false);
                    }
                }
            }
        }
        else
        {
            aux.a = LerpFunction.Lerp(0, 1, transitionStartTime2, transitionDuration2 / 2);
        }
        fadeOut.color = aux;


    }
    #endregion

    #region CustomerTransition
    private void BetweenCustomersTransitionController()
    {
        CustomerTransition();
        if (Time.time >= transitionClientStartTime + transitionClientTime)
        {
            //Queue
            if (Time.time >= transitionClientStartTime + transitionClientTime + queueDuration)
            {
                //nextcustomer
                if (endLoopTrigger3)
                {
                    transitionClientStartTime2 = Time.time;
                    endLoopTrigger3 = false;
                    nightProgress.InMiddleTransition();
                }
                NextCustomerTransition();
                if (Time.time >= transitionClientStartTime2 + transitionClientTime2)
                {
                    nightProgress.TransitionHasToEnd();
                }
            }

        }
    }
    private void CustomerTransition()
    {
        Color aux = fadeOut.color;
        fadeOut.gameObject.SetActive(true);

        if (Time.time >= transitionClientStartTime + transitionClientTime / 2)
        {
            if (Time.time >= transitionClientStartTime + transitionClientTime)
            {
                fadeOut.gameObject.SetActive(false);
            }
            else
            {
                aux.a = LerpFunction.Lerp(1, 0, transitionClientStartTime + transitionClientTime / 2, transitionClientTime / 2);

                UIManager.Instance.ActivateInspectUI(false);
                nightProgress.ResetCustomer();
                UIManager.Instance.ActivateQueueUI(true);

            }
        }
        else
        {
            aux.a = LerpFunction.Lerp(0, 1, transitionClientStartTime, transitionClientTime / 2);
        }
        fadeOut.color = aux;
    }

    private void NextCustomerTransition()
    {
        Color aux = fadeOut.color;
        fadeOut.gameObject.SetActive(true);

        if (Time.time >= transitionClientStartTime2 + transitionClientTime2 / 2)
        {
            if (Time.time >= transitionClientStartTime2 + transitionClientTime2)
            {
                fadeOut.gameObject.SetActive(false);
            }
            else
            {
                aux.a = LerpFunction.Lerp(1, 0, transitionClientStartTime2 + transitionClientTime2 / 2, transitionClientTime2 / 2);
                UIManager.Instance.ActivateInspectUI(true);
                UIManager.Instance.ActivateQueueUI(false);
            }
        }
        else
        {
            aux.a = LerpFunction.Lerp(0, 1, transitionClientStartTime2, transitionClientTime2 / 2);
        }
        fadeOut.color = aux;


    }
    #endregion

    #region FirstTransition
    private void FirstTransitionControler()
    {
        FirstTransition();

        if (Time.time >= firstTransStartTime + firstTransTime)
        {

            //Queue

            if (Time.time >= firstTransStartTime + firstTransTime + queueDuration)
            {
                //nextcustomer
                if (endLoopTrigger3)
                {
                    firstTransStartTime2 = Time.time;
                    endLoopTrigger3 = false;
                    nightProgress.InMiddleTransition();
                }

                FirstCustomerTransition();
                if (Time.time >= firstTransStartTime2 + firstTransTime2)
                {
                    currentNightNumber++;
                    NextNight();
                }
            }
        }
    }
    private void FirstTransition()
    {
        Color aux = fadeOut.color;
        fadeOut.gameObject.SetActive(true);


        if (Time.time >= firstTransStartTime + firstTransTime)
        {
            fadeOut.gameObject.SetActive(false);
        }
        else
        {
            aux.a = LerpFunction.Lerp(1, 0, firstTransStartTime, firstTransTime);

            UIManager.Instance.ActivateInspectUI(false);
            nightProgress.ResetCustomer();
            UIManager.Instance.ActivateQueueUI(true);

        }

        fadeOut.color = aux;
    }

    private void FirstCustomerTransition()
    {
        Color aux = fadeOut.color;
        fadeOut.gameObject.SetActive(true);

        if (Time.time >= firstTransStartTime2 + firstTransTime2 / 2)
        {
            if (Time.time >= firstTransStartTime2 + firstTransTime2)
            {
                fadeOut.gameObject.SetActive(false);
            }
            else
            {
                aux.a = LerpFunction.Lerp(1, 0, firstTransStartTime2 + firstTransTime2 / 2, firstTransTime2 / 2);
                UIManager.Instance.ActivateInspectUI(true);
                UIManager.Instance.ActivateQueueUI(false);
            }
        }
        else
        {
            aux.a = LerpFunction.Lerp(0, 1, firstTransStartTime2, firstTransTime2 / 2);
        }
        fadeOut.color = aux;


    }

    #endregion

}
