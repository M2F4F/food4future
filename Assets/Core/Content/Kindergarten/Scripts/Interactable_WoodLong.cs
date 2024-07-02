using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable_WoodLong : ARInteractable
{
    public delegate void OnWoodLongInteraction(string message);
    public static event OnWoodLongInteraction onWoodLongInteraction;
    public override void OnClick()
    {
        onWoodLongInteraction?.Invoke("Top");
    }
}
