Shader "TextureCombiner/CombineShader"
{
    Properties
    {
        _MainTex("MainTex", 2D) = "white" {}
        _Tex0("TexR", 2D) = "black" {}
        _Tex1("TexG", 2D) = "black" {}
        _Tex2("TexB", 2D) = "black" {}
        _Tex3("TexA", 2D) = "black" {}
        _Mask0("MaskR", Vector) = (0, 0, 0, 0)
        _Mask1("MaskG", Vector) = (0, 0, 0, 0)
        _Mask2("MaskB", Vector) = (0, 0, 0, 0)
        _Mask3("MaskA", Vector) = (0, 0, 0, 0)
        _Add("Add", Vector) = (0, 0, 0, 0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _Tex0;
            sampler2D _Tex1;
            sampler2D _Tex2;
            sampler2D _Tex3;
            float4 _Mask0;
            float4 _Mask1;
            float4 _Mask2;
            float4 _Mask3;
            float4 _Add;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float GetChannelColor(float4 col, float4 mask)
            {
                return (col.r * mask.r) + (col.g * mask.g) + (col.b * mask.b) + (col.a * mask.a);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = fixed4(0, 0, 0, 0);

                col.r = clamp(GetChannelColor(tex2D(_Tex0, i.uv).rgba, _Mask0) + _Add.r, 0, 1);
                col.g = clamp(GetChannelColor(tex2D(_Tex1, i.uv).rgba, _Mask1) + _Add.g, 0, 1);
                col.b = clamp(GetChannelColor(tex2D(_Tex2, i.uv).rgba, _Mask2) + _Add.b, 0, 1);
                col.a = clamp(GetChannelColor(tex2D(_Tex3, i.uv).rgba, _Mask3) + _Add.a, 0, 1);

                return col;
            }
            ENDCG
        }
    }
}
