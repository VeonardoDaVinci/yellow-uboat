using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizePosition : MonoBehaviour
{
    [SerializeField] private Vector2 randomizeRangeRadius;
    [SerializeField] private GameObject submarine;
    void Start()
    {
        submarine = FindObjectOfType<SubmarineController>().gameObject;
        Randomize();
    }

    private void Randomize()
    {
        gameObject.SetActive(false);
        float newX = 0f;
        float newY = 0f;
        newX = Random.Range(0f, randomizeRangeRadius.x) * (Random.Range(1, 3) == 1 ? -1 : 1);
        newY = Random.Range(0f, randomizeRangeRadius.y) * (Random.Range(1, 3) == 1 ? -1 : 1);
        transform.position = new Vector2(newX, newY);
        if (Vector3.Distance(transform.position, submarine.transform.position) <= 2f)
        {
            Randomize();
            return;
        }
        gameObject.SetActive(true);
    }
}
