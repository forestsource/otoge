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
	private float radius;
	private float BPM ;
	private float Endtime;
    //debug
    private bool once = true;
    
    public void Init(){
        notes = new List<newNote>();
        //targetの用意
        UpLeft = GameObject.Find("up_left");
        UpRight = GameObject.Find("up_right");
        DownLeft = GameObject.Find("down_left");
        DownRight = GameObject.Find("down_right");
		BPM = 1.0f;
		Endtime = AudioManager.Instance.LengthBGM ();
    }
    
    public void GetInput(){
        if(Input.GetKeyDown(KeyCode.S)){//up_left
            Debug.Log("S");
            this.SetNote("up_left",UpLeft.transform.position);
        }
        if(Input.GetKeyDown(KeyCode.K)){//up_right
            Debug.Log("K");
            this.SetNote("up_right",UpRight.transform.position);
        }
        if(Input.GetKeyDown(KeyCode.X)){//down_left
            Debug.Log("X");
            this.SetNote("down_left",DownLeft.transform.position);
        } 
        if(Input.GetKeyDown(KeyCode.M)){//down_right
            Debug.Log("M");
            this.SetNote("down_right",DownRight.transform.position);
        }
		if((AudioManager.Instance.isPlayingBGM() == false) & once){
            this.outNotes();
            once = false;
			Application.LoadLevel("menu");
        }
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.LoadLevel ("menu");
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			this.outNotes(); 
			Application.LoadLevel("menu");
		}
    }
    
    
    public void SetNote(string TargetName,Vector3 TargetPosition){
        newnote = new newNote();
        //newnote.color = "black";
        //newnote.radiusBefore = 1.0f;
        //newnote.angle = UnityEngine.Random.value * 2.0f * 10.0f;//値がマイナスだと0になるため10倍 -1~1をとるため2倍
        //Debug.Log(newnote.angle);
        newnote.EmitTime = AudioManager.Instance.GetTimeBGM() - BPM ; // emit time is before 2.0s time on Input
		newnote.DecisionTime = 2.0f;
        newnote.RadiusIncrement = 0.025f;
        newnote.scale = 0.5f; 
        newnote.TargetName = TargetName;
        this.CalcCordinate(TargetPosition,newnote);
        notes.Add(newnote);
    }   
	public void CalcCordinate(Vector3 tposition,newNote newnote){
		radius = 1.5f;
		angle = UnityEngine.Random.Range (0, 360);
		float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
		float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
		newnote.posx = tposition.x + x;
		newnote.posy = -1.23f;
		newnote.posz = tposition.y + y;
	}
    
    public void outNotes(){
        Debug.Log("Start outnotes");
        nowBGMName = AudioManager.Instance.getNowBGMName();
        JsonMapper JsonMapper = new JsonMapper();
        path = Application.persistentDataPath + "/Notes/" ;
		Debug.Log ("save path: "+path);
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
    
}