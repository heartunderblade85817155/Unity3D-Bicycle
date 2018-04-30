﻿Shader "Unlit/TowerAlphaController"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_AlphaBlend ("ControllerAlpha", Float) = 0
	}
	SubShader
	{

		Pass
		{
			Tags 
			{
				"RenderType" = "Transparent"
				"Queue" = "Transparent"
			}

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
			float _AlphaBlend;
			
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
				
				if (col.a <= 0.2)
				{
					discard;
				}
				
				return fixed4(col.xyz, _AlphaBlend);
			}
			ENDCG
		}
	}
}
