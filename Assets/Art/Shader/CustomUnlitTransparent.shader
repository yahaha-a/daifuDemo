Shader "Custom/UnlitTransparent"
{
    Properties
    {
        _Color("Color", Color) = (1, 1, 1, 1)
        _MainTex("MainTex", 2D) = "White" {}
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }

        Pass
        {
            ZWrite Off // 关闭深度写入
            Blend SrcAlpha OneMinusSrcAlpha // 设置混合模式以支持透明效果

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            fixed4 _Color; // 颜色属性
            sampler2D _MainTex; // 纹理属性

            struct appdata_t
            {
                float4 vertex : POSITION; // 顶点位置
                float4 texcoord : TEXCOORD0; // 纹理坐标
            };

            struct v2f
            {
                float4 pos : SV_POSITION; // 片段着色器输入的裁剪坐标
                float2 uv : TEXCOORD0; // 片段着色器输入的纹理坐标
            };

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex); // 计算裁剪空间位置
                o.uv = v.texcoord.xy; // 使用纹理坐标
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 texColor = tex2D(_MainTex, i.uv); // 从纹理中采样
                return fixed4(texColor.rgb * _Color.rgb, texColor.a * _Color.a); // 乘以颜色和透明度
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
