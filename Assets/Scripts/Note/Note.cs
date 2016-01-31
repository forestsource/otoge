using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour{
    //public Color color;
    public float scale;
    public float posx;
    public float posy;
    public float posz;
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
    public SphereCollider sc;
    
    public void Init(GameObject noteobject){
        rend = GetComponent<Renderer>();
        this.transform.position = new Vector3(posx,posy,posz);
        this.transform.SetLocalScaleXYZ(scale);
        this.GetComponent<SphereCollider>().radius = radiusBefore;
        //rend.material.color = color;
    }
    
    public void Move(){
        sc = GetComponent<SphereCollider>();
        //scale = scale + RadiusIncrement;
        this.transform.AddLocalScaleX(RadiusIncrement);
        this.transform.AddLocalScaleZ(RadiusIncrement);
        //sc.radius = sc.radius + RadiusIncrement;
        //Debug.Log(sc.radius);
    }
    
    public void Show(){
        rend.enabled = true;
    }
    
    public void Hidden(){
        rend.enabled = false;
    }
}
