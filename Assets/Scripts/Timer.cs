using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float timeRemaining = 30;
    public bool timerIsRunning = false;
    [SerializeField] public TextMeshProUGUI timeText;
    [SerializeField] public RawImage endDemo;
    [SerializeField] public TextMeshProUGUI endDemoText;
    [SerializeField] public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        endDemo.enabled = false;
        endDemoText.enabled = false;
        timerIsRunning = true;
        music.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);

            }
            else
            {
                Debug.Log("Time has ran out!");
                timeRemaining = 0;
                timeText.text = string.Empty;
                endDemo.enabled = true;
                endDemoText.enabled = true;
                timerIsRunning = false;
                music.Stop();
            }
        }
    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        float ms = Mathf.FloorToInt(time % 1) * 1000;
        timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, ms);

    }
}
