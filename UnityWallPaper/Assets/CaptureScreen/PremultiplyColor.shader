Shader "Unlit/PremultiplyColor"
{
    Properties
    {
        _MainTex ("CameraRT", 2D) = "white" {}//当前RT颜色
        _MaskTex ("Mask", 2D) = "white" {}//模板
    }
    SubShader
    {
        Tags 
        {
            "RenderPipeline"="UniversalPipeline"//这是一个URP Shader！
            "Queue"="Transparent+10"
            "RenderType"="Transparent"
        }
        LOD 100
        ZWrite Off
        Cull Off
        // Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLINCLUDE
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

             //CG中核心代码库 #include "UnityCG.cginc"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
            CBUFFER_END
            ENDHLSL

            HLSLPROGRAM //CGPROGRAM
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_MaskTex);
            SAMPLER(sampler_MaskTex);

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;//实际距离
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };
            v2f vert (appdata v)
            {
                v2f o;
                VertexPositionInputs positionInputs = GetVertexPositionInputs(v.vertex.xyz);
                float4 positionCS = positionInputs.positionCS;
                o.vertex = positionCS;
                o.uv =v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 col = float4(0,0,0,1);
                return col;
				float4 color = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,i.uv);
                float4 mask = SAMPLE_TEXTURE2D(_MaskTex,sampler_MaskTex,i.uv);
                //手动premultiply
                col.rgb = color.rgb * mask.a;
                col.a = mask.a;
                return col;
            }
            ENDHLSL  //ENDCG          
        }
    }
}
