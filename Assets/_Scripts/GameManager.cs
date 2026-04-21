using UnityEngine;
using TMPro;
using NUnit.Framework;
using System;

public class GameManager : MonoBehaviour
{

    public float timeLeft = 60f;

    private float timeSurvived = 0f;
    public TextMeshProUGUI timerText;
    public GameObject endGameMenu;
    public TextMeshProUGUI finalTimerText;

    private bool isGameOver = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        //GameController.Init();

        if (endGameMenu != null)
        {
            endGameMenu.SetActive(false);
        }

        UpdateTimerUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            return;
        }

        timeLeft -= Time.deltaTime;
        timeSurvived += Time.deltaTime;

        if (timeLeft <= 0f){
            timeLeft = 0;
            TriggerGameOver();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI(){
        if (timerText != null){
            timerText.text = "Tempo: " + Mathf.FloorToInt(timeLeft) + "s";
        }
    }

    public void AddTime(float amount)
    {
        if (isGameOver)
        {
            return;
        }

        timeLeft += amount;
        UpdateTimerUI();
    }

    public void RemoveTime(float amount)
    {
        if (isGameOver)
        {
            return;
        }
        timeLeft -= amount;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            TriggerGameOver();
        }

        UpdateTimerUI();
    }

    public void TriggerGameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        if (finalTimerText != null)
        {
            finalTimerText.text = "Tempo sobrevivido: "+Mathf.FloorToInt(timeSurvived) + "s";

        }
        if (endGameMenu != null)
        {
            endGameMenu.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }


}
