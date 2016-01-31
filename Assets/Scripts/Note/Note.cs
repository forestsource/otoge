using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour{
    //public Color color;
    public float scale;
    public float posx;
    public float posy;
    public float radiusBefore;
    public float radius;
    public float angle;
    public float EmitTime;
    public GameObject target;
    public string TargetName;
    public float DecisionTime;
    public float RadiusIncrement;
    private Renderer rend;
    private float a;
    private float b;
    private float h;
    private Transform targettranform;
    private SphereCollider sc;
    
    public void Init(GameObject noteobject){
        rend = GetComponent<Renderer>();
        this.transform.position = new Vector3(posx,posy,0);
        this.transform.SetLocalScaleXYZ(scale);
        this.GetComponent<SphereCollider>().radius = radiusBefore;
        //rend.material.color = color; 
    }
    
    public void Move(){
        this.transform.AddLocalScaleX(RadiusIncrement);
        this.transform.AddLocalScaleY(RadiusIncrement);
    }
    
    public void Show(){
        rend.enabled = true;
    }
    
    public void Hidden(){
        rend.enabled = false;
    }
}
