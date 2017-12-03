// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/RainShader"
{
        Properties
    {
        // Color property for material inspector, default to white
		_MainTex ("Texture", 2D) = "white" {}
        _Color ("Main Color", Color) = (1,1,1,1)
        _RandomStart ("RandomStart",float) = 1

    }
    SubShader
    {
    	Tags { "Queue"="Transparent" }
        Pass
        {
        	ZTest LEqual
        	ZWrite Off
        	//ZTest Always
        	Blend SrcAlpha OneMinusSrcAlpha
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            uniform float4 _TexCoords;
            fixed4 _Color;
            fixed _RandomStart;
            float top;
            float bottom;

            struct data {
            	float4 vertex : POSITION;
            	float2 uv: TEXCOORD0;
            };

            struct v2f{
            	float4 position: SV_POSITION;
            	float4 screenPos: TEXCOORD0;
            	float2 uv:TEXCOORD1;
            };

            v2f vert(data i){
            	v2f o;
            	o.position = UnityObjectToClipPos(i.vertex);
            	o.uv = TRANSFORM_TEX(i.uv,_MainTex);
            	o.screenPos = o.position;
            	return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                
                //float offsett = i.position.x*i.position.y;//Llamas rotas
                //float offsett = i.position.x+i.position.y;//Llamas ralladas
                float offsett = _TexCoords.x;

                float2 screenPos = i.screenPos.xy / i.screenPos.w;
                float2 texCoord = float2(i.uv.x,i.uv.y + _Time.w/6.0);
                float _half = (top + bottom)*0.5;
                float _diff = (bottom - top)*0.5;
                screenPos.x = screenPos.x*(_half+_diff*screenPos.y);
                screenPos.x = (screenPos.x+1)*0.5;
                screenPos.y = (screenPos.y+1)*0.5;
                fixed4 sum = fixed4(0.0h,0.0h,0.0h,0.0h);
                if(texCoord.y>1){
                    texCoord.y = texCoord.y - (int) texCoord.y ;
                }
                sum = tex2D(_MainTex,texCoord)*screenPos.y;//*_SinTime.w

                //sum = tex2D(_MainTex,i.uv);//*_SinTime.w
               	//sum = tex2D(_MainTex,float2(i.uv.x,i.uv.y))*screenPos.y;
               	if(sum.w<0.02h){
               		return fixed4(0.0h,0.0h,0.0h,0.0);
               	}
               	sum.w = 0.4h;
                return sum;
            }
            ENDCG
        }
    }
}
