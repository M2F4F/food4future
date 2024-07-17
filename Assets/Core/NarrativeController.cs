using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeController : MonoBehaviour
{
    public Text narrativeText; // Drag the Text UI element here in the inspector
    private int currentStep = 0;

    private string[] narrativeSteps = new string[]
    {
        "Unsere Städte wachsen, die Bevölkerung nimmt stetig zu...",
        "Mit der wachsenden Bevölkerung und dem begrenzten landwirtschaftlichen Raum...",
        "Eine vielversprechende Lösung ist die Indoor-Kultivierung von Nahrungsmitteln...",
        "Du bist Teil eines engagierten Forscherteams...",
        "Dein Ziel ist es, die verschiedenen Parameter wie Licht, Temperatur und Nährstoffgehalt zu optimieren..."
    };

    void Start()
    {
        UpdateNarrative();
    }

    public void NextStep()
    {
        if (currentStep < narrativeSteps.Length - 1)
        {
            currentStep++;
            UpdateNarrative();
        }
    }

    void UpdateNarrative()
    {
        narrativeText.text = narrativeSteps[currentStep];
    }
}

