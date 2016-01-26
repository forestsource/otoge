Shader "Custom/RingShader" {
	SubShader {
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
        Pass {
	        CGPROGRAM
	        
	        // シェーダーモデルは5.0を指定
	        #pragma target 5.0
	        
	        // シェーダー関数を設定 
	        #pragma vertex vert
			#pragma geometry geom
	        #pragma fragment frag
	         
	        #include "UnityCG.cginc"
	        
	        #define PI 3.1415926535
	        
	        // 頂点シェーダからの出力
	        struct VSOut {
	            float4 pos : SV_POSITION;
	            float4 col : COLOR;
				float id : TEXCOORD0;
			};
	        
	        // 構造体
	        struct Ring
			{
				float3 pos;
				float4 rotate;
				float3 scale;
				float innerPercentage;
				float fanAngle;
				float4 col;
			};
			
			// 構造化バッファ
        	StructuredBuffer<Ring> Rings;
        	
	        // 頂点シェーダ
			VSOut vert (uint id : SV_VertexID)
	       	{
	       		// idをx座標に代入、そのまま帰す
	            VSOut output;
	            output.pos = float4(Rings[id].pos, 1);
	            output.col = Rings[id].col;
	            output.id = id;
	            
	            return output;
	       	}
	       	
	       	float3 qTransform(float4 q, float3 v){ 
				return v + 2.0 * cross(cross(v, q.xyz ) - q.w * v, q.xyz);
			}
	       	
	       	// ジオメトリシェーダ
		   	[maxvertexcount(64)]
		   	void geom (point VSOut input[1], inout TriangleStream<VSOut> outStream)
		   	{
		     	VSOut output;
		      	float4 pos = input[0].pos; 
		      	float4 col = input[0].col; 
		      	float id = input[0].id; 
		      	float3 scale = Rings[id].scale;
		      	float4 rotate = Rings[id].rotate;
		      	
		      	for(int i = 0; i < 32; i++)
		      	{
			      	float angle = i / 31.0f * Rings[id].fanAngle;
			      	float3 posOffset = float3(cos(angle) * scale.x, sin(angle) * scale.y, 0);
			      	
			        output.col = col;
	            	output.id = id;
		      		{
			      		// 頂点座標を計算し、射影変換
			          	float3 ringPosOffset = qTransform(rotate, posOffset);
			          	output.pos = mul (UNITY_MATRIX_VP, pos + float4(ringPosOffset, 0));
			          	
			          	// ストリームに頂点を追加
				      	outStream.Append (output);
			      	}
		      		{
			      		// 頂点座標を計算し、射影変換
			          	float3 ringPosOffset = qTransform(rotate, posOffset * Rings[id].innerPercentage);
			          	output.pos = mul (UNITY_MATRIX_VP, pos + float4(ringPosOffset, 0));
			          	
			          	// ストリームに頂点を追加
				      	outStream.Append (output);
			      	}
			      	
		      	}
		      	
		      	// 連続したトライアングルを終了
		      	outStream.RestartStrip();
		   	}
			
			// ピクセルシェーダー
	        fixed4 frag (VSOut i) : COLOR
	        {
	        	// 色を決める
	            return i.col;
	        }
	         
	        ENDCG
	     } 
     }
 }