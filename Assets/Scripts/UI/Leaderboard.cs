using ExtensionMethods;
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
            rowPrefab.GetComponent<LeaderboardRow>().scoreRow.text = score.score.ParseIntToTimeString();
            if (count >= 3)
            {
                break;
            }
        }
    }
}
