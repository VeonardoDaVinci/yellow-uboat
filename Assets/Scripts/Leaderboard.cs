using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private GameObject rowPrefab;
    void Start()
    {
        int count = 0;
        foreach(var score in ScoreManager.I.GetHighScores())
        {
            count++;
            rowPrefab = Instantiate(rowPrefab, transform);
            rowPrefab.GetComponent<LeaderboardRow>().nicknameRow.text = score.name.ToString();
            rowPrefab.GetComponent<LeaderboardRow>().scoreRow.text = ParseTime(score.score);
            if (count >= 3)
            {
                break;
            }
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
