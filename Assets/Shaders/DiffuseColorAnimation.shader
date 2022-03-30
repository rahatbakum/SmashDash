Shader "Mobile/DiffuseColorAnimation"
{
    Properties
    {
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Color2 ("Color 2", Color) = (0,0,0,0)
        _AnimationTime ("Animation Time", FLoat) = 2.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        fixed4 _Color1;
        fixed4 _Color2;
        half _AnimationTime;

        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 finalColor = lerp(_Color1, _Color2, 0.5 + 0.5 * sin(3.1415 * _Time.y / _AnimationTime));
            o.Albedo = finalColor.rgb;
            o.Metallic = 0;
            o.Smoothness = 0;
            o.Alpha = finalColor.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
