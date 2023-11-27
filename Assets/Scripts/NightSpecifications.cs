using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NightSpecifications", menuName = "ScriptableObjects/NiightSpecifications", order = 4)]
public class NightSpecifications : ScriptableObject
{
    //Distric Limitations
    [SerializeField] private List<int> specificationDistricNumber;

    //Implants Restrictions
    [SerializeField] private bool specificationRegisteredImplants;
    [SerializeField] private bool specificationUnregisteredImplants;
    [SerializeField] private bool specificationNoImplants;
    [SerializeField] private bool specificationIlegalImplants;

    //People Restrictions
    [SerializeField] private bool specificationModifiedPeople;
    [SerializeField] private bool specificationVanilaPeople;

    //Age Restriction
    [SerializeField] private int specificationMinimumAge;
    
    //Legal Restrictions
    [SerializeField] private bool specificationNoCrime;
    [SerializeField] private bool specificationJustifiedCrime;
    [SerializeField] private bool specificationUnjustifiedCrime;

    //Permited Crimes
    [SerializeField] private List<string> specificationsPermitedCrimes;

    public List<int> SpecificationDistricNumber { get => specificationDistricNumber; set => specificationDistricNumber = value; }
    public bool SpecificationRegisteredImplants { get => specificationRegisteredImplants; set => specificationRegisteredImplants = value; }
    public bool SpecificationUnregisteredImplants { get => specificationUnregisteredImplants; set => specificationUnregisteredImplants = value; }
    public bool SpecificationNoImplants { get => specificationNoImplants; set => specificationNoImplants = value; }
    public bool SpecificationIlegalImplants { get => specificationIlegalImplants; set => specificationIlegalImplants = value; }
    public bool SpecificationModifiedPeople { get => specificationModifiedPeople; set => specificationModifiedPeople = value; }
    public bool SpecificationVanilaPeople { get => specificationVanilaPeople; set => specificationVanilaPeople = value; }
    public int SpecificationMinimumAge { get => specificationMinimumAge; set => specificationMinimumAge = value; }
    public bool SpecificationNoCrime { get => specificationNoCrime; set => specificationNoCrime = value; }
    public bool SpecificationJustifiedCrime { get => specificationJustifiedCrime; set => specificationJustifiedCrime = value; }
    public bool SpecificationUnjustifiedCrime { get => specificationUnjustifiedCrime; set => specificationUnjustifiedCrime = value; }
    public List<string> SpecificationsPermitedCrimes { get => specificationsPermitedCrimes; set => specificationsPermitedCrimes = value; }
}
