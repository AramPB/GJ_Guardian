using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DialogType{Beg, Bribe, Normal};


[CreateAssetMenu(fileName = "Customers", menuName = "ScriptableObjects/Customers", order = 1)]
public class Customer : ScriptableObject
{
    [SerializeField] private string customerName;
    [SerializeField] private string customerId;
    [SerializeField] private Sprite customerPhoto;
    [SerializeField] private int customerAge;
    [SerializeField] private string customerDistricNumber;
    [SerializeField] private bool customerDNIToGive;
    [SerializeField] private Sprite customerSprite;
    [SerializeField] private List<string> customerCrimes;
    [SerializeField] private List<string> customerDialogLines;
    [SerializeField] private DialogType customerDialogType;
    [SerializeField] private int customerMoney;
    [SerializeField] private bool customerNightApparition;
    [SerializeField] private List<Implant> customerImplants;
    [SerializeField] private List<Implant> customerImplantsRegistered;
    [SerializeField] private string customerDocumentExpiryDate;


    public string GetName { get { return customerName; } }
    public string GetId { get { return customerId; } }
    public Sprite GetPhoto { get { return customerPhoto; } }
    public int GetAge { get { return customerAge; } }
    public string GetDistrictNumber { get { return customerDistricNumber; } }
    public bool GetDNIToGive { get { return customerDNIToGive; } }
    public Sprite GetSprite { get { return customerSprite; } }
    public List<string> GetCrimes { get { return customerCrimes; } }
    public List<string> GetDialogLines { get { return customerDialogLines; } }
    public DialogType GetDialogType { get { return customerDialogType; } }
    public int GetMoney { get { return customerMoney; } }
    public bool GetNightApparition { get { return customerNightApparition; } }
    public List<Implant> GetImplants { get { return customerImplants; } }
    public List<Implant> GetImplantsRegistered { get { return customerImplantsRegistered; } }
    public string GetDocumentExpiryDate { get { return customerDocumentExpiryDate; } }

}
