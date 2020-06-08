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
				float2 uv047 = IN.texcoord.xy * float2( 0,3 ) + float2( 0,-1 );
				float2 uv018 = IN.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float4 tex2DNode5 = tex2D( _TextureSample0, uv018 );
				float clampResult22 = clamp( (-2.0 + (_fondu - 0.0) * (1.0 - -2.0) / (1.0 - 0.0)) , 0.0 , 1.0 );
				float smoothstepResult10 = smoothstep( _Float3 , _Float2 , ( ( 1.0 - tex2DNode5.r ) + (-0.9 + (( _fondu + clampResult22 ) - 0.0) * (0.6 - -0.9) / (1.0 - 0.0)) ));
				float4 appendResult17 = (float4((( tex2D( _t_ui_fond_menu_start, panner32 ).a + tex2D( _t_ui_fond, uv047 ) )).rgb , smoothstepResult10));
				
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
-1817;121;1691;859;1960.592;394.2594;1;True;True
Node;AmplifyShaderEditor.RangedFloatNode;6;-1811.128,721.4776;Inherit;False;Property;_fondu;fondu;2;0;Create;True;0;0;False;0;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;34;-1719.651,-477.0825;Inherit;False;Constant;_Vector0;Vector 0;6;0;Create;True;0;0;False;0;1,1;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;36;-1646.651,-595.0825;Inherit;False;Property;_tilling;tilling;6;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;21;-1511.835,957.3004;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-2;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1397.936,194.6897;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;22;-1315.615,980.537;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;-1495.651,-521.0825;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;5;-1162.537,175.356;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;-1;0abc1b991c06f2245a24f850affece9f;83327fbd39a0ee84286faea48f3bd352;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-1315.277,-518.6857;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;3,3;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-1146.923,680.5719;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;47;-1242.384,-156.3478;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0,3;False;1;FLOAT2;0,-1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;8;-912.0344,547.3422;Inherit;False;5;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-0.9;False;4;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;32;-1001.244,-485.8457;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,-1;False;1;FLOAT;0.03;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;13;-697.4487,190.1209;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-520.3903,335.45;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;30;-775.8688,-508.6986;Inherit;True;Property;_t_ui_fond_menu_start;t_ui_fond_menu_start;5;0;Create;True;0;0;False;0;-1;c067e37722fe3bd40b9dcfeb1764d4df;c067e37722fe3bd40b9dcfeb1764d4df;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-440.1814,746.8348;Inherit;False;Property;_Float2;Float 2;3;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-475.9044,629.5905;Inherit;False;Property;_Float3;Float 3;1;0;Create;True;0;0;False;0;0.45;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;29;-766.7068,-150.8656;Inherit;True;Property;_t_ui_fond;t_ui_fond;4;0;Create;True;0;0;False;0;-1;c8f225f4e51d1ec4689ce5323de3e176;c8f225f4e51d1ec4689ce5323de3e176;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-142.3157,-366.2967;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;10;-225.835,365.3889;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;16;135.9536,206.8477;Inherit;False;False;False;False;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;15;95.14429,-13.33682;Inherit;True;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;-861.5631,253.2345;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;17;428.6687,138.9284;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;677.5312,101.0141;Float;False;True;-1;2;ASEMaterialInspector;0;4;sh_ui_fondu;5056123faa0c79b47ab6ad7e8bf059a4;True;Default;0;0;Default;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;True;0;True;-9;True;True;0;True;-5;255;True;-8;255;True;-7;0;True;-4;0;True;-6;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;0;True;-11;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;0
WireConnection;21;0;6;0
WireConnection;22;0;21;0
WireConnection;35;0;36;0
WireConnection;35;1;34;0
WireConnection;5;1;18;0
WireConnection;31;0;35;0
WireConnection;23;0;6;0
WireConnection;23;1;22;0
WireConnection;8;0;23;0
WireConnection;32;0;31;0
WireConnection;13;0;5;1
WireConnection;7;0;13;0
WireConnection;7;1;8;0
WireConnection;30;1;32;0
WireConnection;29;1;47;0
WireConnection;33;0;30;4
WireConnection;33;1;29;0
WireConnection;10;0;7;0
WireConnection;10;1;12;0
WireConnection;10;2;11;0
WireConnection;16;0;10;0
WireConnection;15;0;33;0
WireConnection;19;0;5;1
WireConnection;19;1;5;2
WireConnection;19;2;5;3
WireConnection;17;0;15;0
WireConnection;17;3;16;0
WireConnection;1;0;17;0
ASEEND*/
//CHKSM=4823E631CE4408C8E77B788003A6D2A84D6D2B1B