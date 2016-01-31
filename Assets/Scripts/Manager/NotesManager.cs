using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;

public class NotesManager{
    private float audiotime;
    private float[] Timetable;
    private Note[] notes;
    private List<newNote> newnotes;
    private Note note;
    private GameObject noteobject;
    private int NotesSum;
    private int NotesCount;
    private AutoNotes AutoNotes;
    private OutJson outjson;
    private string nowmusic;

	public void Init () {
        this.CreateNotes();
        NotesSum = 0;
        NotesCount = 0;
	}
    
	public void CreateNotes(){
        nowmusic = AudioManager.Instance.getNowBGMName();
        AutoNotes = new AutoNotes();
        outjson = AutoNotes.getDataJson(nowmusic);
        //Note数を取り込む
        NotesSum = outjson.getNotesSum();
        newnotes = AutoNotes.getNotesJson(nowmusic);
        Debug.Log("Sumnotes:"+NotesSum);
        //Note数を元に配列を初期化
        notes = new Note[NotesSum];
        Timetable = new float[NotesSum];
        //ファイルからノーツ情報を読み込む処理
        for(int i=0;i<NotesSum;i++){
            Debug.Log("i:"+i);
            noteobject = GameObject.Instantiate(Resources.Load("Prefabs/Sphere", typeof(GameObject))) as GameObject;
            note = noteobject.GetComponent<Note>();
            note.posx = newnotes[i].posx;
            note.posy = newnotes[i].posy;
            note.angle = newnotes[i].angle;
            note.scale = newnotes[i].scale;
            note.radiusBefore = newnotes[i].radiusBefore;
            note.DecisionTime = newnotes[i].DecisionTime;
            note.EmitTime = newnotes[i].EmitTime;
            note.RadiusIncrement = newnotes[i].RadiusIncrement;
            note.target = GameObject.Find(newnotes[i].TargetName);
            note.Init(noteobject);
            //note.Hidden();
            Timetable[NotesCount] = 5.0f;
            notes[NotesCount] = note;
        }
        //this.NoteDestroy(noteobject);
    }
    
    public GameObject getTargets(string TargetName){
        return GameObject.Find(TargetName);
    }
    
    private void NoteDestroy(GameObject noteobject){
        UnityEngine.Object.Destroy(noteobject);
    }
    
    public void StartManagement(){
         
        if(NotesCount == NotesSum){
        }else {
            audiotime = AudioManager.Instance.GetTimeBGM();
            if(audiotime >= Timetable[NotesCount]){      
                notes[NotesCount].Show();
                NotesCount++;
            } 
        }
	}
    
    
}
