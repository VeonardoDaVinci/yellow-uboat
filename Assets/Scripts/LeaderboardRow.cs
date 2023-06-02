using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardRow : MonoBehaviour
{
    [SerializeField] bool isCurrentScore = false;
    public TextMeshProUGUI nicknameRow;
    public TextMeshProUGUI scoreRow;

    private void OnEnable()
    {
        if(isCurrentScore)
        {
            nicknameRow.text = ScoreManager.I.CurrentNickname;
            scoreRow.text = ParseTime(ScoreManager.I.CurrentScore);
        }
    }

    private string ParseTime(int time)
    {
        int minutes = (time / 60);
        string seconds = (time % 60).ToString();
        if (seconds.Length < 2)
        {
            seconds = "0" + seconds;
        }
        return minutes + ":" + seconds;
    }
}
