﻿Shader "Custom/ColorChange"
{

    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)

        [Header(Blank Textures)]
        _MainTex ("Blank Albedo", 2D) = "white" {}
        [Normal] _BlankNormal ("Blank Normal", 2D) = "white" {}
        [Metallic] _BlankMetallic ("Blank Metallic", 2D) = "white" {}

        [Header(Final Textures)]
        _FinalAlbedo ("Final Albedo", 2D) = "white" {}
        [Normal] _FinalNormal ("Final Normal", 2D) = "white" {}
        [Metallic] _FinalMetallic ("Final Metallic", 2D) = "white" {}

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Normal ("Normal", Range(0,1)) = 1.0
        [Header(Filter Texture)]
        _SplashTex ("FilterTexture", 2D) = "white" {}

        _Alpha ("Alpha", Range(0,1)) = 0

    }
    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0
        #pragma alpha: blend

        sampler2D _FinalAlbedo;
        sampler2D _FinalNormal;
        sampler2D _FinalMetallic;

        sampler2D _MainTex;
        sampler2D _BlankNormal;
        sampler2D _BlankMetallic;

        sampler2D _SplashTex;

        struct Input
        {
            float2 uv_MainTex;
            float2 uv_SplashTex;
        };

        half _Glossiness;
        half _Metallic;
        half _Normal;
        fixed4 _Alpha;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
        // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 BlankAlbedo = tex2D(_MainTex, IN.uv_MainTex);
            fixed4 BlankNormal = tex2D(_BlankNormal, IN.uv_MainTex);
            fixed4 BlankMetallic = tex2D(_BlankMetallic, IN.uv_MainTex);

            fixed4 FinalAlbedo = tex2D(_FinalAlbedo, IN.uv_MainTex);
            fixed4 FinalNormal = tex2D(_FinalNormal, IN.uv_MainTex);
            fixed4 FinalMetallic = tex2D(_FinalMetallic, IN.uv_MainTex);

            fixed4 SplashColor = tex2D(_SplashTex, IN.uv_SplashTex);

            fixed4 c = _Color;

            o.Albedo = (BlankAlbedo.rgb * SplashColor.rgb + FinalAlbedo * (1 - SplashColor.rgb));
            // Metallic and smoothness come from slider variables
            o.Metallic = (BlankMetallic.rgb * SplashColor.rgb + FinalMetallic * (1 - SplashColor.rgb));
            o.Normal = (BlankNormal.rgb * SplashColor.rgb + FinalNormal * (1 - SplashColor.rgb));
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}