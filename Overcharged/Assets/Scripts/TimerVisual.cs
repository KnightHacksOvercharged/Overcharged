using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerVisual : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private void Awake()
    {
        timerText = this.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (GameManager.Instance.remainingTime <= 0)
        {
            GameManager.Instance.remainingTime = 0;
            // GameOver();
        }
        else
        {
            GameManager.Instance.remainingTime -= Time.deltaTime;
        }
    }
}
