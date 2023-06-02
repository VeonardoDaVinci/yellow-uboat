using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public class Score
{
    public string name;
    public int score;

    public Score(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

public class ScoreList
{
    public List<Score> scores;
}
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager I;
    public string CurrentNickname = "";
    public int CurrentScore = 0;
    public ScoreList ScoreList;
    void Awake()
    {
        if (!I)
        {
            I = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        Debug.Log(json);
        if(json == "{}" || json == null)
        {
            ScoreList = new ScoreList();
        }
        else
        {
            ScoreList = JsonUtility.FromJson<ScoreList>(json);
        }
    }

    public IEnumerable<Score> GetHighScores()
    {
        return ScoreList.scores.OrderByDescending(x=>x.score);
    }

    public void AddNewHighScore(int score)
    {
        ConnectionManager.I.SendClientMessage("KONIEC "+score);
        CurrentScore = score;
        ScoreList.scores.Add(new Score(CurrentNickname, score));
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(ScoreList);
        Debug.Log(json);
        PlayerPrefs.SetString("scores", json);
    }


    private void OnApplicationQuit()
    {
        SaveScore();
    }
}
