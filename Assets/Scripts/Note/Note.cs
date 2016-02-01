using UnityEngine;
using System.Collections;

public class Note : MonoBehaviour{
    //public Color color;
    public float scale;
    public float posx;
    public float posy;
    public float posz;
	public Vector3 positionxyz;
    public float radiusBefore;
    public float radius;
    public float angle;
    public float EmitTime;
    public GameObject target;
    public string TargetName;
    public float DecisionTime;
    public float RadiusIncrement;
    private Renderer rend;
	public float BPM;
    private Transform targettranform;
    public SphereCollider sc;
    
    public void Init(GameObject noteobject){
        rend = GetComponent<Renderer>();
        this.transform.position = new Vector3(posx,posy,posz);
        this.transform.SetLocalScaleXYZ(1);
        this.GetComponent<SphereCollider>().radius = 0.5f;
		this.BPM = 0f;
    }
    
    public void Move(){
		GameObject tobj = GameObject.Find(TargetName);
		this.positionxyz = Vector3.MoveTowards (this.transform.position, tobj.transform.position, (this.DecisionTime + BPM) * Time.deltaTime );// goto taget for radius circle
		this.transform.position = this.positionxyz;
    }
    
    public void Show(){
        rend.enabled = true;
    }
    
    public void Hidden(){
        rend.enabled = false;
    }
}
