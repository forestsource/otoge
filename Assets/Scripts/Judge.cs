using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Judge : MonoBehaviour {
	void Update () {

	}
	public void judge(string judge,float timedistance){
		this.GetComponent<Text>().text = judge+": "+ timedistance;
	}
}
