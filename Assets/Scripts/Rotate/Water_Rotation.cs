using UnityEngine;
using System.Collections;

public class Water_Rotation : MonoBehaviour {
	void Update () {
        transform.rotation = Quaternion.AngleAxis(Time.time*3, Vector3.up);
	}
}
