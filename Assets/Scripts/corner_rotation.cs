using UnityEngine;
using System.Collections;



public class corner_rotation : MonoBehaviour {
	void Update () {
        transform.rotation = Quaternion.AngleAxis(Time.time * 30, Vector3.right);
	}
}
//Time.time * 120.0