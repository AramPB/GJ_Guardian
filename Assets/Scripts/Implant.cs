using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImplantType { Eye, Cranium, Jaw, Nose, Arm, Torso};


[CreateAssetMenu(fileName = "Implants", menuName = "ScriptableObjects/Implants", order = 2)]
public class Implant : ScriptableObject
{
    [SerializeField] private string implantName;
    [SerializeField] private string implantManufacterNumber;
    [SerializeField] private bool isLegal;
    [SerializeField] private ImplantType implantType;

    public string ImplantName { get => implantName; set => implantName = value; }
    public string ImplantManufacterNumber { get => implantManufacterNumber; set => implantManufacterNumber = value; }
    public bool IsLegal { get => isLegal; set => isLegal = value; }
    public ImplantType ImplantType { get => implantType; set => implantType = value; }
}
