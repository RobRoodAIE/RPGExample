using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHud : MonoBehaviour
{
    
    public TextMeshProUGUI HealthDisplay;
    public TextMeshProUGUI MoneyDisplay;
    public TextMeshProUGUI XpDisplay;


    public void UpdateHealth(float t)
    {
        HealthDisplay.text = t.ToString("##");
    }
    public void UpdateGold(int t)
    {
        MoneyDisplay.text = t.ToString();
    }

    public void UpdateXp(int t)
    {
        XpDisplay.text = t.ToString();
    }
}
