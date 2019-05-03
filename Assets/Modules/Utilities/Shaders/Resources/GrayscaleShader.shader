// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/GrayscaleShader"
{
	Properties
	{
		_MainTexture ("Texture Image", 2D) = "white" {} 
		_Brightness ("Brightness", Range(1, 10)) = 1
	}

	SubShader
	{
		Tags
		{
		    "Queue" = "Geometry"
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Brightness;

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _MainTex_ST;

            v2f vert (appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 outColor = tex2D (_MainTex, i.uv);
                //float grascaleColor = (outColor[0]+outColor[1]+outColor[2]) / (_Brightness * 3);
                return tex2D (_MainTex, i.uv);//float4(grascaleColor, grascaleColor, grascaleColor, outColor[3]);
            }
            ENDCG

		}
	}
}
