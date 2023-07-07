using ExtensionMethods;
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
            scoreRow.text = ScoreManager.I.CurrentScore.ParseIntToTimeString();
        }
    }
}
