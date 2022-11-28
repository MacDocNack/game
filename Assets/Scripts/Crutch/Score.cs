using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text _scoreToText;
    private int score = 0;

    public void ScoreAdd(int points)
    {
        score += points;
        _scoreToText.text = score.ToString();
    }
    public void ScoreReduce()
    {
        score -= 100;
        _scoreToText.text = score.ToString();
    }
}
