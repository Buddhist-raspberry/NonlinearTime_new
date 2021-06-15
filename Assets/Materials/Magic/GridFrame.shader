Shader "Scene/Grid Frame"
{
    Properties
    {
        [HDR]_RimColor("RimColor",Color)=(1,1,1,1)
        [HDR]_EdgeColor("EdgeColor",Color)=(0,0,0,0)
        _RimIntensity("RimIntensity",Float) = 0
        _TileSpeed("Tile And Speed",Vector)=(10,10,0,0)
        _Width("Width",Range(0,1))=0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Pass
        {

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
             #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            fixed4 _RimColor;
            fixed4 _EdgeColor;
            float4 _TileSpeed;
            float _Width;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;

                return o;
            }

            float rectangle(in fixed4 boarder,in fixed2 coord)
            {
                fixed2 bl=step(boarder.xy,coord);
                fixed2 tr=1.0-step(boarder.zw,coord);
                return bl.x*bl.y*tr.x*tr.y;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv=frac(i.uv*_TileSpeed.xy+_TileSpeed.zw*_Time.x);
                fixed wline=_Width/2.0;
                fixed4 boarder=fixed4(wline,wline,1.-wline,1.-wline);
                float s=rectangle(boarder,i.uv);
                fixed3 color=(1.0-s)*_RimColor.xyz+s*_EdgeColor.xyz;


                return fixed4(color,1.0);
            }
            ENDCG
        }
    }
}
