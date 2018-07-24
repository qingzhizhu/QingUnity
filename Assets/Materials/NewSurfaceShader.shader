﻿//Shader "Custom/NewSurfaceShader" {
//	Properties {
//		_Color ("Color", Color) = (1,1,1,1)
//		_MainTex ("Albedo (RGB)", 2D) = "white" {}
//		_Glossiness ("Smoothness", Range(0,1)) = 0.5
//		_Metallic ("Metallic", Range(0,1)) = 0.0
//	}
//	SubShader {
//		Tags { "RenderType"="Opaque" }
//		LOD 200
//
//		CGPROGRAM
//		// Physically based Standard lighting model, and enable shadows on all light types
//		#pragma surface surf Standard fullforwardshadows
//
//		// Use shader model 3.0 target, to get nicer looking lighting
//		#pragma target 3.0
//
//		sampler2D _MainTex;
//
//		struct Input {
//			float2 uv_MainTex;
//		};
//
//		half _Glossiness;
//		half _Metallic;
//		fixed4 _Color;
//
//		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
//		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
//		// #pragma instancing_options assumeuniformscaling
//		UNITY_INSTANCING_BUFFER_START(Props)
//			// put more per-instance properties here
//		UNITY_INSTANCING_BUFFER_END(Props)
//
//		void surf (Input IN, inout SurfaceOutputStandard o) {
//			// Albedo comes from a texture tinted by color
//			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
//			o.Albedo = c.rgb;
//			// Metallic and smoothness come from slider variables
//			o.Metallic = _Metallic;
//			o.Smoothness = _Glossiness;
//			o.Alpha = c.a;
//		}
//		ENDCG
//	}
//	FallBack "Diffuse"
//}


Shader "Custom/Screen Cutout"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	_ClipX0 ("Clip x0", float) = 0
	_ClipY0 ("Clip y0", float) = 0
	_ClipX1 ("Clip x1", float) = 0
	_ClipY1 ("Clip y1", float) = 0
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0 //屏幕坐标必须使用3.0
	    #include "UnityCG.cginc"
 
            // note: no SV_POSITION in this struct
            struct v2f {
                float2 uv : TEXCOORD0;
            };
 
            v2f vert (
                float4 vertex : POSITION, // vertex position input
                float2 uv : TEXCOORD0, // texture coordinate input
                out float4 outpos : SV_POSITION // clip space position output
                )
            {
                v2f o;
                o.uv = uv;
                outpos = UnityObjectToClipPos(vertex);
                return o;
            }
 
            sampler2D _MainTex;
			float _ClipX0;
			float _ClipY0;
			float _ClipX1;
			float _ClipY1;
 
            fixed4 frag (v2f i, UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
            {
				if(screenPos.x > _ClipX0 && screenPos.x < _ClipX1 && screenPos.y > _ClipY0 && screenPos.y < _ClipY1)
					clip(-1);//结束渲染
 
                fixed4 c = tex2D (_MainTex, i.uv);
                return c;
            }
            ENDCG
        }
    }
}