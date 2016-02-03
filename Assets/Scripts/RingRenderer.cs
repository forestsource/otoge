using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

/// <summary>
/// リングの構造体
/// </summary>
public struct Ring
{
	/// <summary>
	/// 位置
	/// </summary>
	public Vector3 pos;

	/// <summary>
	/// 回転角
	/// </summary>
	public Quaternion rotate;

	/// <summary>
	/// スケール
	/// </summary>
	public Vector3 scale;

	/// <summary>
	/// 内円の割合
	/// </summary>
	public float innerPercentage;

	/// <summary>
	/// 扇型の角度
	/// </summary>
	public float fanAngle;

	/// <summary>
	/// 色
	/// </summary>
	public Color color;
}

/// <summary>
/// リングのレンダラ
/// エディットモードでも実行する
/// </summary>
[ExecuteInEditMode()]
public class RingRenderer : MonoBehaviour {

	/// <summary>
	/// シェーダ
	/// </summary>
	public Shader shader;

	/// <summary>
	/// マテリアル
	/// </summary>
	Material material;

	/// <summary>
	/// リングのデータを渡すための演算バッファ
	/// </summary>
	ComputeBuffer buffer;

	/// <summary>
	/// リングのリスト
	/// </summary>
	List<Ring> rings;

	/// <summary>
	/// 初期化
	/// </summary>
	void Awake () {
		if (material == null)
		{
			material = new Material(shader);
			material.hideFlags = HideFlags.DontSave;
		}
		if (buffer == null)
			buffer = new ComputeBuffer(10000, Marshal.SizeOf(typeof(Ring)));
		if (rings == null)
			rings = new List<Ring>();

	}

	/// <summary>
	/// 破棄
	/// </summary>
	void OnDisable()
	{
		if (buffer != null)
			buffer.Dispose();
		buffer = null;
	}

	/// <summary>
	/// 更新処理
	/// </summary>
	void Update()
	{
		Awake();
		rings.Clear();
	}

	/// <summary>
	/// レンダリング
	/// </summary>
	void OnRenderObject() {
		
		Awake();
		// レンダリングを開始
		material.SetPass(0);

		material.SetBuffer("Rings", buffer);

		buffer.SetData(rings.ToArray());
		Graphics.DrawProcedural(MeshTopology.Points, rings.Count);
		//スケールを変更する。
		///Debug.Log(rings[0].scale);
	}

	/// <summary>
	/// 現在のタイムステップにおけるリングを追加する
	/// </summary>
	/// <param name="ring">Ring.</param>
	public void Push(Ring ring)
	{
		rings.Add(ring);
	}
	
}