using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI clockText;
    private int time = 120;
    public int Time { get { return time; } }
    private void Start()
    {
        StartCoroutine(CountDown());
    }

    public void RemoveTime(int timeToRemove)
    {
        time-=timeToRemove;
        if (time < 0)
        {
            time = 0;
            ConnectionManager.I.SendClientMessage("GAMEOVER");
            SceneManager.LoadScene("GameOver");
        }
        clockText.text = ParseTime(time);
    }

    public string ParseTime(int time)
    {
        int minutes = (time / 60);
        string seconds = (time % 60).ToString();
        if(seconds.Length <2)
        {
            seconds= "0" + seconds;
        }
        return  minutes + ":" + seconds;
    }

    private IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            RemoveTime(1);
            clockText.text = ParseTime(time);
        }
    }
}
