using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Health playerHealth;
    
    ScoreKeeper scoreKeeper;

    Slider slider;
    TextMeshProUGUI text;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        slider = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        slider.maxValue = playerHealth.GetHealth();
        scoreKeeper.ResetScore();
        text.text = scoreKeeper.GetScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
         UpdateUI();
    }

    void UpdateUI()
    {
        slider.value = playerHealth.GetHealth();
        text.text = scoreKeeper.GetScore().ToString("000000000");
    }
}
