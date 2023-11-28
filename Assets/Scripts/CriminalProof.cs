using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Criminal Proof")]
public class CriminalProof : ScriptableObject
{
    [SerializeField] public new string name;
    [SerializeField] public Sprite image;
    [SerializeField] public int age;
    [SerializeField] public string serialNumber;
    [SerializeField] public List<string> criminalList;

    public string GetName { get { return name; } }
    public Sprite GetImage { get { return image; } }
    public int GetAge { get { return age; } }
    public string GetSerialNumber { get { return serialNumber; } }
    public List<string> GetCriminalList { get { return criminalList; } }
}
