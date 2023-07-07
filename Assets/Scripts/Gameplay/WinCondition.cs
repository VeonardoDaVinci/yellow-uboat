using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WinCondition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ScaleBackAndForth());
    }

    private IEnumerator ScaleBackAndForth()
    {
        while (true)
        {
            transform.DOScale(1.5f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            transform.DOScale(1.0f, 1.5f);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
