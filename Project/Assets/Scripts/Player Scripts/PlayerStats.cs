using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    // Býr til field/box fyrir health og stamina stats, það þarf að draga health og stamina objectin í þessi box í inspectorinum
    [SerializeField]
    private Image health_Stats, stamina_Stats;
    //
    public Text scoreText;
    private int score;
    // Function til að uppfæra health UI
    public void Display_HealthStats(float healthValue)
    {
        // Fill value er frá 0.0 upp í 1.0
        // Deilir með 100 til að fá rétt fill value
        healthValue /= 100f;
        // Fillir svo UI
        health_Stats.fillAmount = healthValue;
    }
    // Sama og hitt nema fyrir stamina
    public void Display_StaminaStats(float staminaValue)
    {
        staminaValue /= 100f;
        stamina_Stats.fillAmount = staminaValue;
    }
    // Uppfærir score þegar enemy deyr
    public void Display_Score()
    {
        score += 100;
        scoreText.text = score.ToString();
    }
}