using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OutJson{
    public string MusicName;
    public int NotesSum;
    
    public void set(string name,int count){
        MusicName = name;
        NotesSum = count;
    }
    
    public string getMusicName(){
        return MusicName;
    }
    
    public int getNotesSum(){
        return NotesSum;
    }
}