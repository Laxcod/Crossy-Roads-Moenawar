using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;

    public void UpdaScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
