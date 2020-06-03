// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sh_shield"
{
	Properties
	{
		_emisspow("emiss pow", Float) = 4
		_Offset("Offset", Vector) = (-4.36,0,0,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Speed("Speed", Float) = 0.5
		_Tilling("Tilling", Vector) = (1,1,0,0)
		_addzer("addzer", Float) = 0
		_opaflm("opaflm", Float) = 0
		_Power("Power", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
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

		uniform sampler2D _TextureSample0;
		uniform float _Speed;
		uniform float2 _Tilling;
		uniform float2 _Offset;
		uniform float _addzer;
		uniform float _Power;
		uniform float _opaflm;
		uniform float _emisspow;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float3 desaturateInitialColor52 = i.vertexColor.rgb;
			float desaturateDot52 = dot( desaturateInitialColor52, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar52 = lerp( desaturateInitialColor52, desaturateDot52.xxx, 0.1 );
			float2 uv_TexCoord12 = i.uv_texcoord * _Tilling + _Offset;
			float2 panner13 = ( ( _Time.y * _Speed ) * float2( 0.2,-1 ) + uv_TexCoord12);
			float4 tex2DNode17 = tex2D( _TextureSample0, panner13 );
			float temp_output_14_0 = pow( ( i.uv_texcoord.y + _addzer ) , 0.58 );
			float clampResult16 = clamp( temp_output_14_0 , 0.0 , 0.9 );
			float4 temp_cast_2 = (clampResult16).xxxx;
			float4 clampResult23 = clamp( ( tex2DNode17 - temp_cast_2 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 temp_cast_3 = (_Power).xxxx;
			float4 clampResult27 = clamp( pow( clampResult23 , temp_cast_3 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float clampResult19 = clamp( pow( ( ( 1.0 - i.uv_texcoord.y ) + _addzer ) , 0.99 ) , 0.0 , 0.9 );
			float clampResult29 = clamp( ( pow( ( clampResult16 * clampResult19 ) , 2.82 ) * 34.35 ) , 0.0 , 1.0 );
			float4 clampResult33 = clamp( ( ( clampResult27 * _opaflm ) * clampResult29 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 temp_output_50_0 = ( ( clampResult33 * i.vertexColor.a * 0.5 ) + 0.5 );
			float smoothstepResult43 = smoothstep( 0.99 , 1.0 , ( i.uv_texcoord.y + 0.1 ));
			float4 lerpResult46 = lerp( ( float4( desaturateVar52 , 0.0 ) + temp_output_50_0 ) , ( i.vertexColor * _emisspow ) , smoothstepResult43);
			o.Emission = lerpResult46.rgb;
			float4 temp_cast_5 = (smoothstepResult43).xxxx;
			float4 lerpResult48 = lerp( temp_output_50_0 , temp_cast_5 , smoothstepResult43);
			float4 clampResult2 = clamp( lerpResult48 , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Alpha = clampResult2.r;
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
-1920;32;1920;877;2476.347;636.1909;1.835275;True;True
Node;AmplifyShaderEditor.CommentaryNode;1;-4323.315,-186.3436;Inherit;False;1919.28;569.5598;Valo Flame;18;37;36;24;23;22;20;17;16;15;14;13;12;11;10;9;7;6;4;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-4453.058,220.1175;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-4169.75,126.5584;Inherit;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0.5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;8;-4329.249,-142.2959;Inherit;False;Property;_Tilling;Tilling;4;0;Create;True;0;0;False;0;1,1;1,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;9;-4298.107,-9.372653;Inherit;False;Property;_Offset;Offset;1;0;Create;True;0;0;False;0;-4.36,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;5;-4142.904,446.966;Inherit;False;Property;_addzer;addzer;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;4;-4185.739,58.15171;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-3999.75,57.55848;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-3973.179,-99.52535;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;11;-3867.638,123.8099;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.09;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;6;-4084.478,263.9748;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;14;-3607.506,97.80994;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0.58;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-3738.975,302.3744;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;-0.09;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;13;-3661.978,-136.3436;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;16;-3354.551,63.11493;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;17;-3408.513,-135.2018;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;eb08a18ff338d8946b83d7415f687a08;a38e720966545124f8f7ca6598781892;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;18;-3489.363,537.9873;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0.99;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;20;-2954.092,-89.41606;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;19;-3234.774,617.0323;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-2568.567,207.9032;Inherit;False;Property;_Power;Power;7;0;Create;True;0;0;False;0;0;0.85;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-2890.286,739.8636;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;23;-2686.191,-56.24314;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;24;-2409.567,-37.09673;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;25;-2547.695,776.7102;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;2.82;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;27;-2169.129,106.5267;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-2019.998,369.472;Inherit;False;Property;_opaflm;opaflm;6;0;Create;True;0;0;False;0;0;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-2165.232,803.1428;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;34.35;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;29;-1777.01,807.8966;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1796.504,243.8924;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-1500.322,545.1364;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;32;-1439.586,-38.60749;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;33;-1130.5,468.3182;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-928.2062,729.0054;Inherit;False;Constant;_Float1;Float 1;8;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-1551.179,-469.4645;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-753.3094,424.5523;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-564.3488,683.8243;Inherit;False;Constant;_Float2;Float 2;8;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-1301.436,-340.6976;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;52;-1120.7,86.35207;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SmoothstepOpNode;43;-1149.436,-443.6976;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.99;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;-470.5964,496.7987;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-1984.281,-322.0404;Inherit;False;Property;_emisspow;emiss pow;0;0;Create;True;0;0;False;0;4;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;48;-357.3705,-400.1639;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-1037.207,-82.21335;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-673.5442,218.2357;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;36;-3390.015,314.5666;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.77;False;2;FLOAT;5.88;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;37;-2945.933,363.2547;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-2651.992,544.2578;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;2.08;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;2;-140.2079,43.68714;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-2442.883,411.4735;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;40;-1849.162,481.214;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;46;-626.255,-35.45049;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;273.3101,-192.3155;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;sh_shield;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;5;True;True;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;4;0
WireConnection;10;1;7;0
WireConnection;12;0;8;0
WireConnection;12;1;9;0
WireConnection;11;0;3;2
WireConnection;11;1;5;0
WireConnection;6;0;3;2
WireConnection;14;0;11;0
WireConnection;15;0;6;0
WireConnection;15;1;5;0
WireConnection;13;0;12;0
WireConnection;13;1;10;0
WireConnection;16;0;14;0
WireConnection;17;1;13;0
WireConnection;18;0;15;0
WireConnection;20;0;17;0
WireConnection;20;1;16;0
WireConnection;19;0;18;0
WireConnection;21;0;16;0
WireConnection;21;1;19;0
WireConnection;23;0;20;0
WireConnection;24;0;23;0
WireConnection;24;1;22;0
WireConnection;25;0;21;0
WireConnection;27;0;24;0
WireConnection;28;0;25;0
WireConnection;29;0;28;0
WireConnection;30;0;27;0
WireConnection;30;1;26;0
WireConnection;31;0;30;0
WireConnection;31;1;29;0
WireConnection;33;0;31;0
WireConnection;34;0;33;0
WireConnection;34;1;32;4
WireConnection;34;2;49;0
WireConnection;45;0;42;2
WireConnection;52;0;32;0
WireConnection;43;0;45;0
WireConnection;50;0;34;0
WireConnection;50;1;51;0
WireConnection;48;0;50;0
WireConnection;48;1;43;0
WireConnection;48;2;43;0
WireConnection;38;0;32;0
WireConnection;38;1;35;0
WireConnection;54;0;52;0
WireConnection;54;1;50;0
WireConnection;36;0;14;0
WireConnection;37;0;17;0
WireConnection;37;1;36;0
WireConnection;2;0;48;0
WireConnection;39;0;37;0
WireConnection;39;1;41;0
WireConnection;40;0;39;0
WireConnection;46;0;54;0
WireConnection;46;1;38;0
WireConnection;46;2;43;0
WireConnection;0;2;46;0
WireConnection;0;9;2;0
ASEEND*/
//CHKSM=EC21918AD10D137917294BFACD92116FEA611E46