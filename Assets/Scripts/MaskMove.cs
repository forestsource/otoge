using UnityEngine;
using System.Collections;

public class MaskMove : MonoBehaviour {
    
    private RectTransform rect_transform;
    private float newX;
    
    void Start(){
        rect_transform = GetComponent<RectTransform>();
    }
    
    public void setX(float percent){
        newX =  rect_transform.sizeDelta.x * percent;
        rect_transform.localPosition = new Vector3(newX, 0, 0);
    }
}

//  GetComponent<RectTransform>().localPosition.x //posX
//  GetComponent<RectTransform>().sizeDelta.x