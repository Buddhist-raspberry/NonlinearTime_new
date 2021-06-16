Shader "Scene/Honey Grid"
 {
Properties {
    [HDR]_EdgeColor ("Edge Color", Color) = (1,1,1,1)
    [HDR]_Color ("BG Color", Color) = (1,1,1,1)
    _Colmun ("Colmun", float) = 1
    _Edge ("Edge Width", Range(0,1)) = 0
    _Center ("Center(xy) OffsetX(zw)", Vector) =  (0,0,0,0)

    [HDR]_GridColorV("Grid Color Vertical",Color)=(1,1,1,1)
    [HDR]_GridColorH("Grid Color Horizontal",Color)=(1,1,1,1)
    _TileSpeed("Tile And Speed",Vector)=(10,10,0,0)
    _FrameWidth("Frame Width",Vector)=(0.1,0.1,0,0)
}

SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 100
    Pass {  

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

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

            fixed4 _GridColorV;
            fixed4 _GridColorH;
            float4 _TileSpeed;
            fixed2 _FrameWidth;

            #define SQRT3 1.7321

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

            fixed4 honeyComb(float2 uv)
            {
                fixed size = 0.5 / (_Colmun * SQRT3/2.0);
                fixed2 pixel = uv.xy + fixed2( 2 * size * _Center.z - _Center.x, 2 * size * _Center.w - _Center.y);

                fixed q = (pixel.x * 0.5774 - pixel.y *0.3333) / size;
                fixed r = pixel.y * 0.6666 / size;

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
                    fixed2 pos = fixed2(size *SQRT3* ((rx+n[i].x) + (rz+n[i].z) * 0.5),size * 1.5 *(rz+ n[i].z));
                    fixed a = distance(pixel.xy,pos);
                    mindis = min(mindis,a);
                }

                fixed isEdge = step(abs(distance(pixel.xy,fixed2(size * SQRT3 * (rx + rz * 0.5) ,size * 1.5 * rz))-mindis),size*_Edge);

                fixed4 col = isEdge * _EdgeColor + (1 - isEdge)* _Color;

                return col;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //fixed4 color=honeyComb(i.uv);
				fixed4 color = _Color;

                i.uv=frac(i.uv*_TileSpeed.xy+_TileSpeed.zw*_Time.x);
                fixed vwidth=_FrameWidth.x/2.0;
                fixed hwidth=_FrameWidth.y/2.0;
                fixed4 boarder=fixed4(0.,hwidth,1.,1.-hwidth);
                float s=rectangle(boarder,i.uv);
                color=(1.0-s)*_GridColorH+s*color;

                boarder=fixed4(vwidth,0.,1.-vwidth,1.);
                s=rectangle(boarder,i.uv);
                color=(1.0-s)*_GridColorV+s*color;
                
                return color;
            }
        ENDCG
    }
}

}