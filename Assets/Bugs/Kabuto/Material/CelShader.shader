Shader "Custom/Kyle/CelShader" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MetallicGlossMap("Metallic Gloss Map", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			
			#pragma surface surf Cel fullforwardshadows vertex:vert addshadow;
			#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		sampler2D _MainTex;
		sampler2D _MetallicGlossMap;
		sampler2D _Normal;

		void vert(inout appdata_full v)
		{
			//float a = sin(v.vertex.x + _Time.z) * 0.15;
			//v.vertex.y += a;
		}


		half4 LightingCel(SurfaceOutput s, half3 lightDir, half atten) {
			half3 nDotL = dot(lightDir, s.Normal);
			half3 lightAmount = nDotL * .5;
			lightAmount = (step(.2, lightAmount)/ 5) + .2;

			half4 c;
			c.rgb = _LightColor0.rgb * s.Albedo * lightAmount;
			c.a = 1;
			return c;
		}

		void surf (Input IN, inout SurfaceOutput o) {

			float2 uv = IN.uv_MainTex;
			//uv.y += _Time.z;
			o.Albedo = tex2D(_MainTex, uv).rgb;

			float2 metallicSmoothness = tex2D(_MetallicGlossMap, uv).ra;
			//o.Metallic = metallicSmoothness.r;
			//o.Smoothness = metallicSmoothness.g;
			o.Normal = UnpackNormal(tex2D(_Normal, uv));

		}

		ENDCG
	}
	FallBack "Diffuse"
}
