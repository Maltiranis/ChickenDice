// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sh_playerslot"
{
	Properties
	{
		_PlayerNumber("PlayerNumber", Float) = 4
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Passedevant("Passe devant", Float) = 1
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
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;

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
			o.Emission = ( 1.0 * ifLocalVar12_g9 ).rgb;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			o.Alpha = tex2D( _TextureSample0, uv_TextureSample0 ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
16;180;1735;839;1707.668;684.3318;2.286198;True;True
Node;AmplifyShaderEditor.WorldPosInputsNode;10;-355.5348,734.1658;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceCameraPos;11;-447.2299,978.6089;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;17;-1151.089,-34.13546;Inherit;False;Property;_PlayerNumber;PlayerNumber;0;0;Create;True;0;0;False;0;4;4;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;-42.22986,823.6089;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;14;88.18317,973.9358;Inherit;False;Property;_Passedevant;Passe devant;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-473.3318,-139.324;Inherit;False;Constant;_PowEmiss;PowEmiss;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;27;-825.4515,-25.1904;Inherit;True;ColorPlayer;-1;;9;218e2e94ee9227a4c9271970f673a684;0;1;13;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;196.9972,810.7147;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-246.4562,21.25336;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;18;-651.9825,276.2361;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;bf0d8f766a3a92246a4d2b979c765141;770eb0a6f27243340bc4722fdc738886;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;480.475,-29.0397;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;sh_playerslot;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;10;0
WireConnection;12;1;11;0
WireConnection;27;13;17;0
WireConnection;13;0;12;0
WireConnection;13;1;14;0
WireConnection;19;0;20;0
WireConnection;19;1;27;0
WireConnection;0;2;19;0
WireConnection;0;9;18;0
WireConnection;0;11;13;0
ASEEND*/
//CHKSM=29056FCC4F3EF6B212538650699A784A369D5EA3