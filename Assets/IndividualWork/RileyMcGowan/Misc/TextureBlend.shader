Shader "Custom/Texture Blend" {
         Properties {
             //Blend bar for transition
             _Blend ("Texture Blend", Range(0,1)) = 0.0
             //Default values
             _Color ("Color", Color) = (1,1,1,1)
             _Glossiness ("Smoothness", Range(0,1)) = 0.5
             //Base textures to alternate
             _MainTex ("Albedo (RGB)", 2D) = "white" {}
             _MainTex2 ("Albedo 2 (RGB)", 2D) = "white" {}
             //Added basic normal, metallic, and ao
             _AOTex ("AO", 2D) = "white" {}
             [Normal] _BlankNormal ("Blank Normal", 2D) = "white" {}
             [Metallic] _BlankMetallic ("Blank Metallic", 2D) = "white" {}
         }
         SubShader {
             Tags { "RenderType"="Opaque" }
             LOD 200
            
             CGPROGRAM
             #pragma surface surf Standard fullforwardshadows
             #pragma target 3.0

             //Textures and other
             sampler2D _MainTex;
             sampler2D _MainTex2;
             sampler2D _AOTex;
             sampler2D _BlankNormal;
             sampler2D _BlankMetallic;
      
             struct Input {
                 float2 uv_MainTex;
                 float2 uv_MainTex2;
                 float2 uv_Metallic;
                 float2 uv_Normal;
                 float2 uv_AOTex;
             };

             //Usable variables for blend and defaults
             half _Blend;
             half _Glossiness;
             half _Metallic;
             fixed4 _Color;
      
             void surf (Input IN, inout SurfaceOutputStandard o) {
                 // Albedo colour is determined by "_Color" // This is the main diffuse and saves it as "c" to be used below
                 fixed4 c = lerp (tex2D (_MainTex, IN.uv_MainTex), tex2D (_MainTex2, IN.uv_MainTex2), _Blend) * _Color;
                 //Basic normal and metallic
                 fixed4 BlankNormal = tex2D(_BlankNormal, IN.uv_Normal);
                 fixed4 BlankMetallic = tex2D(_BlankMetallic, IN.uv_Metallic);
                 fixed4 AOTex = tex2D(_AOTex, IN.uv_AOTex.xy).r;
                 //Set the diffused value to the "Albedo" of our output
                 o.Albedo = c.rgb;
                 o.Occlusion = AOTex;
                 //Set metallic and normal for shader
                 o.Metallic = (BlankMetallic.rgb);
                 o.Normal = (BlankNormal.rgb);
                 // Smoothness comes from slider variable
                 o.Smoothness = _Glossiness;
                 o.Alpha = c.a;
             }
             ENDCG
         }
         FallBack "Diffuse"
     }