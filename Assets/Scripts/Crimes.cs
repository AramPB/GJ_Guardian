using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Crimes", menuName = "ScriptableObjects/Crimes", order = 6)]
public class Crimes : ScriptableObject
{
    [SerializeField] private string crimeName;
    [SerializeField] private string crimeDescription;
    [SerializeField] private string crimeSentenceTime;

    public string CrimeName { get => crimeName; set => crimeName = value; }
    public string CrimeDescription { get => crimeDescription; set => crimeDescription = value; }
    public string CrimeSentenceTime { get => crimeSentenceTime; set => crimeSentenceTime = value; }
}
