using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager{
    Score Score;
	Judge Judge;
    GameObject ScoreLable;
	GameObject JudgeLable;
    
    
    public void AddScore(int point){
		ScoreLable = GameObject.Find("Score");
		Score = ScoreLable.GetComponent<Score>();
        Score.score = Score.score + point;
    }

	public void DisplayJudge(string judge,float TimeDistance){
		JudgeLable = GameObject.Find ("Judge");
		Judge = JudgeLable.GetComponent<Judge>();
		Judge.judge (judge,TimeDistance);
	}
}

//シングルトンで実装し、単一の保証とどこからでも加算できるようにすべき