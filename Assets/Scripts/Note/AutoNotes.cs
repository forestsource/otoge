using UnityEngine;
using System.Collections;

public class AutoNotes{
    public TargetSphere target;
    public GameObject sphere;
    public Note note;
    public float angle;
    private float a;
    private float b;
    private float h;
    
    void Start(){
        this.init();
        h = target.radius + note.radius * Mathf.Asin(note.angle);//直角三角形の高さ r1+r2*sinθ
        b = target.radius + note.radius;//円の中心と中心の長さ　直角三角形の斜辺 r1+r2
        a = Mathf.Pow(b,2) - h;//直角三角形のx軸の長さ b^2 - h
        note.posx = target.posx + a;
        note.posy = target.posy + h;
    }
    
    void init(){
        sphere = GameObject.Find(note.target);
        target.posx = sphere.transform.position.x;
        target.posy = sphere.transform.position.y;
        target.radius = sphere.GetComponent<SphereCollider>().radius;
    }
}

public class TargetSphere{
    public float posx;
    public float posy;
    public float radius;
}