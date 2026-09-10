using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Option", menuName = "Option")]
public class Option : ScriptableObject
{
    public string optionName;
    public float optionTime;
    public string optionTextDescription;
    public bool isOptionAvailable;
    public Action action;
}
