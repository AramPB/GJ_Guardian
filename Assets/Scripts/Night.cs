using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Nights", menuName = "ScriptableObjects/Nights", order = 3)]

public class Night : ScriptableObject
{
    [SerializeField] private int nightNumber;
    [SerializeField] private List<Customer> nightsCustomers;
    [SerializeField] private NightSpecifications nightSpecifications;
    [SerializeField] private int successes;
    [SerializeField] private int fails;

    public int NightNumber { get => nightNumber; set => nightNumber = value; }
    public List<Customer> NightsCustomers { get => nightsCustomers; set => nightsCustomers = value; }
    public NightSpecifications NightSpecifications { get => nightSpecifications; set => nightSpecifications = value; }
    public int Fails { get => fails; set => fails = value; }
    public int Successes { get => successes; set => successes = value; }
}
