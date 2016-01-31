using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class NotesManager{
    private ScoreManager scoremanager;
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
    
    public bool keyS;
    public bool keyK;
    public bool keyX;
    public bool keyM;

	public void Init () {
        //this.CreateNotes();
        scoremanager = new ScoreManager();
        NotesSum = 0;
        NotesCount = 0;
        DisplayNotesCount = 0;
        UpLeft = GameObject.Find("up_left");
        UpRight = GameObject.Find("up_right");
        DownLeft = GameObject.Find("down_left");
        DownRight = GameObject.Find("down_right");
        keyS=false;
        keyK=false;
        keyX=false;
        keyM=false;
	}
    
	public void CreateNotes(){
        nowmusic = AudioManager.Instance.getNowBGMName();
        AutoNotes = new AutoNotes();
        outjson = AutoNotes.getDataJson(nowmusic);
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
            Debug.Log("readNotes:"+ j);
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
            rotation.EmitTime = note.EmitTime;
            note.Init(noteobject);
            //note.Hidden();
            Timetable.Add(note.EmitTime);
            notes.Add(note);
            noteobjects.Add(noteobject);
        }
        //Debug.Log(notes.Count);
        //this.NoteDestroy(noteobject);
    }
    
    public GameObject getTargets(string TargetName){
        return GameObject.Find(TargetName);
    }
    
    private void NoteDestroy(GameObject noteobject){
        UnityEngine.Object.Destroy(noteobject);
    }
    
    public void StartManagement(){
        keyS=false;
        keyK=false;
        keyX=false;
        keyM=false;
        audiotime = AudioManager.Instance.GetTimeBGM();
        if(NotesCount == NotesSum){
            //Debug.Log("End Notes");
            //Debug.Log("notescount:"+NotesCount +"  NotesSum:"+NotesSum);
        }else {
            //Debug.Log("audiotime:"+audiotime +"  timetable:"+Timetable[0]);
            if(audiotime >= Timetable[0]){
                //Debug.Log("count"+NotesCount);  
                notes[DisplayNotesCount].Show();
                Debug.Log("Notes show"+NotesCount);
                NotesCount++;
                DisplayNotesCount++;
                Timetable.RemoveAt(0);
            }
        }//continue           
        for(int i=0;i<DisplayNotesCount;i++){
            //Debug.Log(i + ":"+DisplayNotesCount);
            //Debug.Log((note.DecisionTime + note.EmitTime));
            note = notes[i];
            //Vector3 noteVector3 = new Vector3(note.posx,note.posy,2.3f);
            note.Move();
            if(note.TargetName == "up_left"){
                //Debug.Log("up_left");
                if(this.CalcTimeOut(UpLeft.transform.position,note.transform.position,note.scale)){
                    if(Input.GetKeyDown(KeyCode.S)){
                        Debug.Log("S");
                        Debug.Log("del S");
                        noteobject = noteobjects[i];//避難させる          
                        noteobjects.RemoveAt(i);//リストを先に消す
                        notes.RemoveAt(i);
                        this.NoteDestroy(noteobject);//避難させたオブジェクトも消す
                        DisplayNotesCount--;
                        scoremanager.AddScore();
                    }
                }
            }
            else if(note.TargetName == "up_right"){
                //Debug.Log("up_right");
                if(this.CalcTimeOut(UpLeft.transform.position,note.transform.position,note.scale)){
                    if(Input.GetKeyDown(KeyCode.K)){
                        Debug.Log("K");
                        Debug.Log("del K");
                        noteobject = noteobjects[i];//避難させる          
                        noteobjects.RemoveAt(i);//リストを先に消す
                        notes.RemoveAt(i);
                        this.NoteDestroy(noteobject);//避難させたオブジェクトも消す
                        DisplayNotesCount--;
                        scoremanager.AddScore();
                    }
                }
            }
            else if(note.TargetName == "down_left"){
                //Debug.Log("down_left");
                if(this.CalcTimeOut(UpLeft.transform.position,note.transform.position,note.scale)){
                    if(Input.GetKeyDown(KeyCode.X)){
                        Debug.Log("X");
                        Debug.Log("del X");
                        noteobject = noteobjects[i];//避難させる          
                        noteobjects.RemoveAt(i);//リストを先に消す
                        notes.RemoveAt(i);
                        this.NoteDestroy(noteobject);//避難させたオブジェクトも消す
                        DisplayNotesCount--;
                        scoremanager.AddScore();
                    }
                }
            }
            else if(note.TargetName == "down_right"){
                //Debug.Log("down_right");
                if(this.CalcTimeOut(UpLeft.transform.position,note.transform.position,note.scale)){
                    if(Input.GetKeyDown(KeyCode.M)){
                        Debug.Log("M");
                        Debug.Log("del M");
                        noteobject = noteobjects[i];//避難させる          
                        noteobjects.RemoveAt(i);//リストを先に消す
                        notes.RemoveAt(i);
                        this.NoteDestroy(noteobject);//避難させたオブジェクトも消す
                        DisplayNotesCount--;
                        scoremanager.AddScore();
                    }
                }
            }
            // 発生時間+消滅予想時間をすぎたら消す.
            if((note.DecisionTime + note.EmitTime) <= audiotime){
                Debug.Log("del Time");
                noteobject = noteobjects[i];//避難させる          
                noteobjects.RemoveAt(i);//リストを先に消す
                notes.RemoveAt(i);
                this.NoteDestroy(noteobject);//避難させたオブジェクトも消す
                DisplayNotesCount--;
            } 
        }              
	}//StartManagement
    
    public bool CalcTimeOut(Vector3 position1,Vector3 position2,float scale){
        distance = Vector3.Distance(position1, position2);
        //Debug.Log(distance);
        if(distance > 1+scale && distance < 2+scale){//r1+r2
            return true;
        }else{
            return false;
        }
    }  
}
