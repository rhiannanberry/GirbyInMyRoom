// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/SquishyShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SquishY ("Squish Y", Float) = -3.1
		_SquishDistance ("Squish Distance", Float) = .2
		_SquishDistanceY ("Squish Distance Y", Float) = .2
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
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _SquishY;
			float _SquishDistance;
			float _SquishDistanceY;

			float4 getNewVertPosition( float4 p ) {
				float4 worldPosition = mul(unity_ObjectToWorld, p);
				//worldPosition.y += .1;
				return worldPosition;
			} 
			
			v2f vert (appdata v)
			{
				float4 bitangent = float4(cross(v.normal, v.tangent.xyz), 0);

				//bitangent.xyz = cross(v.normal, v.tangent.xyz); 
				//bitangent.w = 1.0;

				
				
				float4 worldPos = getNewVertPosition(v.vertex);
				float3 worldNorm = mul(unity_ObjectToWorld, v.normal);

				if(worldPos.y > _SquishY) {
					worldPos.y -= .1;
				} else { 
					float colliderYPos = _SquishY - _SquishDistance;
					//percent dist
					float yDiff = abs(_SquishY - worldPos.y);

					float yPercent = clamp(yDiff / _SquishDistance,0.0, 1.0);
					float squishRad = sin(yPercent * (3.14159) / 3.0);
					float squishY = sin((1.0-yPercent)  * (3.14159 / 3.0) + 0.5);
					//squish = 1 - saturate(squish);
					
					
					worldPos += (float4(worldNorm.x, 0, worldNorm.z ,0))*squishRad*.3;
					worldPos.y -= squishY*.15;
					worldPos.y = clamp(worldPos.y, colliderYPos, 1000.0);
					

				}

				worldPos = mul(2, worldPos);
				
				float4 worldPosTan = getNewVertPosition(v.vertex + v.tangent * 0.01);
				float4 worldPosBitan = getNewVertPosition(v.vertex + bitangent * 0.01);
			
				float4 newTan = (worldPosTan - worldPos);
				float4 newBitan = (worldPosBitan - worldPos);

				float3 newNormal = cross(newTan, newBitan);


				v2f o;
				o.vertex = mul(UNITY_MATRIX_VP, worldPos);
				o.normal = v.normal;//newNormal;
				//o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
