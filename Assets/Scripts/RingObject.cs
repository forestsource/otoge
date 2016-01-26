using UnityEngine;
using System.Collections;

/// <summary>
/// リングのGameObject用スクリプト
/// </summary>
[ExecuteInEditMode()]
public class RingObject : MonoBehaviour {
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
	public float circleSpeed = (float)0.01;

	/// <summary>
	/// 初期化
	/// </summary>
	void Start () {
		// レンダラを探す
		ringRenderer = GameObject.Find("RingRenderer").GetComponent<RingRenderer>();
		
		//スケールを初期化
		this.transform.SetLocalScaleXYZ((float)0.1);
		
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

		}else{
			this.transform.AddLocalScaleX( -circleSpeed );
			this.transform.AddLocalScaleY( -circleSpeed );
			this.transform.AddLocalScaleZ( -circleSpeed );
			//Debug.Log(this.transform.localScale);
		}
		//outside 評価
		if(this.transform.localScale[1] >= (float)2.0)
		{
			outside = false;
		}
		else if(this.transform.localScale[1] <= (float)0.2)
		{
			outside = true;
		}
	}
}