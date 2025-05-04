// Простой unlit шейдер для плазменного шара, оптимизированный для WebGL
Shader "Custom/PlasmaSphereWebGL"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (0,0.8,1,1)
        _GlowColor ("Glow Color", Color) = (0,1,1,1)
        _GlowIntensity ("Glow Intensity", Range(0, 3)) = 1.5
        _PulseSpeed ("Pulse Speed", Range(0, 100)) = 2
    }
    
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "IgnoreProjector"="True" }
        LOD 100
        
        Blend SrcAlpha One
        ZWrite Off
        Cull Back
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0 // Для совместимости с WebGL
            
            #include "UnityCG.cginc"
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };
            
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 viewDir : TEXCOORD1;
                float3 normal : NORMAL;
            };
            
            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _GlowColor;
            float _GlowIntensity;
            float _PulseSpeed;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // Простой Fresnel эффект для края сферы
                float fresnel = pow(1.0 - saturate(dot(i.normal, i.viewDir)), 3.0);
                
                // Пульсация
                float pulse = sin(_Time.y * _PulseSpeed);
                
                // Текстура для добавления деталей
                fixed4 texColor = tex2D(_MainTex, i.uv);
                
                // Финальный цвет
                fixed4 col = _Color * texColor;
                col.a *= 0.7; // Базовая прозрачность
                
                // Добавляем свечение на основе Fresnel
                fixed4 glow = _GlowColor * fresnel * _GlowIntensity;
                glow *= (0.1 + 0.2 * pulse); // Немного пульсации
                
                // Смешиваем базовый цвет со свечением
                col += glow;
                col.a = saturate(col.a); // Ограничиваем альфу до 1
                
                return col;
            }
            ENDCG
        }
    }
    
    // Fallback для очень старых устройств
    FallBack "Mobile/Particles/Additive"
}