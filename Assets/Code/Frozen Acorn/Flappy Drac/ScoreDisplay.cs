using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreDisplay : MonoBehaviour {
    public TextMeshProUGUI scoreText;
    
    public void UpdateScore(int score) {
        scoreText.text = score.ToString();
    }
}