// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sh_hit_sph"
{
	Properties
	{
		_emisspow("emiss pow", Float) = 4
		_Offset("Offset", Vector) = (-4.36,0,0,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Speed("Speed", Float) = 0.5
		_Tilling("Tilling", Vector) = (1,1,0,0)
		_addzer("addzer", Float) = 0
		_opaflm("opaflm", Float) = 0.27
		_Power("Power", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float _emisspow;
		uniform sampler2D _TextureSample0;
		uniform float _Speed;
		uniform float2 _Tilling;
		uniform float2 _Offset;
		uniform float _Power;
		uniform float _opaflm;
		uniform float _addzer;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = ( i.vertexColor * _emisspow ).rgb;
			float2 uv_TexCoord11 = i.uv_texcoord * _Tilling + _Offset;
			float2 panner12 = ( ( _Time.y * _Speed ) * float2( 1,0.4 ) + uv_TexCoord11);
			float4 temp_cast_1 = ((-0.31 + (i.vertexColor.a - 1.0) * (0.5 - -0.31) / (0.0 - 1.0))).xxxx;
			float4 clampResult22 = clamp( ( tex2D( _TextureSample0, panner12 ) - temp_cast_1 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 temp_cast_2 = (_Power).xxxx;
			float4 clampResult26 = clamp( pow( clampResult22 , temp_cast_2 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float clampResult15 = clamp( pow( ( ( 1.0 - i.uv_texcoord.y ) + _addzer ) , 1.0 ) , 0.0 , 0.9 );
			float clampResult18 = clamp( pow( ( i.uv_texcoord.y + _addzer ) , 0.99 ) , 0.0 , 0.9 );
			float clampResult28 = clamp( ( pow( ( clampResult15 * clampResult18 ) , 2.82 ) * 34.35 ) , 0.0 , 1.0 );
			float4 clampResult32 = clamp( ( ( clampResult26 * _opaflm ) * clampResult28 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 clampResult39 = clamp( clampResult32 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Alpha = clampResult39.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.vertexColor = IN.color;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
-1920;36;1920;983;1517.605;249.8212;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;1;-4444.67,-283.0695;Inherit;False;1919.28;569.5598;Valo Flame;16;23;22;21;19;16;15;14;13;12;11;10;9;8;6;5;3;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleTimeNode;3;-4307.094,-38.5743;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-4291.105,29.83242;Inherit;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0.5;-0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-4582.566,145.1321;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;8;-4419.462,-106.0986;Inherit;False;Property;_Offset;Offset;1;0;Create;True;0;0;False;0;-4.36,0;-4.36,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;7;-4450.604,-239.0219;Inherit;False;Property;_Tilling;Tilling;4;0;Create;True;0;0;False;0;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-4121.105,-39.16754;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-4094.535,-196.2514;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-4272.412,371.9806;Inherit;False;Property;_addzer;addzer;5;0;Create;True;0;0;False;0;0;0.21;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;5;-4285.132,137.3487;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-3860.329,205.6484;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;-0.09;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;10;-3988.993,27.08392;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.09;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;31;-3023.085,-565.9319;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;12;-3783.333,-233.0696;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0.4;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;17;-3618.87,463.002;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0.99;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;16;-3529.867,-231.9278;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;eb08a18ff338d8946b83d7415f687a08;eb08a18ff338d8946b83d7415f687a08;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;13;-3728.86,1.083927;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;41;-3129.558,199.3907;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;-0.31;False;4;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;15;-3501.906,52.38892;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;18;-3364.281,542.047;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;19;-3062.447,-105.142;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;22;-2807.545,-152.9692;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-2689.921,111.1772;Inherit;False;Property;_Power;Power;7;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-3019.793,664.8783;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;23;-2530.921,-133.8228;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;24;-2677.202,701.7248;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;2.82;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;26;-2298.635,31.54135;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;25;-2149.504,294.4866;Inherit;False;Property;_opaflm;opaflm;6;0;Create;True;0;0;False;0;0.27;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-2294.739,728.1575;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;34.35;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;28;-1906.517,732.9113;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-1926.011,168.907;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1629.829,470.151;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;32;-1234.49,292.6824;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-2113.787,-397.0258;Inherit;False;Property;_emisspow;emiss pow;0;0;Create;True;0;0;False;0;4;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;39;-450.724,235.8419;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-1509.546,-245.0567;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;sh_hit_sph;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;9;0;3;0
WireConnection;9;1;6;0
WireConnection;11;0;7;0
WireConnection;11;1;8;0
WireConnection;5;0;2;2
WireConnection;14;0;2;2
WireConnection;14;1;4;0
WireConnection;10;0;5;0
WireConnection;10;1;4;0
WireConnection;12;0;11;0
WireConnection;12;1;9;0
WireConnection;17;0;14;0
WireConnection;16;1;12;0
WireConnection;13;0;10;0
WireConnection;41;0;31;4
WireConnection;15;0;13;0
WireConnection;18;0;17;0
WireConnection;19;0;16;0
WireConnection;19;1;41;0
WireConnection;22;0;19;0
WireConnection;20;0;15;0
WireConnection;20;1;18;0
WireConnection;23;0;22;0
WireConnection;23;1;21;0
WireConnection;24;0;20;0
WireConnection;26;0;23;0
WireConnection;27;0;24;0
WireConnection;28;0;27;0
WireConnection;29;0;26;0
WireConnection;29;1;25;0
WireConnection;30;0;29;0
WireConnection;30;1;28;0
WireConnection;32;0;30;0
WireConnection;39;0;32;0
WireConnection;37;0;31;0
WireConnection;37;1;34;0
WireConnection;0;2;37;0
WireConnection;0;9;39;0
WireConnection;0;10;39;0
ASEEND*/
//CHKSM=24920D84E44B039DD760D50C34BEA66372A18D9D