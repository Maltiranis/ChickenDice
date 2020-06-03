// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sh_crack"
{
	Properties
	{
		_t_crack("t_crack", 2D) = "white" {}
		_Scale("Scale", Range( -1 , 0)) = 0.11
		_curvatureu("curvature u", Range( 0 , 100)) = 17.3
		_curvaturev("curvature v", Range( 0 , 30)) = 6.45
		_Texture0("Texture 0", 2D) = "white" {}
		_addX("addX", Float) = 0
		_addY("addY", Float) = 0
		_Color0("Color 0", Color) = (0,0,0,0)
		_Color1("Color 1", Color) = (0,0,0,0)
		_pow("pow", Float) = 0
		_ade("ade", Float) = 0
		_Color2("Color 2", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[Header(Parallax Occlusion Mapping)]
		_CurvFix("Curvature Bias", Range( 0 , 1)) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		ZWrite On
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float3 viewDir;
			INTERNAL_DATA
			float3 worldNormal;
			float3 worldPos;
		};

		uniform float4 _Color0;
		uniform float4 _Color2;
		uniform float4 _Color1;
		uniform float _ade;
		uniform sampler2D _t_crack;
		uniform float _addX;
		uniform float _addY;
		uniform sampler2D _Texture0;
		uniform float _Scale;
		uniform float _CurvFix;
		uniform float _curvatureu;
		uniform float _curvaturev;
		uniform float4 _Texture0_ST;
		uniform float _pow;


		inline float2 POM( sampler2D heightMap, float2 uvs, float2 dx, float2 dy, float3 normalWorld, float3 viewWorld, float3 viewDirTan, int minSamples, int maxSamples, float parallax, float refPlane, float2 tilling, float2 curv, int index )
		{
			float3 result = 0;
			int stepIndex = 0;
			int numSteps = ( int )lerp( (float)maxSamples, (float)minSamples, saturate( dot( normalWorld, viewWorld ) ) );
			float layerHeight = 1.0 / numSteps;
			float2 plane = parallax * ( viewDirTan.xy / viewDirTan.z );
			uvs += refPlane * plane;
			float2 deltaTex = -plane * layerHeight;
			float2 prevTexOffset = 0;
			float prevRayZ = 1.0f;
			float prevHeight = 0.0f;
			float2 currTexOffset = deltaTex;
			float currRayZ = 1.0f - layerHeight;
			float currHeight = 0.0f;
			float intersection = 0;
			float2 finalTexOffset = 0;
			while ( stepIndex < numSteps + 1 )
			{
				result.z = dot( curv, currTexOffset * currTexOffset );
				currHeight = tex2Dgrad( heightMap, uvs + currTexOffset, dx, dy ).r * ( 1 - result.z );
				if ( currHeight > currRayZ )
				{
					stepIndex = numSteps + 1;
				}
				else
				{
					stepIndex++;
					prevTexOffset = currTexOffset;
					prevRayZ = currRayZ;
					prevHeight = currHeight;
					currTexOffset += deltaTex;
					currRayZ -= layerHeight * ( 1 - result.z ) * (1+_CurvFix);
				}
			}
			int sectionSteps = 4;
			int sectionIndex = 0;
			float newZ = 0;
			float newHeight = 0;
			while ( sectionIndex < sectionSteps )
			{
				intersection = ( prevHeight - prevRayZ ) / ( prevHeight - currHeight + currRayZ - prevRayZ );
				finalTexOffset = prevTexOffset + intersection * deltaTex;
				newZ = prevRayZ - intersection * layerHeight;
				newHeight = tex2Dgrad( heightMap, uvs + finalTexOffset, dx, dy ).r;
				if ( newHeight > newZ )
				{
					currTexOffset = finalTexOffset;
					currHeight = newHeight;
					currRayZ = newZ;
					deltaTex = intersection * deltaTex;
					layerHeight = intersection * layerHeight;
				}
				else
				{
					prevTexOffset = finalTexOffset;
					prevHeight = newHeight;
					prevRayZ = newZ;
					deltaTex = ( 1 - intersection ) * deltaTex;
					layerHeight = ( 1 - intersection ) * layerHeight;
				}
				sectionIndex++;
			}
			#ifdef UNITY_PASS_SHADOWCASTER
			if ( unity_LightShadowBias.z == 0.0 )
			{
			#endif
				if ( result.z > 1 )
					clip( -1 );
			#ifdef UNITY_PASS_SHADOWCASTER
			}
			#endif
			return uvs + finalTexOffset;
		}


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			o.Normal = float3(0,0,1);
			float mulTime33 = _Time.y * 3.0;
			float temp_output_34_0 = sin( mulTime33 );
			float clampResult43 = clamp( temp_output_34_0 , 0.0 , 1.0 );
			float4 lerpResult35 = lerp( _Color0 , _Color2 , clampResult43);
			float2 appendResult15 = (float2(( i.uv_texcoord.x + _addX ) , ( i.uv_texcoord.y + _addY )));
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float2 appendResult8 = (float2(_curvatureu , _curvaturev));
			float2 OffsetPOM2 = POM( _Texture0, appendResult15, ddx(appendResult15), ddy(appendResult15), ase_worldNormal, ase_worldViewDir, i.viewDir, 64, 64, _Scale, 0, _Texture0_ST.xy, appendResult8, 0 );
			float2 ParaUV11 = OffsetPOM2;
			float4 tex2DNode1 = tex2D( _t_crack, ParaUV11, ddx( i.uv_texcoord ), ddy( i.uv_texcoord ) );
			float4 temp_cast_0 = (_pow).xxxx;
			float4 clampResult25 = clamp( pow( ( _ade + tex2DNode1 ) , temp_cast_0 ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 lerpResult21 = lerp( lerpResult35 , _Color1 , clampResult25);
			o.Albedo = lerpResult21.rgb;
			float lerpResult18 = lerp( 0.0 , 1.0 , tex2DNode1.r);
			float clampResult26 = clamp( lerpResult18 , 0.0 , 1.0 );
			o.Alpha = clampResult26;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17700
-1922;45;1920;877;2231.172;342.8214;1.500905;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;3;-2074.085,-471.0079;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;14;-1912.192,-608.9029;Inherit;False;Property;_addX;addX;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1770.012,-427.3481;Inherit;False;Property;_addY;addY;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-1604.955,-356.4487;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-1747.135,-538.0035;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1685.664,333.5869;Inherit;False;Property;_curvatureu;curvature u;3;0;Create;True;0;0;False;0;17.3;0;0;100;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1660.229,454.3992;Inherit;False;Property;_curvaturev;curvature v;4;0;Create;True;0;0;False;0;6.45;30;0;30;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;15;-1549.846,-540.5137;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;12;-2009.305,-140.1681;Inherit;True;Property;_Texture0;Texture 0;5;0;Create;True;0;0;False;0;eece574e69a12ed469111383a05fb76d;eece574e69a12ed469111383a05fb76d;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.DynamicAppendNode;8;-1234.207,351.3908;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;7;-1627.071,138.0372;Inherit;False;Tangent;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;6;-1556.404,3.322995;Inherit;False;Property;_Scale;Scale;2;0;Create;True;0;0;False;0;0.11;-0.22;-1;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ParallaxOcclusionMappingNode;2;-1021.141,-77.93967;Inherit;False;0;64;False;-1;64;False;-1;4;0.02;0;False;1,1;True;0,0;Texture2D;7;0;FLOAT2;0,0;False;1;SAMPLER2D;;False;2;FLOAT;0.02;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT2;0,0;False;6;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DdyOpNode;5;-963.7884,285.6423;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DdxOpNode;4;-970.1469,185.1772;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;11;-753.9471,-135.6817;Inherit;False;ParaUV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-471.8553,3.504122;Inherit;True;Property;_t_crack;t_crack;1;0;Create;True;0;0;False;0;-1;eece574e69a12ed469111383a05fb76d;eece574e69a12ed469111383a05fb76d;True;0;False;white;Auto;False;Object;-1;Derivative;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;33;-1256.937,-591.1709;Inherit;False;1;0;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-535.986,-197.0588;Inherit;False;Property;_ade;ade;11;0;Create;True;0;0;False;0;0;-0.98;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-273.5837,-146.2585;Inherit;False;Property;_pow;pow;10;0;Create;True;0;0;False;0;0;1.93;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;34;-976.5552,-589.8381;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;30;-439.8667,-120.1633;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;24;-79.88377,-142.3585;Inherit;True;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;36;-367.1985,-606.9122;Inherit;False;Property;_Color2;Color 2;12;0;Create;True;0;0;False;0;0,0,0,0;0.2169811,0.08085617,0.08085617,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;22;-365.577,-856.6401;Inherit;False;Property;_Color0;Color 0;8;0;Create;True;0;0;False;0;0,0,0,0;0.2641509,0.1756197,0.1707013,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;20;-22.879,161.2036;Inherit;False;Constant;_Float1;Float 1;7;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;43;-515.9744,-618.7241;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;23;-333.2958,-354.22;Inherit;False;Property;_Color1;Color 1;9;0;Create;True;0;0;False;0;0,0,0,0;0.4528302,0.2767463,0.1815593,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;35;-65.21472,-547.3294;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;18;206.3707,77.55035;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;25;165.8163,-128.0586;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;26;379.0162,139.7415;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;42;-774.3556,-917.3903;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;21;298.4042,-276.32;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleTimeNode;40;-1302.813,-855.5452;Inherit;False;1;0;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-735.4595,-749.3038;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;37;-785.53,-505.9122;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;39;-990.4803,-869.4932;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;578.3491,-197.8066;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;sh_crack;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;1;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;16;0;3;2
WireConnection;16;1;17;0
WireConnection;13;0;3;1
WireConnection;13;1;14;0
WireConnection;15;0;13;0
WireConnection;15;1;16;0
WireConnection;8;0;9;0
WireConnection;8;1;10;0
WireConnection;2;0;15;0
WireConnection;2;1;12;0
WireConnection;2;2;6;0
WireConnection;2;3;7;0
WireConnection;2;5;8;0
WireConnection;5;0;3;0
WireConnection;4;0;3;0
WireConnection;11;0;2;0
WireConnection;1;1;11;0
WireConnection;1;3;4;0
WireConnection;1;4;5;0
WireConnection;34;0;33;0
WireConnection;30;0;31;0
WireConnection;30;1;1;0
WireConnection;24;0;30;0
WireConnection;24;1;27;0
WireConnection;43;0;34;0
WireConnection;35;0;22;0
WireConnection;35;1;36;0
WireConnection;35;2;43;0
WireConnection;18;1;20;0
WireConnection;18;2;1;0
WireConnection;25;0;24;0
WireConnection;26;0;18;0
WireConnection;42;0;39;0
WireConnection;21;0;35;0
WireConnection;21;1;23;0
WireConnection;21;2;25;0
WireConnection;41;0;42;0
WireConnection;41;1;37;0
WireConnection;37;0;34;0
WireConnection;39;0;40;0
WireConnection;0;0;21;0
WireConnection;0;9;26;0
ASEEND*/
//CHKSM=782B9E440C51E275B499657CA97E64E3AAF6CD47