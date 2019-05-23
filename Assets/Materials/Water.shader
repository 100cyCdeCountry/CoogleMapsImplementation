Shader "Custom/Water" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Bumpmap (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_NormalIntensity ("Normal Intensity", float) = 1.0
		_WaveSpeed ("Wave Speed", float) = 1.0
		_LightColor("Light Color", Color) = (1,1,1,1)
		_Shinethreshold("Shine Threshold", float) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		fixed4 _LightColor;
		float _NormalIntensity;
		float _Shinethreshold;
		half _WaveSpeed;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = _Color;
			

			//Wave normals
			half timeSpeed = _Time.x * _WaveSpeed;
			half4 normal1 = tex2D(_MainTex, IN.uv_MainTex + half2(timeSpeed, 0));
			half4 normal2 = tex2D(_MainTex, IN.uv_MainTex * half2(-1,1) + half2(1, timeSpeed));
			half3 normal = UnpackNormal((normal1 + normal2) / 2.f);
			normal.z /= _NormalIntensity;
			
			float willShine = normal.y > _Shinethreshold;
			float beNormal = 1 - willShine;
			o.Albedo = willShine * _LightColor + beNormal * c.rgb;
			o.Alpha = willShine + beNormal * c.a;
			o.Metallic = willShine + _Metallic * beNormal;
			o.Smoothness = willShine + _Glossiness * beNormal;
			

			o.Normal = normal;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
