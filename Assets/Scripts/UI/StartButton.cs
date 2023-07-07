using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] TMP_InputField nicknameInput;
    public void OnButtonClicked()
    {
        if (nicknameInput.text.Trim() != "")
        {
            if (nicknameInput.text == "CLEARLIST")
            {
                ScoreManager.I.ScoreList = new();
                ScoreManager.I.ScoreList.scores = new List<Score>();
                PlayerPrefs.SetString("scores", "{}");
                return;
            }
            ConnectionManager.I.SendClientMessage("START");
            ScoreManager.I.CurrentNickname = nicknameInput.text;
            SceneManager.LoadScene("MainScene");
        }
    }
}
