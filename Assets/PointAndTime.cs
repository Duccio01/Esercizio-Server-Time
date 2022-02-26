using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndTime : MonoBehaviour
{

    public int score;

    private void Start()
    {
        score = GetComponent<ScoreHandler>()._score;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Value>())
        {
            score += other.gameObject.GetComponent<Value>().ballValue;
            Destroy(other.gameObject);
        }
    }
}
