// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sh_ray"
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
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
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
		uniform float _addzer;
		uniform float _Power;
		uniform float _opaflm;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			o.Emission = ( i.vertexColor * _emisspow ).rgb;
			float2 uv_TexCoord13 = i.uv_texcoord * _Tilling + _Offset;
			float2 panner11 = ( ( _Time.y * _Speed ) * float2( 0.2,1 ) + uv_TexCoord13);
			float4 tex2DNode40 = tex2D( _TextureSample0, panner11 );
			float temp_output_23_0 = pow( ( ( 1.0 - i.uv_texcoord.y ) + _addzer ) , 1.0 );
			float clampResult21 = clamp( temp_output_23_0 , 0.0 , 0.9 );
			float4 temp_cast_1 = (clampResult21).xxxx;
			float4 clampResult27 = clamp( ( tex2DNode40 - temp_cast_1 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 temp_cast_2 = (_Power).xxxx;
			float4 clampResult31 = clamp( pow( clampResult27 , temp_cast_2 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float clampResult69 = clamp( pow( ( i.uv_texcoord.y + _addzer ) , 0.99 ) , 0.0 , 0.9 );
			float clampResult80 = clamp( ( pow( ( clampResult21 * clampResult69 ) , 2.82 ) * 34.35 ) , 0.0 , 1.0 );
			float4 clampResult60 = clamp( ( ( clampResult31 * _opaflm ) * clampResult80 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 clampResult64 = clamp( ( clampResult60 * i.vertexColor.a ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			o.Alpha = clampResult64.r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
-1920;36;1920;983;1341.022;604.1211;1.882836;True;True
Node;AmplifyShaderEditor.CommentaryNode;41;-2996.758,-194.1828;Inherit;False;1919.28;569.5598;Valo Flame;15;24;23;21;38;13;11;40;27;20;51;56;57;58;93;98;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-3126.501,212.2782;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;56;-2859.182,50.31247;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-2816.347,439.1267;Inherit;False;Property;_addzer;addzer;5;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;98;-2837.22,226.2355;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;-2843.193,118.7192;Inherit;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;59;-3002.692,-150.1351;Inherit;False;Property;_Tilling;Tilling;4;0;Create;True;0;0;False;0;1,1;1,0.2;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;38;-2971.55,-17.2119;Inherit;False;Property;_Offset;Offset;1;0;Create;True;0;0;False;0;-4.36,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-2673.193,49.71924;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-2541.081,115.9707;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;-0.09;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;-2646.622,-107.3646;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;11;-2335.421,-144.1828;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.2,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;23;-2280.949,89.9707;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;68;-2412.418,294.5352;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;-0.09;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;21;-2027.994,55.27568;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;40;-2081.956,-143.041;Inherit;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;-1;eb08a18ff338d8946b83d7415f687a08;a38e720966545124f8f7ca6598781892;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;70;-2162.806,530.1481;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0.99;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;69;-1908.217,609.1931;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.9;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;20;-1627.535,-97.25531;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-1563.729,732.0244;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-1242.01,200.064;Inherit;False;Property;_Power;Power;7;0;Create;True;0;0;False;0;0;0.85;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;27;-1359.634,-64.08238;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;91;-1083.01,-44.93597;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;78;-1221.138,768.8709;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;2.82;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-693.4404,361.6327;Inherit;False;Property;_opaflm;opaflm;6;0;Create;True;0;0;False;0;0;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;31;-842.5715,98.68748;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;-838.6751,795.3036;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;34.35;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;80;-450.4525,800.0574;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-469.9468,236.0531;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-173.765,537.2971;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexColorNode;63;607.4189,479.8558;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;60;221.574,359.8285;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;862.9385,401.9056;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-657.7234,-329.8797;Inherit;False;Property;_emisspow;emiss pow;0;0;Create;True;0;0;False;0;4;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;51;-2063.458,306.7273;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0.77;False;2;FLOAT;5.88;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;52;-1619.376,355.4154;Inherit;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-296.8982,-372.645;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;-1116.326,403.6343;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;64;1030.149,53.83915;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;53;-522.6052,473.3748;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-1325.435,536.4186;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;2.08;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1303.331,-384.882;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;sh_ray;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.29;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;98;0;3;2
WireConnection;57;0;56;0
WireConnection;57;1;58;0
WireConnection;24;0;98;0
WireConnection;24;1;73;0
WireConnection;13;0;59;0
WireConnection;13;1;38;0
WireConnection;11;0;13;0
WireConnection;11;1;57;0
WireConnection;23;0;24;0
WireConnection;68;0;3;2
WireConnection;68;1;73;0
WireConnection;21;0;23;0
WireConnection;40;1;11;0
WireConnection;70;0;68;0
WireConnection;69;0;70;0
WireConnection;20;0;40;0
WireConnection;20;1;21;0
WireConnection;66;0;21;0
WireConnection;66;1;69;0
WireConnection;27;0;20;0
WireConnection;91;0;27;0
WireConnection;91;1;93;0
WireConnection;78;0;66;0
WireConnection;31;0;91;0
WireConnection;71;0;78;0
WireConnection;80;0;71;0
WireConnection;74;0;31;0
WireConnection;74;1;77;0
WireConnection;79;0;74;0
WireConnection;79;1;80;0
WireConnection;60;0;79;0
WireConnection;65;0;60;0
WireConnection;65;1;63;4
WireConnection;51;0;23;0
WireConnection;52;0;40;0
WireConnection;52;1;51;0
WireConnection;16;0;63;0
WireConnection;16;1;17;0
WireConnection;54;0;52;0
WireConnection;54;1;55;0
WireConnection;64;0;65;0
WireConnection;53;0;54;0
WireConnection;0;2;16;0
WireConnection;0;9;64;0
ASEEND*/
//CHKSM=672434A654E3B66A018AAC51CE7034A1172EF04C