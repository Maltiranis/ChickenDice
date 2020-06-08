// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "sh_ui_fondu"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		
		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Float3("Float 3", Float) = 0.45
		_fondu("fondu", Range( 0 , 1)) = 1
		_Float2("Float 2", Float) = 0.5
		_t_ui_fond("t_ui_fond", 2D) = "white" {}
		_t_ui_fond_menu_start("t_ui_fond_menu_start", 2D) = "white" {}
		_tilling("tilling", Float) = 0
		_icone("icone", Float) = 0

	}

	SubShader
	{
		LOD 0

		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		
		Stencil
		{
			Ref [_Stencil]
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
			CompFront [_StencilComp]
			PassFront [_StencilOp]
			FailFront Keep
			ZFailFront Keep
			CompBack Always
			PassBack Keep
			FailBack Keep
			ZFailBack Keep
		}


		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		
		Pass
		{
			Name "Default"
		CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_CLIP_RECT
			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			#include "UnityShaderVariables.cginc"

			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform fixed4 _TextureSampleAdd;
			uniform float4 _ClipRect;
			uniform sampler2D _MainTex;
			uniform float _icone;
			uniform sampler2D _t_ui_fond_menu_start;
			uniform float _tilling;
			uniform sampler2D _t_ui_fond;
			uniform float _Float3;
			uniform float _Float2;
			uniform sampler2D _TextureSample0;
			uniform float _fondu;

			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID( IN );
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				OUT.worldPosition = IN.vertex;
				
				
				OUT.worldPosition.xyz +=  float3( 0, 0, 0 ) ;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				OUT.color = IN.color * _Color;
				return OUT;
			}

			fixed4 frag(v2f IN  ) : SV_Target
			{
				float2 uv031 = IN.texcoord.xy * ( _tilling * float2( 1,1 ) ) + float2( 0,0 );
				float2 panner32 = ( 0.03 * _Time.y * float2( 1,-1 ) + uv031);
				float ifLocalVar49 = 0;
				if( _icone > 1.0 )
				ifLocalVar49 = 0.0;
				else if( _icone == 1.0 )
				ifLocalVar49 = 0.0;
				else if( _icone < 1.0 )
				ifLocalVar49 = tex2D( _t_ui_fond_menu_start, panner32 ).a;
				float ifLocalVar51 = 0;
				if( _icone >= 4.0 )
				ifLocalVar51 = 0.0;
				else
				ifLocalVar51 = ifLocalVar49;
				float2 uv047 = IN.texcoord.xy * float2( 0,3 ) + float2( 0,-1 );
				float2 uv018 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float clampResult22 = clamp( (-2.0 + (_fondu - 0.0) * (1.0 - -2.0) / (1.0 - 0.0)) , 0.0 , 1.0 );
				float smoothstepResult10 = smoothstep( _Float3 , _Float2 , ( ( 1.0 - tex2D( _TextureSample0, uv018 ).r ) + (-0.9 + (( _fondu + clampResult22 ) - 0.0) * (0.6 - -0.9) / (1.0 - 0.0)) ));
				float4 appendResult17 = (float4((( ifLocalVar51 + tex2D( _t_ui_fond, uv047 ) )).rgb , smoothstepResult10));
				
				half4 color = appendResult17;
				
				#ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
                #endif
				
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif

				return color;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17700
78;116;1691;859;2995.523;822.4917;1.963871;True;True
Node;AmplifyShaderEditor.Vector2Node;34;-2800.402,-520.0547;Inherit;False;Constant;_Vector0;Vector 0;6;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;36;-2727.402,-638.0547;Inherit;False;Property;_tilling;tilling;6;0;Create;True;0;0;False;0;0;3.75;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-2576.402,-564.0547;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-2134.013,1076.651;Inherit;False;Property;_fondu;fondu;2;0;Create;True;0;0;False;0;1;0.629;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-2396.028,-561.658;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;3,3;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;21;-1834.72,1312.474;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-2;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1787.704,559.0891;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;32;-2081.995,-528.8179;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,-1;False;1;FLOAT;0.03;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;22;-1638.5,1335.711;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;54;-2009.938,269.0223;Inherit;False;Constant;_egge;egge;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;30;-1856.619,-551.6708;Inherit;True;Property;_t_ui_fond_menu_start;t_ui_fond_menu_start;5;0;Create;True;0;0;False;0;-1;c067e37722fe3bd40b9dcfeb1764d4df;c067e37722fe3bd40b9dcfeb1764d4df;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;50;-1694.026,126.3034;Inherit;False;Constant;_Float0;Float 0;8;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-1469.808,1035.746;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-1891.48,44.29132;Inherit;False;Property;_icone;icone;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;53;-2013.838,193.6223;Inherit;False;Constant;_sword;sword;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;-1485.422,530.53;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;0abc1b991c06f2245a24f850affece9f;83327fbd39a0ee84286faea48f3bd352;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;49;-1467.099,96.99313;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;-2007.338,348.3224;Inherit;False;Constant;_graine;graine;8;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;52;-1354.738,305.4223;Inherit;False;Constant;_Float1;Float 1;8;0;Create;True;0;0;False;0;4;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;8;-1234.92,902.5163;Inherit;False;5;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.9;False;4;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;13;-1020.334,545.295;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;47;-1242.384,-156.3478;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0,3;False;1;FLOAT2;0,-1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;12;-798.7897,984.7646;Inherit;False;Property;_Float3;Float 3;1;0;Create;True;0;0;False;0;0.45;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-763.0667,1102.009;Inherit;False;Property;_Float2;Float 2;3;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;51;-1124.638,219.6224;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;29;-766.7068,-150.8656;Inherit;True;Property;_t_ui_fond;t_ui_fond;4;0;Create;True;0;0;False;0;-1;c8f225f4e51d1ec4689ce5323de3e176;c8f225f4e51d1ec4689ce5323de3e176;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-843.2756,690.6241;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-142.3157,-366.2967;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;10;-548.7203,720.563;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;15;95.14429,-13.33682;Inherit;True;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;16;-175.4002,721.1579;Inherit;False;False;False;False;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;17;428.6687,138.9284;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;677.5312,101.0141;Float;False;True;-1;2;ASEMaterialInspector;0;4;sh_ui_fondu;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;True;0;True;-9;True;True;0;True;-5;255;True;-8;255;True;-7;0;True;-4;0;True;-6;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;0;True;-11;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;35;0;36;0
WireConnection;35;1;34;0
WireConnection;31;0;35;0
WireConnection;21;0;6;0
WireConnection;32;0;31;0
WireConnection;22;0;21;0
WireConnection;30;1;32;0
WireConnection;23;0;6;0
WireConnection;23;1;22;0
WireConnection;5;1;18;0
WireConnection;49;0;48;0
WireConnection;49;1;50;0
WireConnection;49;2;53;0
WireConnection;49;3;54;0
WireConnection;49;4;30;4
WireConnection;8;0;23;0
WireConnection;13;0;5;1
WireConnection;51;0;48;0
WireConnection;51;1;52;0
WireConnection;51;2;55;0
WireConnection;51;3;55;0
WireConnection;51;4;49;0
WireConnection;29;1;47;0
WireConnection;7;0;13;0
WireConnection;7;1;8;0
WireConnection;33;0;51;0
WireConnection;33;1;29;0
WireConnection;10;0;7;0
WireConnection;10;1;12;0
WireConnection;10;2;11;0
WireConnection;15;0;33;0
WireConnection;16;0;10;0
WireConnection;17;0;15;0
WireConnection;17;3;16;0
WireConnection;1;0;17;0
ASEEND*/
//CHKSM=095CB9996B79B814329367A989BFDE2C48CC8C52