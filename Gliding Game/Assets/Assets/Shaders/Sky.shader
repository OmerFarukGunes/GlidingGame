Shader "Custom/EquatorSkybox"
{
    Properties
    {
        _SkyColor("Sky Color", Color) = (0.4980392,0.7450981,1,1)
        _EquatorColor("Equator Color", Color) = (1,0.747,0,1)
        _GroundColor("Ground Color", Color) = (0.4980392,0.497,0,1)
        _EquatorHeight("Equator Height", Range(0, 1)) = 0.5
        _EquatorSmoothness("Equator Smoothness", Range(0.01, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Background" "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldDir : TEXCOORD0;
            };

            float4 _SkyColor;
            float4 _EquatorColor;
            float4 _GroundColor;
            float _EquatorHeight;
            float _EquatorSmoothness;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldDir = normalize(mul((float3x3)UNITY_MATRIX_M, v.vertex.xyz));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Map world Y-coordinate to [0, 1], ensuring the equator is at the center
                float y = i.worldDir.y * 0.5 + 0.5;

                // Adjust equator position to be exactly at the center
                float equatorBlend = smoothstep(0.5 - _EquatorSmoothness, 0.5 + _EquatorSmoothness, y);

                // Interpolate between SkyColor and EquatorColor
                float4 topColor = lerp(_EquatorColor, _SkyColor, equatorBlend);

                // Interpolate between GroundColor and EquatorColor
                float4 bottomColor = lerp(_GroundColor, _EquatorColor, equatorBlend);

                // Blend top and bottom colors based on height
                return lerp(bottomColor, topColor, y);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
