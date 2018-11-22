// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/Custom Outline (Experimental)" {

Properties {
    [PerRendererData] _MainTex ("Base (RGB)", 2D) = "white" {}
    // Save on drawcalls when using a Sprite Renderer.
    // Use and safely animate the vertex color provided by
    // the renderer to set the outline color.
}

SubShader {
    Tags {
        "Queue"="Transparent"
        "RenderType"="Transparent"
    }

    Blend SrcAlpha OneMinusSrcAlpha
    ZWrite Off
    Cull Off

    Pass {
        Name "Outline"

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag
        #include "UnityCG.cginc"
        ENDCG
    }
}

FallBack "Sprite/Default"

CGINCLUDE
struct vertexData
{
    float4 vertex   : POSITION;
    float4 texcoord : TEXCOORD0;
    fixed4 color    : COLOR;
};

struct v2f
{
    float4 pos      : SV_POSITION;
    float2 uv       : TEXCOORD0;
    fixed4 color    : COLOR;
};

v2f vert(vertexData v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.color = v.color;
    o.uv = v.texcoord;
    return o;
}

sampler2D _MainTex;
float4 _MainTex_TexelSize;

fixed4 frag(v2f i) : SV_TARGET
{
    fixed4 outColor = tex2D(_MainTex, i.uv);

    // Use this to check for a dropped pixel, so to speak. Best for pixel art sprites.
    // But, you'll also note this effect does not work so great with anti-aliased images.

    if (outColor.a != 0) {
        fixed alpha_up = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y)).a;
        fixed alpha_down = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
        fixed alpha_right = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0)).a;
        fixed alpha_left = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0)).a;

        fixed result = alpha_up * alpha_down * alpha_right * alpha_left;
        outColor = lerp(i.color, outColor, result);
    }

    // So try this, or a variant of this, instead of the if(condition).
    //outColor.a = lerp(0, 1, result * 100 * outColor.a);

    return outColor;
}
ENDCG

}