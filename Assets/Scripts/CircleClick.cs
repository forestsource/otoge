using UnityEngine;
using System.Collections;

public class CircleClick : MonoBehaviour {
	
	private Vector3 clickPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			clickPosition = Input.mousePosition;
			Debug.Log(clickPosition);
		}
	}
}
