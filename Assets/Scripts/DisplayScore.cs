using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DisplayScore : MonoBehaviour
{
    TMP_Text scoreText;
    ScoreKeeper scoreKeeper;
    private void Awake() {
        scoreText = GetComponent<TMP_Text>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    private void Start() {
        scoreText.text = scoreKeeper.GetScore().ToString("0.00") + " seconds";
    }
}
