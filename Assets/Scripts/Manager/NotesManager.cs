using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class NotesManager{
	private UIManager UImanager;
	private float audiotime;
	private List<float> Timetable;
	private List<Note> notes;
	private List<newNote> newnotes;
	private Note note;
	private GameObject noteobject;
	private List<GameObject> noteobjects;
	private int NotesSum;
	private int NotesCount;
	private AutoNotes AutoNotes;
	private OutJson outjson;
	private Note_Rotation rotation;
	private string nowmusic;
	private int DisplayNotesCount;
	public GameObject UpLeft;
	public GameObject UpRight;
	public GameObject DownLeft;
	public GameObject DownRight;
	private float distance;
	private int BPM;
	private Dictionary<string,GameObject> Target;
	public float adjustDecitionTime;
	public bool keyS;
	public bool keyK;
	public bool keyX;
	public bool keyM;
	
	public void Init () {
		//this.CreateNotes();
		UImanager = new UIManager();
		NotesSum = 0;
		NotesCount = 0;
		DisplayNotesCount = 0;
		UpLeft = GameObject.Find("up_left");
		UpRight = GameObject.Find("up_right");
		DownLeft = GameObject.Find("down_left");
		DownRight = GameObject.Find("down_right");
		Target = new Dictionary<string,GameObject> (){
			{"up_left",UpLeft},
			{"up_right",UpRight},
			{"down_left",DownLeft},
			{"down_right",DownRight}

		};
		keyS=false;
		keyK=false;
		keyX=false;
		keyM=false;
	}
	
	public void CreateNotes(){
		nowmusic = AudioManager.Instance.getNowBGMName();
		AutoNotes = new AutoNotes();
		outjson = AutoNotes.getDataJson(nowmusic);
		//BPM
		BPM = outjson.getBPM();
		//Note数を取り込む
		NotesSum = outjson.getNotesSum();
		newnotes = AutoNotes.getNotesJson(nowmusic);
		//Debug.Log("Sumnotes:"+NotesSum);
		//Note数を元に配列を初期化
		notes = new List<Note>(NotesSum);
		noteobjects = new List<GameObject>(NotesSum);
		Timetable = new List<float>(NotesSum);
		//ファイルからノーツ情報を読み込む処理
		for(int j=0; j<NotesSum; j++){
			//Debug.Log("readNotes:"+ j);
			noteobject = GameObject.Instantiate(Resources.Load("Prefabs/Sphere", typeof(GameObject))) as GameObject;
			rotation = noteobject.GetComponent<Note_Rotation>();
			note = noteobject.GetComponent<Note>();
			note.posx = newnotes[j].posx;
			note.posy = newnotes[j].posy;
			note.posz = newnotes[j].posz;
			note.angle = newnotes[j].angle;
			note.scale = newnotes[j].scale;
			note.radiusBefore = newnotes[j].radiusBefore;
			note.DecisionTime = newnotes[j].DecisionTime;
			note.EmitTime = newnotes[j].EmitTime;
			note.RadiusIncrement = newnotes[j].RadiusIncrement;
			note.TargetName = newnotes[j].TargetName;
			//Debug.Log(note.EmitTime);
			note.Init(noteobject);
			note.Hidden();
			Timetable.Add(note.EmitTime);
			notes.Add(note);
			noteobjects.Add(noteobject);
			//Debug.Log(notes.Count);
		}
		//Debug.Log(notes.Count);
		//this.NoteDestroy(noteobject);
	}
	
	public GameObject getTargets(string TargetName){
		return GameObject.Find(TargetName);
	}
	
	private void NoteDestroy(int i){
		noteobject = noteobjects[i];//避難させる
		noteobjects.RemoveAt(i);//リストを先に消す
		notes.RemoveAt(i);
		UnityEngine.Object.Destroy(noteobject);//避難させたオブジェクトも消す
		DisplayNotesCount--;
	}
	
	public void StartManagement(){
		if (!AudioManager.Instance.isPlayingBGM ()) {
			if(Input.GetKeyDown(KeyCode.Space)){
				Application.LoadLevel("menu"); 
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape)){
			Application.LoadLevel("menu"); 
		}
		audiotime = AudioManager.Instance.GetTimeBGM();
		if(NotesCount == NotesSum){
			//Debug.Log("End Notes");
			//Debug.Log("notescount:"+NotesCount +" NotesSum:"+NotesSum);
		}else {
			//Debug.Log("audiotime:"+audiotime +" timetable:"+Timetable[0]);
			if(audiotime >= Timetable[0]){
				//Debug.Log("count"+NotesCount); 
				notes[DisplayNotesCount].Show();
				//notes[DisplayNotesCount].Move();
				//Debug.Log("Notes show"+NotesCount);
				NotesCount++;
				DisplayNotesCount++;
				Timetable.RemoveAt(0);
			}
		}//continue
		for(int i=0;i<DisplayNotesCount;i++){
			//Debug.Log(i + ":"+DisplayNotesCount);
			//Debug.Log((note.DecisionTime + note.EmitTime));
			note = notes[i];
			note.Move();
			if(Input.GetKeyDown(KeyCode.S)){
				if(note.TargetName == "up_left"){
					if(JudgeArea(Target["up_left"].transform.position,note.positionxyz)){
						this.JudgeTiming(Target["up_right"].transform.position,note.positionxyz);
						this.NoteDestroy(i);
					}
				}
			}else if(Input.GetKeyDown(KeyCode.K)){
				if(note.TargetName == "up_right"){
					if(JudgeArea(Target["up_left"].transform.position,note.positionxyz)){
						this.JudgeTiming(Target["up_left"].transform.position,note.positionxyz);
						this.NoteDestroy(i);
					}
				}
			}else if(Input.GetKeyDown(KeyCode.M)){
				if(note.TargetName == "down_right"){
					if(JudgeArea(Target["down_right"].transform.position,note.positionxyz)){
						this.JudgeTiming(Target["down_right"].transform.position,note.positionxyz);
						this.NoteDestroy(i);
					}
				}
			}else if(Input.GetKeyDown(KeyCode.X)){
				if(note.TargetName == "down_left"){
					if(JudgeArea(Target["down_left"].transform.position,note.positionxyz)){
						this.JudgeTiming(Target["down_left"].transform.position,note.positionxyz);
						this.NoteDestroy(i);
					}
				}
			}else if((note.DecisionTime + note.EmitTime - adjustDecitionTime) <= audiotime){// 発生時間+消滅予想時間をすぎたら消す.
				Debug.Log("del Time");
				this.NoteDestroy(i);//避難させたオブジェクトも消す
			}
		}
	}
			//Vector3 noteVector3 = new Vector3(note.posx,note.posy,2.3f);
			/*
			if(note.TargetName == "up_left"){
				//Debug.Log("up_left");
				if(this.JudgeArea(UpLeft.transform.position,note.positionxyz,note.scale)){
					Debug.Log ("ok");
					if(Input.GetKeyDown(KeyCode.S)){
						Debug.Log("S");
						Debug.Log("del S");
						this.NoteDestroy(i);//避難させたオブジェクトも消す
						scoremanager.AddScore();
					}
				}
			}
			else if(note.TargetName == "up_right"){
				//Debug.Log("up_right");
				if(this.JudgeArea(UpRight.transform.position,note.positionxyz,note.scale)){
					if(Input.GetKeyDown(KeyCode.K)){
						Debug.Log("K");
						Debug.Log("del K");
						this.NoteDestroy(i);//避難させたオブジェクトも消す
						scoremanager.AddScore();
					}
				}
			}
			else if(note.TargetName == "down_left"){
				//Debug.Log("down_left");
				if(this.JudgeArea(DownLeft.transform.position,note.positionxyz,note.scale)){
					if(Input.GetKeyDown(KeyCode.X)){
						Debug.Log("X");
						Debug.Log("del X");
						this.NoteDestroy(i);//避難させたオブジェクトも消す
						scoremanager.AddScore();
					}
				}
			}else if(note.TargetName == "down_right"){
					//Debug.Log("down_right");
				if(this.JudgeArea(DownRight.transform.position,note.positionxyz,note.scale)){
					if(Input.GetKeyDown(KeyCode.M)){
						Debug.Log("M");
						Debug.Log("del M");
						this.NoteDestroy(i);//避難させたオブジェクトも消すす
						scoremanager.AddScore();
					}
				}
			}
			// 発生時間+消滅予想時間をすぎたら消す.
			else if((note.DecisionTime + note.EmitTime) <= audiotime){
				Debug.Log("del Time");
				this.NoteDestroy(i);//避難させたオブジェクトも消す
			}
    }//EndStartManage*/
    
    public bool JudgeArea(Vector3 tposition,Vector3 nposition){
		distance = Distance (tposition, nposition);
        //Debug.Log(distance);
        if(0.2f < distance && distance < 9f){// distance of 2 objects codinates == target radius
            return true;
        }else{
			Debug.Log (distance);
            return false;
        }
    }
	public void JudgeTiming(Vector3 tposition,Vector3 nposition){
		distance = Distance(tposition, nposition);
		if (8.4f < distance && distance < 8.6f) {
			Debug.Log ("grate!!: " + distance);
			UImanager.AddScore (20);
			UImanager.DisplayJudge("grate",(distance - 8.5f));
		} else if (8.3f < distance && distance < 9f) {
			Debug.Log ("good: " + distance);
			UImanager.AddScore (10);
			UImanager.DisplayJudge("good",(distance - 0.7f));
		} else {
			Debug.Log ("bad: " + distance);
		}
	}
	public float Distance(Vector3 tposition,Vector3 nposition){
		return Vector3.Distance(tposition, nposition)+0.5f;
	}
}
