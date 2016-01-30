using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject barmanager;
    private BarManager BarManager;
    private NotesManager NotesManager;
    private bool barmanagerStart = true;
	void Start () {
        //BGM Start
        AudioManager.Instance.PlaySE (AUDIO.SE_BIRD);
        AudioManager.Instance.PlayBGM (AUDIO.BGM_MENU, AudioManager.BGM_FADE_SPEED_RATE_HIGH);
        
        //Bar management
        barmanager = GameObject.Find("MusicBar");
        BarManager = barmanager.GetComponent<BarManager>();
        
        //Notes management
        NotesManager = new NotesManager();
        NotesManager.Init();
	}
	
	void Update () {
        if(AudioManager.Instance.isPlayingBGM() && barmanagerStart){
            BarManager.Init();
            barmanagerStart = false;
            
        }
        BarManager.IncrementBar();
        
        NotesManager.StartManagement();
	}
}
