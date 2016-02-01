using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject barmanager;
    private BarManager BarManager;
    private NotesManager NotesManager;
    private AutoNotes AutoNotes;
    private UIManager UIManager;
    private bool barmanagerStart = true;
	void Start () {
        //BGM Start
        AudioManager.Instance.PlaySE (AUDIO.SE_BIRD);
        AudioManager.Instance.PlayBGM (AUDIO.BGM_ONTHEEARTH, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
        
        //Bar management
        barmanager = GameObject.Find("MusicBar");
        BarManager = barmanager.GetComponent<BarManager>();
        
        //ScoreManager
        UIManager = new UIManager();
        
        if (Application.loadedLevelName == "circle_music") {
            //Notes management
            NotesManager = new NotesManager();
            NotesManager.Init();
            NotesManager.CreateNotes();
        } else if(Application.loadedLevelName == "create_notes"){
            AutoNotes = new AutoNotes();
            AutoNotes.Init();
        }
	}
	
	void Update () {
        if(AudioManager.Instance.isPlayingBGM() && barmanagerStart){
            BarManager.Init();
            barmanagerStart = false;   
        }
        BarManager.IncrementBar();
        
        if (Application.loadedLevelName == "circle_music") {
            NotesManager.StartManagement();
        } else if(Application.loadedLevelName == "create_notes"){
            AutoNotes.GetInput();
        }
        
	}
}
