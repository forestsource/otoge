using UnityEngine;
using System.Collections;
using System;

public class NotesManager{
    private float audiotime;
    private float[] Timetable;
    private Note[] notes;
    private Note note;
    private GameObject noteobject;
    private int NotesSum;
    private int NotesCount;

	public void Init () {
        this.CreateNotes();
        NotesSum = 0;
        NotesCount = 0;
	}
    
	public void CreateNotes(){
        //Note数を取り込む
        NotesSum =1;
        //Note数を元に配列を初期化
        notes = new Note[NotesSum];
        Timetable = new float[NotesSum];    
        //ファイルからノーツ情報を読み込む処理
        noteobject = GameObject.Instantiate(Resources.Load("Prefabs/Sphere", typeof(GameObject))) as GameObject;
        note = noteobject.GetComponent<Note>();
        note.posx = 1.0f;
        note.posy = 1.0f;
        note.angle = 30f;
        note.DecisionTime = 1.0f;
        note.EmitTime = 5.0f;
        note.color = Color.black;
        note.Init(noteobject);
        //note.Hidden();
        Timetable[NotesCount] = 5.0f;
        notes[NotesCount] = note;
        this.NoteDestroy(noteobject);
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
