using UnityEngine;
using System.Collections;

/// <summary>
/// リングのGameObject用スクリプト
/// </summary>
[ExecuteInEditMode()]
public class Notes_Ring : MonoBehaviour {
	private bool outside;
	/// <summary>
	/// レンダラ
	/// </summary>
	RingRenderer ringRenderer;
	
	/// <summary>
	/// 内円の割合
	/// </summary>
	[Range(0, 1)]
	public float innerPercentage = 0.8f;
	
	/// <summary>
	/// 扇型の角度
	/// </summary>
	[Range(0, Mathf.PI * 2 + 0.01f)]
	public float fanAngle = 1.5f;
	
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
	public float initsScale = 0.001f;
	
	/// <summary>
	/// 円破棄のスケール
	/// </summary>
	public float destroyScale = 0.3f;
	
	/// <summary>
	/// 初期化
	/// </summary>
	void Start () {
		// レンダラを探す
		ringRenderer = GameObject.Find("RingRenderer").GetComponent<RingRenderer>();
		
		//スケールを初期化
		this.transform.SetLocalScaleXYZ(initsScale);
		
		//変数初期化
		outside = true;
	}
	
	/// <summary>
	/// 更新
	/// </summary>
	void LateUpdate () {
		PushToRenderer();
	}
	
	/// <summary>
	/// レンダラに追加する
	/// </summary>
	void PushToRenderer()
	{
		var ring = new Ring();
		
		ring.pos = transform.position;
		ring.rotate = transform.rotation;
		ring.scale = transform.localScale;
		ring.innerPercentage = innerPercentage;
		ring.fanAngle = fanAngle;
		ring.color = color;
		
		ringRenderer.Push(ring);
	}
	
	void Update(){
		if(outside)
		{
			this.transform.AddLocalScaleX( circleSpeed );
			this.transform.AddLocalScaleY( circleSpeed );
			this.transform.AddLocalScaleZ( circleSpeed );
			//Debug.Log(this.transform.localScale);		
		}
		//一定の大きさで自分を消す
		if(this.transform.localScale[1] >= destroyScale){
			this.transform.SetLocalScaleXYZ(initsScale);
			Object.Destroy(this);
		}
	}
}