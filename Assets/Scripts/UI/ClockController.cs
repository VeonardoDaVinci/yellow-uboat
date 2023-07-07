using ExtensionMethods;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClockController : MonoBehaviour
{
    public int Time { get { return time; } }

    private int time = 120;
    [SerializeField] private TextMeshProUGUI clockText;
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
        clockText.text = time.ParseIntToTimeString();
    }

    private IEnumerator CountDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            RemoveTime(1);
            clockText.text = time.ParseIntToTimeString();
        }
    }
}
