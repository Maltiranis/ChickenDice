// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sh_playercd"
{
	Properties
	{
		_Passedevant("Passe devant", Float) = 1
		_TextureSample1("Texture Sample 0", 2D) = "white" {}
		_Cd("Cd", Range( 0 , 1)) = 0
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
		uniform float _Cd;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;

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
			float4 color97 = IsGammaSpace() ? float4(0.9433962,0.9433962,0.9433962,0) : float4(0.8760344,0.8760344,0.8760344,0);
			float4 color90 = IsGammaSpace() ? float4(0.1595764,0.1595764,0.2075472,0) : float4(0.02187714,0.02187714,0.03550519,0);
			float smoothstepResult101 = smoothstep( 0.99 , 1.0 , ( ( 1.0 - i.uv_texcoord.y ) + (0.25 + (_Cd - 0.0) * (0.75 - 0.25) / (1.0 - 0.0)) ));
			float clampResult105 = clamp( ( 1.0 - smoothstepResult101 ) , 0.0 , 1.0 );
			float4 lerpResult98 = lerp( color97 , color90 , clampResult105);
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float4 tex2DNode87 = tex2D( _TextureSample1, uv_TextureSample1 );
			float4 lerpResult93 = lerp( float4( 0,0,0,0 ) , lerpResult98 , tex2DNode87.r);
			o.Emission = lerpResult93.rgb;
			float clampResult94 = clamp( ( tex2DNode87.r * 2.0 ) , 0.0 , 1.0 );
			o.Alpha = clampResult94;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
-1920;36;1920;983;3240.05;390.1395;2.419735;True;True
Node;AmplifyShaderEditor.RangedFloatNode;103;-3148.024,332.0408;Inherit;False;Property;_Cd;Cd;2;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;99;-3052.487,-52.59474;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;107;-2699.073,181.3963;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.25;False;4;FLOAT;0.75;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;106;-2688.443,-37.74927;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;102;-2388.702,80.38843;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;101;-1938.252,82.28607;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.99;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;104;-1644.216,104.2761;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;87;-1057.162,138.8099;Inherit;True;Property;_TextureSample1;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;6c76ab506dbc533458fccdc9dd0b72d8;6c76ab506dbc533458fccdc9dd0b72d8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldSpaceCameraPos;11;-26.67302,1127.839;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ClampOpNode;105;-1422.85,107.2081;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;90;-1482.38,-353.8516;Inherit;False;Constant;_Color5;Color 4;3;0;Create;True;0;0;False;0;0.1595764,0.1595764,0.2075472,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;10;65.02208,883.3956;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;97;-1594.665,-649.2963;Inherit;False;Constant;_Color1;Color 0;3;0;Create;True;0;0;False;0;0.9433962,0.9433962,0.9433962,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;98;-875.1299,-474.4609;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;378.327,972.8387;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;14;508.74,1123.166;Inherit;False;Property;_Passedevant;Passe devant;0;0;Create;True;0;0;False;0;1;-0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-180.6754,261.2701;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;617.554,959.9445;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;93;-527.4759,-400.1673;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;94;161.2083,285.6197;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;810.5894,-29.0397;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;sh_playercd;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;107;0;103;0
WireConnection;106;0;99;2
WireConnection;102;0;106;0
WireConnection;102;1;107;0
WireConnection;101;0;102;0
WireConnection;104;0;101;0
WireConnection;105;0;104;0
WireConnection;98;0;97;0
WireConnection;98;1;90;0
WireConnection;98;2;105;0
WireConnection;12;0;10;0
WireConnection;12;1;11;0
WireConnection;91;0;87;1
WireConnection;13;0;12;0
WireConnection;13;1;14;0
WireConnection;93;1;98;0
WireConnection;93;2;87;1
WireConnection;94;0;91;0
WireConnection;0;2;93;0
WireConnection;0;9;94;0
WireConnection;0;11;13;0
ASEEND*/
//CHKSM=DEFF7B767E6F386D919D1144707DC0CD723EA721