using UnityEngine;
using System.Collections;
public class Note_Rotation  : MonoBehaviour {
    public float EmitTime;
	void Update () {
        //transform.rotation = Quaternion.AngleAxis(Time.time*120, Vector3.up);
        //transform.rotation = Quaternion.AngleAxis(Time.time*50, Vector3.forward);
        transform.rotation = Quaternion.AngleAxis(EmitTime*20+Time.time*70, new Vector3(0,1,0));
	}
}