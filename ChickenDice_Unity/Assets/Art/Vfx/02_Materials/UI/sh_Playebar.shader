// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sh_playerbar"
{
	Properties
	{
		_PlayerNumber("PlayerNumber", Float) = 4
		_Passedevant("Passe devant", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_life("life", Range( 0 , 1)) = 0.5
		_life2("life2", Range( 0 , 1)) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow vertex:vertexDataFunc 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float _Passedevant;
		uniform float _PlayerNumber;
		uniform float _life;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _life2;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			v.vertex.xyz += ( ( ase_worldPos - _WorldSpaceCameraPos ) * _Passedevant );
		}

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float temp_output_13_0_g9 = _PlayerNumber;
			float4 color2_g9 = IsGammaSpace() ? float4(1,0.8612682,0.09019607,1) : float4(1,0.7129325,0.008568125,1);
			float4 color1_g9 = IsGammaSpace() ? float4(0.08018869,0.7634774,1,1) : float4(0.007218568,0.5437636,1,1);
			float4 color3_g9 = IsGammaSpace() ? float4(0.9607843,0.1803922,0.2972732,1) : float4(0.9130987,0.0273209,0.07189607,1);
			float4 ifLocalVar7_g9 = 0;
			if( temp_output_13_0_g9 > 1.0 )
				ifLocalVar7_g9 = color2_g9;
			else if( temp_output_13_0_g9 == 1.0 )
				ifLocalVar7_g9 = color1_g9;
			else if( temp_output_13_0_g9 < 1.0 )
				ifLocalVar7_g9 = color3_g9;
			float4 color4_g9 = IsGammaSpace() ? float4(0.09936813,0.7264151,0.1962294,1) : float4(0.009925043,0.4865309,0.03194208,1);
			float4 ifLocalVar12_g9 = 0;
			if( temp_output_13_0_g9 == 3.0 )
				ifLocalVar12_g9 = color4_g9;
			else
				ifLocalVar12_g9 = ifLocalVar7_g9;
			float4 temp_output_27_0 = ifLocalVar12_g9;
			float temp_output_37_0 = ( i.uv_texcoord.x + (0.02 + (_life - 1.0) * (0.96 - 0.02) / (0.0 - 1.0)) );
			float smoothstepResult38 = smoothstep( 0.98 , 1.0 , temp_output_37_0);
			float temp_output_40_0 = ( 1.0 - smoothstepResult38 );
			float clampResult39 = clamp( temp_output_40_0 , 0.0 , 1.0 );
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode28 = tex2D( _TextureSample0, uv_TextureSample0 );
			float4 lerpResult29 = lerp( float4( 0,0,0,0 ) , ( 1.0 * temp_output_27_0 * clampResult39 ) , tex2DNode28.g);
			float3 desaturateInitialColor80 = temp_output_27_0.rgb;
			float desaturateDot80 = dot( desaturateInitialColor80, float3( 0.299, 0.587, 0.114 ));
			float3 desaturateVar80 = lerp( desaturateInitialColor80, desaturateDot80.xxx, 0.7 );
			float clampResult81 = clamp( smoothstepResult38 , 0.0 , 1.0 );
			float smoothstepResult46 = smoothstep( 0.98 , 1.0 , ( i.uv_texcoord.x + (0.02 + (_life2 - 1.0) * (0.96 - 0.02) / (0.0 - 1.0)) ));
			float clampResult44 = clamp( ( ( 1.0 - smoothstepResult46 ) - temp_output_40_0 ) , 0.0 , 1.0 );
			float4 color33 = IsGammaSpace() ? float4(0.2138216,0.2138216,0.2924528,0) : float4(0.03757578,0.03757578,0.06955753,0);
			float4 lerpResult32 = lerp( float4( ( ( desaturateVar80 * 0.5 ) * tex2DNode28.g * ( clampResult81 - clampResult44 ) ) , 0.0 ) , color33 , tex2DNode28.r);
			float mulTime50 = _Time.y * 8.0;
			float clampResult54 = clamp( ( clampResult44 * (0.9 + (sin( mulTime50 ) - 0.0) * (1.0 - 0.9) / (1.0 - 0.0)) ) , 0.0 , 1.0 );
			float lerpResult48 = lerp( 0.0 , clampResult54 , tex2DNode28.g);
			float clampResult70 = clamp( lerpResult48 , 0.0 , 1.0 );
			float smoothstepResult57 = smoothstep( 0.98 , 1.0 , ( temp_output_37_0 + 0.04 ));
			float clampResult72 = clamp( ( tex2DNode28.b * ( temp_output_40_0 * smoothstepResult57 ) ) , 0.0 , 1.0 );
			float clampResult74 = clamp( ( clampResult70 - clampResult72 ) , 0.0 , 1.0 );
			float ifLocalVar76 = 0;
			if( _life > _life2 )
				ifLocalVar76 = clampResult70;
			else if( _life < _life2 )
				ifLocalVar76 = ( clampResult74 + clampResult72 );
			float clampResult75 = clamp( ifLocalVar76 , 0.0 , 1.0 );
			o.Emission = ( lerpResult29 + lerpResult32 + clampResult75 ).rgb;
			float clampResult66 = clamp( ( tex2DNode28.r + tex2DNode28.g + ifLocalVar76 ) , 0.0 , 1.0 );
			o.Alpha = clampResult66;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
-1920;36;1920;983;2323.647;-773.525;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;47;-3790.219,570.0483;Inherit;False;Property;_life2;life2;4;0;Create;True;0;0;False;0;1;0.772;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;36;-3866.727,115.46;Inherit;False;Property;_life;life;3;0;Create;True;0;0;False;0;0.5;0.405;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;45;-3153.872,574.6564;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0.02;False;4;FLOAT;0.96;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;35;-3071.987,113.0568;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0.02;False;4;FLOAT;0.96;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-3121.563,-94.8062;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-2849.918,-83.69273;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;-2882.041,451.7517;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;38;-2482.995,-35.23701;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.98;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;46;-2533.251,516.8629;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.98;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;50;-2949.546,1607.399;Inherit;False;1;0;FLOAT;8;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;40;-2167.017,-37.63141;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;43;-2202.775,481.2271;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;51;-2596.756,1576.429;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;55;-1972.655,403.4197;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;44;-1728.166,487.2263;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;52;-2369.195,1554.884;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.9;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-2887.046,919.757;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0.04;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;57;-2662.993,926.8141;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.98;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;53;-1739.551,1188.583;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;28;-1814.488,96.98267;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;7c96498d7f43dae49bc87a18344dc533;7c96498d7f43dae49bc87a18344dc533;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;54;-1478.531,1030.925;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;61;-2409.111,916.354;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-2139.171,877.9506;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;48;-1231.553,916.6077;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-2450.731,-497.1307;Inherit;False;Property;_PlayerNumber;PlayerNumber;0;0;Create;True;0;0;False;0;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;70;-983.5925,942.9225;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;72;-980.2487,1292.342;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;71;-722.6734,1016.985;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;27;-2125.094,-488.1857;Inherit;True;ColorPlayer;-1;;9;218e2e94ee9227a4c9271970f673a684;0;1;13;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-1429.037,-583.7104;Inherit;False;Constant;_Float2;Float 2;5;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;80;-1630.428,-750.2178;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.7;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;74;-551.4264,1248.631;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;81;-1584.425,-175.4494;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;73;-303.8814,1155.37;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-1195.726,-546.185;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.5;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;82;-1206.236,189.6508;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;39;-1883.976,-75.82526;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-1772.973,-602.3191;Inherit;False;Constant;_PowEmiss;PowEmiss;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;33;-1189.772,-376.0752;Inherit;False;Constant;_Color4;Color 4;3;0;Create;True;0;0;False;0;0.2138216,0.2138216,0.2924528,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-827.5781,-170.0417;Inherit;True;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldSpaceCameraPos;11;-26.67302,1127.839;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-1546.098,-441.7419;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ConditionalIfNode;76;-1169.982,631.0216;Inherit;True;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;10;65.02208,883.3956;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;14;508.74,1123.166;Inherit;False;Property;_Passedevant;Passe devant;1;0;Create;True;0;0;False;0;1;-0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;32;-661.403,-399.4081;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;29;-1078.343,-23.07031;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;75;-356.3633,901.2005;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;378.327,972.8387;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;65;-705.6115,626.613;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;617.554,959.9445;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-342.0882,83.78825;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;66;-363.4177,582.7264;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;810.5894,-29.0397;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;sh_playerbar;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;45;0;47;0
WireConnection;35;0;36;0
WireConnection;37;0;31;1
WireConnection;37;1;35;0
WireConnection;42;0;31;1
WireConnection;42;1;45;0
WireConnection;38;0;37;0
WireConnection;46;0;42;0
WireConnection;40;0;38;0
WireConnection;43;0;46;0
WireConnection;51;0;50;0
WireConnection;55;0;43;0
WireConnection;55;1;40;0
WireConnection;44;0;55;0
WireConnection;52;0;51;0
WireConnection;56;0;37;0
WireConnection;57;0;56;0
WireConnection;53;0;44;0
WireConnection;53;1;52;0
WireConnection;54;0;53;0
WireConnection;61;0;40;0
WireConnection;61;1;57;0
WireConnection;58;0;28;3
WireConnection;58;1;61;0
WireConnection;48;1;54;0
WireConnection;48;2;28;2
WireConnection;70;0;48;0
WireConnection;72;0;58;0
WireConnection;71;0;70;0
WireConnection;71;1;72;0
WireConnection;27;13;17;0
WireConnection;80;0;27;0
WireConnection;74;0;71;0
WireConnection;81;0;38;0
WireConnection;73;0;74;0
WireConnection;73;1;72;0
WireConnection;77;0;80;0
WireConnection;77;1;78;0
WireConnection;82;0;81;0
WireConnection;82;1;44;0
WireConnection;39;0;40;0
WireConnection;79;0;77;0
WireConnection;79;1;28;2
WireConnection;79;2;82;0
WireConnection;19;0;20;0
WireConnection;19;1;27;0
WireConnection;19;2;39;0
WireConnection;76;0;36;0
WireConnection;76;1;47;0
WireConnection;76;2;70;0
WireConnection;76;4;73;0
WireConnection;32;0;79;0
WireConnection;32;1;33;0
WireConnection;32;2;28;1
WireConnection;29;1;19;0
WireConnection;29;2;28;2
WireConnection;75;0;76;0
WireConnection;12;0;10;0
WireConnection;12;1;11;0
WireConnection;65;0;28;1
WireConnection;65;1;28;2
WireConnection;65;2;76;0
WireConnection;13;0;12;0
WireConnection;13;1;14;0
WireConnection;34;0;29;0
WireConnection;34;1;32;0
WireConnection;34;2;75;0
WireConnection;66;0;65;0
WireConnection;0;2;34;0
WireConnection;0;9;66;0
WireConnection;0;11;13;0
ASEEND*/
//CHKSM=34FCF2F7618A0EA8EAD6B2B325D20A4ECEF3A27B