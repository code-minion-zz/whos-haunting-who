using UnityEngine;
using System.Collections;

[DisallowMultipleComponent]
public class Interactable : Photon.MonoBehaviour 
{
    public enum Interaction
    {
        None,
        Drag,
        Hold,
    }
    public Interaction currentInteration = Interaction.None;
    protected bool Interacting = false;
    protected Transform target;
}
