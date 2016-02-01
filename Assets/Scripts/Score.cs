using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
    public float score;
    void Start(){
        score = 0;
    }
    
	void Update () {
        this.GetComponent<Text>().text = "Score: "+ score;
	}
}
