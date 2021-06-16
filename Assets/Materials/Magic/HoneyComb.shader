Shader "Scene/HoneyComb"
 {
Properties {
    [HDR]_EdgeColor ("Edge Color", Color) = (1,1,1,1)
    [HDR]_Color ("BG Color", Color) = (1,1,1,1)
    _Colmun ("Colmun", float) = 1
    _Edge ("Edge Width", Range(0,1)) = 0
    _Center ("Center(xy) OffsetX(zw)", Vector) =  (0,0,0,0)
}

SubShader {
    Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
    // LOD 100
    Pass
    {
        Tags {"LightMode"="SRPDefaultUnlit"}
        ZWrite On
        ColorMask 0 
    }
    Pass {  
            Tags {"LightMode"="UniversalForward"}
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back
            ZWrite Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

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

            fixed4 _Color;
            fixed4 _EdgeColor;
            fixed _Colmun;
            fixed _Edge;
            fixed4 _Center;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv=v.uv;

                return o;
            }

            fixed4 frag (v2f input) : SV_Target
            {

                fixed size = 0.5 / (_Colmun * 0.86602540378445);
                fixed2 pixel = input.uv.xy + fixed2( 2 * size * _Center.z - _Center.x, 2 * size * _Center.w - _Center.y);

                fixed q = (pixel.x * 0.57735026918963 - pixel.y *0.33333333333) / size;
                fixed r = pixel.y * 0.66666666666 / size;

                fixed rx = round(q);
                fixed ry = round(-q - r);
                fixed rz = round(r);

                fixed yx = step(abs(ry + q + r),abs(rx - q));
                fixed zx = step(abs(rz - r),abs(rx - q));
                fixed zy = step(abs(rz - r),abs(ry + q + r));

                rx = (yx * zx) * (-ry - rz) + (1-yx * zx ) * rx;
                ry = (zy- yx*zx*zy) * (-rx - rz) + (1- zy+yx*zx*zy) *ry;
                rz = (1- zy+yx*zx*zy) * (-rx - ry) + (zy-yx * zx* zy) * rz;

                fixed3 n[6] = {
                    fixed3(0, 1, -1),
                    fixed3(1, 0, -1),
                    fixed3(-1, 1, 0),
                    fixed3(1, -1, 0),
                    fixed3(-1, 0, 1),
                    fixed3(0, -1, 1),
                };

                fixed mindis = 2;
                for(int i=0; i<6; i++){
                    fixed2 pos = fixed2(size * 1.73205080756887 * ((rx+n[i].x) + (rz+n[i].z) * 0.5),size * 1.5 *(rz+ n[i].z));
                    fixed a = distance(pixel.xy,pos);
                    mindis = min(mindis,a);
                }

                fixed isEdge = step(abs(distance(pixel.xy,fixed2(size * 1.73205080756887 * (rx + rz * 0.5) ,size * 1.5 * rz))-mindis),size*_Edge);

                fixed4 col = isEdge * _EdgeColor + 
                (1 - isEdge)* _Color;

                return col;
            }
        ENDCG
    }
}

}