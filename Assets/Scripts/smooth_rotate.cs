using UnityEngine;
using System.Collections;

public class smooth_rotate : MonoBehaviour {
    float minAngle = 0.0F;
    float maxAngle = 360.0F;
    void Update() {
        float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
        transform.eulerAngles = new Vector3(0, angle, 0);
        /*
        if(angle >= 360){
            angle = 0;
        }*/
    }
}
