using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NightSpecifications", menuName = "ScriptableObjects/NightSpecifications", order = 4)]
public class NightSpecifications : ScriptableObject
{
    //Distric Limitations
    [SerializeField] private List<int> specificationDistricNumber;

    //Implants Restrictions
    [SerializeField] private bool specificationRegisteredImplants;
    [SerializeField] private bool specificationUnregisteredImplants;
    [SerializeField] private bool specificationNoImplants;
    [SerializeField] private bool specificationIlegalImplants;

    //Age Restriction
    [SerializeField] private int specificationMinimumAge;
    
    //Legal Restrictions
    [SerializeField] private bool specificationNoCrime;
    [SerializeField] private bool specificationJustifiedCrime;
    [SerializeField] private bool specificationUnjustifiedCrime;

    //Permited Crimes
    [SerializeField] private List<Crimes> specificationsPermitedCrimes;

    public List<int> SpecificationDistricNumber { get => specificationDistricNumber; set => specificationDistricNumber = value; }
    public bool SpecificationRegisteredImplants { get => specificationRegisteredImplants; set => specificationRegisteredImplants = value; }
    public bool SpecificationUnregisteredImplants { get => specificationUnregisteredImplants; set => specificationUnregisteredImplants = value; }
    public bool SpecificationNoImplants { get => specificationNoImplants; set => specificationNoImplants = value; }
    public bool SpecificationIlegalImplants { get => specificationIlegalImplants; set => specificationIlegalImplants = value; }
    public int SpecificationMinimumAge { get => specificationMinimumAge; set => specificationMinimumAge = value; }
    public bool SpecificationNoCrime { get => specificationNoCrime; set => specificationNoCrime = value; }
    public bool SpecificationJustifiedCrime { get => specificationJustifiedCrime; set => specificationJustifiedCrime = value; }
    public bool SpecificationUnjustifiedCrime { get => specificationUnjustifiedCrime; set => specificationUnjustifiedCrime = value; }
    public List<Crimes> SpecificationsPermitedCrimes { get => specificationsPermitedCrimes; set => specificationsPermitedCrimes = value; }
}
