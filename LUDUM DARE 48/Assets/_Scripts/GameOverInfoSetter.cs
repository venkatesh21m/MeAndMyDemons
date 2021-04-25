using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameOverInfoSetter : MonoBehaviour
{
    public TMP_Text ReachedLevel;
    public TMP_Text Enemieskilled;
    public TMP_Text Score;
    public TMP_Text HighScore;

    public void SetGameOverDetatils(int level, int enemieskilled, int score)
    {
        ReachedLevel.text = "Reached Level : " + level.ToString();
        Enemieskilled.text = "Enemied Killed : "+ enemieskilled.ToString();
        Score.text = "Score : "+ score.ToString();
        HighScore.text = "HighScore : " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
