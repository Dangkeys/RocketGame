using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI scoreText;
    static ScoreKeeper instance;
    private void Awake() {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Update() {
        score += Time.deltaTime;
        scoreText.text = "Time: " + score.ToString("0.0");
        Debug.Log(score);
    }
    [SerializeField] [Range(0f, float.MaxValue)] float score;
    public float GetScore(){return score;}
    public void ModifyScore(float value){score += value;Debug.Log(score);}
    public void ResetScore(){score = 0;}
}
