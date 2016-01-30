using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarManager : MonoBehaviour {

    //classes
    public Scrollbar MusicBar;
    public MaskMove Mask;
    //variables
    public GameObject mask;
    public float length = 0;
    private float audiotime;
    private float audiolength;
    private float lapserate;
    
    public void Init(){
        mask = GameObject.Find("BarBackGround");
        Mask = mask.GetComponent<MaskMove>();
        audiolength = AudioManager.Instance.LengthBGM();
    }
    public void IncrementBar(){
        audiotime = AudioManager.Instance.GetTimeBGM();
        lapserate = audiotime / audiolength;
        Mask.setX(lapserate);
    }
}
