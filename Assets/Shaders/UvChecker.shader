Shader "Unlit/NormalChecker"
{
    Properties
    {
        _mainTex("Main Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _mainTex;

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : normal;
            };

            struct Interpolators
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
            };

            Interpolators vert (MeshData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = v.normal;
                o.uv = v.uv;
                //o.normal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag (Interpolators i) : SV_Target
            {
                return fixed4(tex2D(_mainTex, i.uv));
                return fixed4(i.uv, 1, 1);
            }
            ENDCG
        }
    }
}
