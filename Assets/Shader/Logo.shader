// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Logo"
{
	Properties
	{
		_MainTex("Grass Texture", 2D) = "white" {}
	}

		SubShader
		{
			Tags{ "Queue" = "AlphaTest" "RenderType" = "TransparentCutout" }
			Pass
			{
				CGPROGRAM
				#pragma vertex vert 
				#pragma fragment frag 

				#include "UnityCG.cginc" 
				#include "Lighting.cginc"

				sampler2D _MainTex;
				float4 _MainTex_ST;

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

				v2f vert(appdata v)
				{
					v2f o;

					v.vertex.y += sin(3.1415926 * _Time * 10.0f + v.uv.x * 3) * 0.5f;

					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.uv, _MainTex);
					
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = tex2D(_MainTex, i.uv);

					clip(col.a - 0.2f);

					return col;
				}
				ENDCG
			}
		}
		FallBack "Mobile/Diffuse"
}
