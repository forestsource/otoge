using UnityEngine;
using System.Collections;

/// <summary>
/// リングのGameObject用スクリプト
/// </summary>
[ExecuteInEditMode()]
public class Note3D : MonoBehaviour {
	/// <summary>
	/// 色
	/// </summary>
	public Color color = Color.black;
	
	/// <summary>
	/// 円の速度
	/// </summary>
	public float circleSpeed = 0.01f;
	
	/// <summary>
	/// 円の初期スケール
	/// </summary>
	public float initsScale = 0.01f;
	
	/// <summary>
	/// 円破棄のスケール
	/// </summary>
	public float destroyScale = 3f;
	
	/// <summary>
	/// 初期化
	/// </summary>
	void Start () {
		
		//スケールを初期化
		this.transform.SetLocalScaleXYZ(initsScale);
		
	}
	
	/// <summary>
	/// 更新
	/// </summary>
	
	void Update(){
		this.transform.AddLocalScaleX( circleSpeed );
		this.transform.AddLocalScaleY( circleSpeed );
		//this.transform.AddLocalScaleZ( circleSpeed );
		//Debug.Log(this.transform.localScale);		
		//一定の大きさで自分を消す
		if(this.transform.localScale[1] >= destroyScale){
			this.transform.SetLocalScaleXYZ(initsScale);
			Object.Destroy(this);
		}
	}
}