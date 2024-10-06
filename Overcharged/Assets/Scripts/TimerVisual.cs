using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float remainingTime;

    private void Awake()
    {
        timerText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        remainingTime = GameManager.Instance.GetCountdownTimerValue();
    }

    private void Update()
    {
        if (remainingTime <= 0)
        {
            remainingTime = 0;
        }
        else
        {
            remainingTime -= Time.deltaTime;
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
