Shader "Hidden/Hilight Blur Shader"
{
	Properties
	{
		_OutlineTex ("Texture", 2D) = "white" {}
		_RenderTex ("Texture", 2D) = "white" {}
		_WScreen ("W Screen", Float) = 0
		_HScreen ("H Screen", Float) = 0
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
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

			sampler2D _OutlineTex;
			half4 _OutlineTex_ST;

			sampler2D _RenderTex;
			half4 _RenderTex_ST;

			float _WScreen;
			float _HScreen;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float2 v1 = float2(1/_WScreen, 0)*2;
				float2 v2 = float2(0, 1/_HScreen)*2;
				fixed4 col = tex2D(_OutlineTex, i.uv + v1) + tex2D(_OutlineTex, i.uv - v1) + tex2D(_OutlineTex, i.uv + v2) + tex2D(_OutlineTex, i.uv - v2);
				return col/4 - tex2D(_OutlineTex, i.uv) + tex2D(_RenderTex, fixed2(i.uv.x, 1-i.uv.y));
			}
			ENDCG
		}
	}
}
