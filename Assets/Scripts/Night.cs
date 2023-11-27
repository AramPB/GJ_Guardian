using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Nights", menuName = "ScriptableObjects/Nights", order = 3)]

public class Night : ScriptableObject
{
    [SerializeField] private int nightNumber;
    [SerializeField] private List<Customer> nightsCustomers;
    [SerializeField] private List<NightSpecifications> nightSpecifications;

    public int NightNumber { get => nightNumber; set => nightNumber = value; }
    public List<Customer> NightsCustomers { get => nightsCustomers; set => nightsCustomers = value; }
    public List<NightSpecifications> NightSpecifications { get => nightSpecifications; set => nightSpecifications = value; }
}
