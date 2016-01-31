using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager{
    Score Score;
    GameObject ScoreLable;
    
    void Start(){
    }
    
    public void AddScore(){
        ScoreLable = GameObject.Find("Score");
        Score = ScoreLable.GetComponent<Score>();
        Score.score = Score.score + 10;
    }
}

//シングルトンで実装し、単一の保証とどこからでも加算できるようにすべき