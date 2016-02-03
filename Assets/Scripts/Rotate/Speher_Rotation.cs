using UnityEngine;
using System.Collections;
public class Speher_Rotation : MonoBehaviour {
	void Update () {
        //transform.rotation = Quaternion.AngleAxis(Time.time*120, Vector3.up);
        //transform.rotation = Quaternion.AngleAxis(Time.time*50, Vector3.forward);
        transform.rotation = Quaternion.AngleAxis(Time.time*50, new Vector3(0,1,1));
	}
}