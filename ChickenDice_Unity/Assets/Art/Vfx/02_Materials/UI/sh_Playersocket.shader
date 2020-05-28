// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sh_playersocket"
{
	Properties
	{
		_Passedevant("Passe devant", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Gemis("Gemis", Float) = 1
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
		uniform float _Gemis;
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
			float4 color95 = IsGammaSpace() ? float4(0.345098,0.3708933,1,0) : float4(0.09758735,0.1133747,1,0);
			float4 color94 = IsGammaSpace() ? float4(0.9433962,0.9433962,0.9433962,0) : float4(0.8760344,0.8760344,0.8760344,0);
			float4 color85 = IsGammaSpace() ? float4(0.3962264,0.3447917,0.3158597,0) : float4(0.130239,0.09740814,0.08133871,0);
			float4 ifLocalVar86 = 0;
			if( _Gemis > 1.0 )
				ifLocalVar86 = color95;
			else if( _Gemis == 1.0 )
				ifLocalVar86 = color94;
			else if( _Gemis < 1.0 )
				ifLocalVar86 = color85;
			float4 color97 = IsGammaSpace() ? float4(1,0.5117006,0.2122642,0) : float4(1,0.2250313,0.03705544,0);
			float4 ifLocalVar96 = 0;
			if( _Gemis == 3.0 )
				ifLocalVar96 = color97;
			else
				ifLocalVar96 = ifLocalVar86;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 tex2DNode82 = tex2D( _TextureSample0, uv_TextureSample0 );
			float4 lerpResult98 = lerp( float4( 0,0,0,0 ) , ( 1.5 * ifLocalVar96 ) , tex2DNode82.r);
			float4 color33 = IsGammaSpace() ? float4(0.2138216,0.2138216,0.2924528,0) : float4(0.03757578,0.03757578,0.06955753,0);
			float4 lerpResult83 = lerp( float4( 0,0,0,0 ) , color33 , tex2DNode82.g);
			o.Emission = ( lerpResult98 + lerpResult83 ).rgb;
			float clampResult92 = clamp( ( ( tex2DNode82.r + tex2DNode82.g ) * 2.0 ) , 0.0 , 1.0 );
			o.Alpha = clampResult92;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
44;488;1920;995;2282.299;555.406;1.851944;True;True
Node;AmplifyShaderEditor.ColorNode;85;-1587.762,-42.94982;Inherit;False;Constant;_Color5;Color 4;3;0;Create;True;0;0;False;0;0.3962264,0.3447917,0.3158597,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;87;-1550.077,-493.2147;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;94;-1738.547,-245.595;Inherit;False;Constant;_Color0;Color 0;3;0;Create;True;0;0;False;0;0.9433962,0.9433962,0.9433962,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;95;-1795.597,-429.3501;Inherit;False;Constant;_Color1;Color 1;3;0;Create;True;0;0;False;0;0.345098,0.3708933,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;91;-1576.812,-611.3835;Inherit;False;Property;_Gemis;Gemis;2;0;Create;True;0;0;False;0;1;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;89;-1543.445,-356.1138;Inherit;False;Constant;_Float2;Float 2;2;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;97;-1145.942,-31.09802;Inherit;False;Constant;_Color2;Color 2;3;0;Create;True;0;0;False;0;1,0.5117006,0.2122642,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;86;-1109.149,-472.0762;Inherit;True;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ConditionalIfNode;96;-830.2784,-169.0272;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;101;-726.3047,-318.429;Inherit;False;Constant;_Float3;Float 3;3;0;Create;True;0;0;False;0;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;82;-956.5502,479.7245;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;-1;6c76ab506dbc533458fccdc9dd0b72d8;6c76ab506dbc533458fccdc9dd0b72d8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;10;65.02208,883.3956;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.WorldSpaceCameraPos;11;-26.67302,1127.839;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;100;-626.9471,-232.5437;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;84;-294.0679,512.8419;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;33;-1064.195,259.0227;Inherit;False;Constant;_Color4;Color 4;3;0;Create;True;0;0;False;0;0.2138216,0.2138216,0.2924528,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;2.459961,616.1811;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;508.74,1123.166;Inherit;False;Property;_Passedevant;Passe devant;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;83;-374.3363,121.925;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;98;-511.7213,-111.4729;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;12;378.327,972.8387;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;617.554,959.9445;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ClampOpNode;92;176.9628,574.3141;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;88;-1542.338,-425.7699;Inherit;False;Constant;_Float1;Float 1;2;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;99;-11.59753,67.67078;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;810.5894,-29.0397;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;sh_playersocket;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;86;0;91;0
WireConnection;86;1;87;0
WireConnection;86;2;95;0
WireConnection;86;3;94;0
WireConnection;86;4;85;0
WireConnection;96;0;91;0
WireConnection;96;1;89;0
WireConnection;96;2;86;0
WireConnection;96;3;97;0
WireConnection;96;4;86;0
WireConnection;100;0;101;0
WireConnection;100;1;96;0
WireConnection;84;0;82;1
WireConnection;84;1;82;2
WireConnection;93;0;84;0
WireConnection;83;1;33;0
WireConnection;83;2;82;2
WireConnection;98;1;100;0
WireConnection;98;2;82;1
WireConnection;12;0;10;0
WireConnection;12;1;11;0
WireConnection;13;0;12;0
WireConnection;13;1;14;0
WireConnection;92;0;93;0
WireConnection;99;0;98;0
WireConnection;99;1;83;0
WireConnection;0;2;99;0
WireConnection;0;9;92;0
WireConnection;0;11;13;0
ASEEND*/
//CHKSM=F3D73E46D45F2B03F0F3C0536CEC99F40FC2EADF