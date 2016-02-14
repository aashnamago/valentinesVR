Shader "FX/Cartoon Explosion/Ring Particle"
{
	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_TintColor ("Tint", Color) = (1,1,1,0.5)
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _TintColor;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _TintColor;

				return OUT;
			}

			sampler2D _MainTex;
			
			fixed SampleMask(float2 texcoord, fixed a)
			{
				texcoord = texcoord/a - (0.5/a - 0.5) * float2(1, 1);
				
				if (texcoord.x < 0 || texcoord.x > 1 || texcoord.y < 0 || texcoord.y > 1)
					return 0;
				
				return tex2D(_MainTex, texcoord).a;
			}
			
			fixed4 frag(v2f IN) : SV_Target
			{
				fixed a = IN.color.a < 1 ? SampleMask(IN.texcoord, 1 - IN.color.a) : 0;
				IN.color.a = 1;
			
				fixed4 c = tex2D(_MainTex, IN.texcoord) * IN.color;
				
				c.a -= a;
				
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
