// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

Shader "Mobile/Grayscale Colored Diffuse" {
Properties {
    
    _Brightness("Brightness", Range(0.1, 2)) = 1
    _Color("Color", Color) = (1, 1, 1, 1)
    _MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 10

CGPROGRAM
#pragma surface surf Lambert noforwardadd

sampler2D _MainTex;
float _Brightness;
fixed4 _Color;

struct Input {
    float2 uv_MainTex;
};

void surf (Input IN, inout SurfaceOutput o) {
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
    fixed3 rgb = c.rgb;
    fixed gray= (rgb.r+rgb.g+rgb.b)/3;
    rgb.r = gray;
    rgb.g = gray;
    rgb.b = gray;
    
    o.Albedo = rgb * _Brightness * _Color;
    o.Alpha = c.a;
}
ENDCG
}

Fallback "Mobile/Diffuse"
}
