// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/CPULight"
{
    Properties
    {
        // we have removed support for texture tiling/offset,
        // so make them not be displayed in material inspector
		_Color("Color", Color) = (1,1,1,1)
		//_CastShadow("CastShadow", float) = 1
		//https://stackoverflow.com/questions/45098671/how-to-define-an-array-of-floats-in-shader-properties

        [NoScaleOffset] _MainTex ("Texture", 2D) = "white" {}

		//[NoScaleOffset] _Color ("Main Color", Color) = (0,0,0,1)
		[NoScaleOffset] _LightPos ("LightPos", Vector) = (0,0,0,0)
		[NoScaleOffset] _ObjPos ("ObjPos", Vector) = (0,0,0,0)
		//[NoScaleOffset] _LightDot ("LightDot", float) = 0.0
		//[NoScaleOffset] _DebugVertexPos ("DebugVertexPos", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            // use "vert" function as the vertex shader
            #pragma vertex vert
            // use "frag" function as the pixel (fragment) shader
            #pragma fragment frag

            // vertex shader inputs ... asking for it from somewhere
            struct appdata//DONT CHANGE LINE ENDINGS HERE
            {
                float4 vertex : POSITION; // vertex position
				float3 normal : NORMAL;
                float2 uv : TEXCOORD0; // texture coordinate
            };

            // vertex shader outputs ("vertex to fragment")
            struct v2f
            {
                float2 uv : TEXCOORD0; // texture coordinate
				float3 normal : NORMAL;
                float4 vertex : SV_POSITION; // clip space position
            };

            // vertex shader

			float4 _LightPos;
			float4 _ObjPos;
			float _LightDot;
			float4 _DebugVertexPos;

            v2f vert (appdata v)
            {
                v2f o;
                // transform position to clip space
                // (multiply with model*view*projection matrix)
                o.vertex = UnityObjectToClipPos(v.vertex);
				//_DebugVertexPos = UnityObjectToClipPos(v.vertex);
				o.normal = v.normal;
                // just pass the texture coordinate
                o.uv = v.uv;				

                return o;
            }
            
            // texture we will sample
            sampler2D _MainTex;
			half4 _Color;

            // pixel shader; returns low precision ("fixed4" type)
            // color ("SV_Target" semantic)
            fixed4 frag (v2f i) : SV_Target
            {
				//http://answers.google.com/answers/threadview?id=18979 for calculating
				//ray-plane intersection
				


				//calulating CPUlighting
				//

				float3 LightObjectDiff = normalize (_LightPos - _ObjPos);
				float3 SurfaceNormal = i.normal;
				//
				_LightDot = LightObjectDiff.x * SurfaceNormal.x +
				LightObjectDiff.y * SurfaceNormal.y +
				LightObjectDiff.z * SurfaceNormal.z;

				//
				////_LightDot = dot ( normalize ( _LightPos - _ObjPos /*i.vertex*/), i.normal );
                // sample texture and return it

                fixed4 col = tex2D(_MainTex, i.uv);
				col *= _LightDot;//*=
				col = col * _Color;
                return /*_Color **/ col;
				//hát ha egy meshből akarod megoldani, akkor ide bizony kell
				//annyi color ahogy résznek külön akarod kiszámolni 
				//de várj ennek nincs is értelme, mert inkább magát csak a színt
				//akarod külön számolni...
				//if koordináták color1 ... if koordináták color2 ...
            }
            ENDCG
        }
    }
}