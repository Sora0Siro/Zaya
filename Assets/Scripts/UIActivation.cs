using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivation : MonoBehaviour
{
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject fadePanel;
    
    public void RestartButtonState(bool state)
    {
        menuPanel.SetActive(state);
        restartButton.SetActive(state);
    }

    public void FadePanelState(bool state)
    {
        fadePanel.SetActive(state);
    }
}