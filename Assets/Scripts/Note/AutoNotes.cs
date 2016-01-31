using LitJson;
using System;
using System.Text;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class AutoNotes{
    public GameObject target;
    public string TargetName;
    public GameObject sphere;
    public GameObject UpLeft;
    public GameObject UpRight;
    public GameObject DownLeft;
    public GameObject DownRight;
    public List<newNote> notes;
    private OutJson outjson;
    private newNote newnote;
    public float angle;
    private Vector3 targetPos;
    private float a,b,h;
    private string nowBGMName;
    private string path;
    private FileInfo fi;
    private string jsontext;
    //debug
    private bool once = true;
    
    public void Init(){
        notes = new List<newNote>();
        //targetの用意
        UpLeft = GameObject.Find("up_left");
        UpRight = GameObject.Find("up_right");
        DownLeft = GameObject.Find("down_left");
        DownRight = GameObject.Find("down_right");
    }
    
    public void GetInput(){
        if(Input.GetKeyDown(KeyCode.S)){//up_left
            Debug.Log("S");
            //this.SetNote("up_left",UpLeft.transform.position);
            this.SetNote("up_left",(new Vector3(-2.25f,-1.0f,1.2f)));
        }
        if(Input.GetKeyDown(KeyCode.K)){//up_right
            Debug.Log("K");
            //this.SetNote("up_right",UpRight.transform.position);
            this.SetNote("up_right",(new Vector3(2.25f,-1.0f,1.2f)));
        }
        if(Input.GetKeyDown(KeyCode.X)){//down_left
            Debug.Log("X");
            //this.SetNote("down_left",DownLeft.transform.position);
            this.SetNote("down_left",new Vector3(-2.25f,-1.0f,-1.40f));
        }
        if(Input.GetKeyDown(KeyCode.M)){//down_right
            Debug.Log("M");
            //this.SetNote("down_right",DownRight.transform.position);
            this.SetNote("down_right",new Vector3(2.25f,-1.0f,-1.40f));
        }
        if((AudioManager.Instance.GetTimeBGM() >= 5.0f) & once){
            this.outNotes();
            once = false;
        }
        
    }
    
    
    public void SetNote(string TargetName,Vector3 targetPos){
        Debug.Log(targetPos);
        newnote = new newNote();
        //newnote.color = "black";
        newnote.radiusBefore = 1.0f;
        newnote.angle = UnityEngine.Random.value * 2.0f * 10.0f;//値がマイナスだと0になるため10倍 -1~1をとるため2倍
        //Debug.Log(newnote.angle);
        newnote.DecisionTime = 5.1f;
        newnote.EmitTime = AudioManager.Instance.GetTimeBGM();
        newnote.RadiusIncrement = 0.025f;
        newnote.scale = 0.025f;
        newnote.TargetName = TargetName;
        this.CalcCordinate(targetPos,newnote);
        notes.Add(newnote);
    }   
    
    public void outNotes(){
        Debug.Log("Start outnotes");
        nowBGMName = AudioManager.Instance.getNowBGMName();
        JsonMapper JsonMapper = new JsonMapper();
        path = Application.persistentDataPath + "/Notes/" ;
        // フォルダーがない場合は作成する
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        //Notesのデータ
        jsontext = JsonMapper.ToJson(notes);
        File.WriteAllText(path + nowBGMName + ".json", jsontext);
        //Notesのjsonのデータ
        outjson = new OutJson();
        outjson.set(nowBGMName,notes.Count);
        jsontext = JsonMapper.ToJson(outjson);
        File.WriteAllText(path + "Data-" +nowBGMName + ".json", jsontext);
        notes.Clear();
        Debug.Log("Done outnotes");
    }
    
    public OutJson getDataJson(string MusicName){
        JsonMapper JsonMapper = new JsonMapper();
        path = Application.persistentDataPath + "/Notes/" ;
        outjson = new OutJson();
        fi = new FileInfo(path + "Data-"+MusicName + ".json");
        try {
			using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8)){
				jsontext = sr.ReadToEnd();
			}
		} catch (Exception e){
			Debug.Log("NotFound Notes-Data File");
            Debug.Log("Error:"+e);
		}
        outjson = JsonMapper.ToObject<OutJson>(jsontext);
        return outjson;
    }
    
    public List<newNote> getNotesJson(string MusicName){
        JsonMapper JsonMapper = new JsonMapper();
        path = Application.persistentDataPath + "/Notes/" ;
        outjson = new OutJson();
        fi = new FileInfo(path +MusicName + ".json");
        try {
			using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8)){
				jsontext = sr.ReadToEnd();
			}
		} catch (Exception e){
			Debug.Log("NotFound Notes-Json File");
            Debug.Log("Error:"+e);
		}
        notes = JsonMapper.ToObject<List<newNote>>(jsontext);
        return notes; 
    }
    
    public void CalcCordinate(Vector3 targetPos,newNote newnote){
        /*
        float radius = newnote.radiusBefore + newnote.RadiusIncrement *  newnote.DecisionTime; //r2 = r0 + in
        newnote.radius = radius;
        angle = (newnote.angle /10.0f) - 1.0f;// * Mathf.Deg2Rad;
        h = 0.5f + radius * Mathf.Asin(angle);//直角三角形の高さ r1+r2*sinθ
        //Debug.Log(angle);
        b = 0.5f + radius;//円の中心と中心の長さ　直角三角形の斜辺 r1+r2
        a = Mathf.Pow(b,2) - h;//直角三角形のx軸の長さ b^2 - h
        newnote.posx = targetPos.x + a;
        newnote.posy = targetPos.y + h;
        //Debug.Log("angle:"+angle + "  posx:"+newnote.posx);
        */
        newnote.posx = targetPos.x;
        newnote.posy = targetPos.y;
        newnote.posz = targetPos.z;
    }
}