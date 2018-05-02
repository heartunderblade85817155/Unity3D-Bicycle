﻿Shader "Unlit/WholeRoom"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ShowPercent ("ShowPercent", Float) = 0.4
		_SetAlpha ("Alpha", Float) = 0.0
	}
	SubShader
	{
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _ShowPercent;
			float _SetAlpha;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				col.a = _SetAlpha;

				if (i.uv.x <= 1 - _ShowPercent)
				{
					col.a = 0.0f;
				}
				return col;
			}
			ENDCG
		}
	}
}
