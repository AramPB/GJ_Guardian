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
    [SerializeField] private string customerAge;
    [SerializeField] private string customerDistricNumber;
    [SerializeField] private Sprite customerSprite;
    [SerializeField] private List<string> customerCrimes;
    [SerializeField] private List<string> customerDialogLines;
    [SerializeField] private DialogType customerDialogType;
    [SerializeField] private int customerMoney;
    [SerializeField] private bool customerNightApparition;
    [SerializeField] private List<Implant> customerImplants;
    [SerializeField] private List<Implant> customerImplantsRegistered;
    [SerializeField] private string customerDocumentExpiryDate;
}
