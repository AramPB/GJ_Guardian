using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NightSystem : MonoBehaviour
{
    [SerializeField] private TextMeshPro nightDialog;

    [SerializeField] private GameObject uiQueue;
    [SerializeField] private GameObject uiInspect;

    [SerializeField] private Night night1;
    [SerializeField] private Night night2;
    [SerializeField] private Night night3;
    [SerializeField] private Night night4;

    [SerializeField] private Night currentNight;
    [SerializeField] private int currentNightNumber;
    [SerializeField] private NightProgress nightProgress;

    public TextMeshPro NightDialog { get => NightDialog; set => NightDialog = value; }
    public GameObject UIInspect { get => uiInspect; set => uiInspect = value; }
    public GameObject UIQueue { get => UIQueue; set => UIQueue = value; }
    public Night Night1 { get => night1; set => night1 = value; }
    public Night Night2 { get => night2; set => night2 = value; }
    public Night Night3 { get => night3; set => night3 = value; }
    public Night Night4 { get => night4; set => night4 = value; }
    public TextMeshPro NightDialog1 { get => nightDialog; set => nightDialog = value; }

    private void Start()
    {
        currentNight = night1;
        currentNightNumber = 0;
        Debug.Log("START");
    }
    private void Update()
    {
        if (currentNightNumber != 0) {
            if (nightProgress.isInProgress())
            {

            }
            else
            {
                //
                int a = NightResume(1, 1);
            }
        }
        else
        {
            currentNightNumber++;
            NextNight();
        }
    }

    public int NightResume(int successes, int fails)
    {
        Debug.Log("RESUME");
        currentNightNumber++; //endGame?
        
        //Player Goal = 1000€
        float auxiliarMoney = 50 * successes - 10 * fails;

        if (currentNightNumber >= 5)
        {
            endGame();
            return 0;
        }

        

        //Add Money to GameManager

        //NightDialog1.text = "With today's shift you have won: " + auxiliarMoney + "€";

        NextNight();
        NightTransition();

        return currentNight.NightNumber;

    

    }

    private void endGame()
    {
        //if (GameManager.money >= 1000)
        //{
        //    //Play Good Ending Animation & Return to Menu
        //}
        //else
        //{
        //    //Play Bad Ending Animation & Return to Menu
        //}
        Debug.Log("EEENDDDDDD GAMEEEEEEE");
    }

    private void NextNight()
    {
        Debug.Log("NEXT NIGHT");

        if (currentNightNumber == 1)
        {
            currentNight = night1;
        }
        else if (currentNightNumber == 2)
        {
            currentNight = night2;
        }
        else if (currentNightNumber == 3)
        {
            currentNight = night3;
        }
        else
        {
            currentNight = night4;
        }

        nightProgress.StartLoop(currentNight.NightsCustomers.Count, currentNight.NightsCustomers);
    }

    private void NightTransition()
    {

        //Play of an Animation, Animate in Unity the transition, Fade Out, Etc.
        
    }

    public void NextNightTransition()
    {

        //Play of an Animation, Animate in Unity the transition, Fade Out, Etc.

    }
}
