using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DialogType { Beg, Bribe, Normal };


[CreateAssetMenu(fileName = "Customers", menuName = "ScriptableObjects/Customers", order = 1)]
public class Customer : ScriptableObject
{
    [SerializeField] private string customerName;
    [SerializeField] private string customerId;
    [SerializeField] private Sprite customerPhoto;
    [SerializeField] private int customerAge;
    [SerializeField] private int customerDistricNumber;
    [SerializeField] private bool customerDNIToGive;
    [SerializeField] private Sprite customerSprite;
    [SerializeField] private List<Crimes> customerCrimes;
    [SerializeField] private List<string> customerDialogLines;
    [SerializeField] private List<string> customerAcceptDialogLines;
    [SerializeField] private List<string> customerDeclineDialogLines;
    [SerializeField] private DialogType customerDialogType;
    [SerializeField] private int customerMoney;
    [SerializeField] private bool customerNightApparition;
    [SerializeField] private List<Implant> customerImplants;
    [SerializeField] private List<Implant> customerImplantsRegistered;
    [SerializeField] private string customerDocumentExpiryDate;
    [SerializeField] private bool customerHasJustificant;


    public string GetName { get { return customerName; } }
    public string GetId { get { return customerId; } }
    public Sprite GetPhoto { get { return customerPhoto; } }
    public int GetAge { get { return customerAge; } }
    public int GetDistrictNumber { get { return customerDistricNumber; } }
    public bool GetDNIToGive { get { return customerDNIToGive; } }
    public Sprite GetSprite { get { return customerSprite; } }
    public List<Crimes> GetCrimes { get { return customerCrimes; } }
    public List<string> GetDialogLines { get { return customerDialogLines; } }
    public List<string> GetAcceptDialogLines { get { return customerAcceptDialogLines; } }
    public List<string> GetDeclineDialogLines { get { return customerDeclineDialogLines; } }
    public DialogType GetDialogType { get { return customerDialogType; } }
    public int GetMoney { get { return customerMoney; } }
    public bool GetNightApparition { get { return customerNightApparition; } }
    public List<Implant> GetImplants { get { return customerImplants; } }
    public List<Implant> GetImplantsRegistered { get { return customerImplantsRegistered; } }
    public string GetDocumentExpiryDate { get { return customerDocumentExpiryDate; } }
    public bool CustomerHasJustificant { get => customerHasJustificant; set => customerHasJustificant = value; }
    public List<string> CustomerDeclineDialogLines { get => customerDeclineDialogLines; set => customerDeclineDialogLines = value; }
    public List<string> CustomerAcceptDialogLines { get => customerAcceptDialogLines; set => customerAcceptDialogLines = value; }
}
