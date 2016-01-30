using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour{
    public Color color = Color.black;
    public float scale = 1.0f;
    public float posx;
    public float posy;
    public float radiusBefore = 0.8f;
    public float radius;
    public float angle;
    public float EmitTime;
    public string target = "up_left";
    public float DecisionTime;
    public float RadiusIncrement = 0.01f;
    private Renderer rend;
    
    void Start(){
        radius = radiusBefore + RadiusIncrement *  DecisionTime; //r2 = r0 + in
    }
    
    public void Init(GameObject noteobject){
        rend = GetComponent<Renderer>();
        this.transform.position = new Vector3(posx,posy,0);
        this.transform.SetLocalScaleXYZ(scale);
        //note.GetComponent<SphereCollider>().radius = radiusBefore;
        //Debug.Log(this.GetComponent<SphereCollider>().radius);
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
