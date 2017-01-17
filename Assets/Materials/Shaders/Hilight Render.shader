Shader "Unlit/Hilight Render"
{
	Properties
	{
		_HilightColor ("Hilight Color", Color) = (1, 1, 0, 1)
	}
	SubShader
	{
		Tags { "Entity"="Cube" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			fixed4 _HilightColor;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = _HilightColor;
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
