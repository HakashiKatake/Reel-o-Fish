Shader "Custom/SimpleWaterShader"
{
    Properties
    {
        _WaterColor ("Water Color", Color) = (0.0, 0.47, 0.75, 0.8)
        _DeepWaterColor ("Deep Water Color", Color) = (0.0, 0.2, 0.4, 1.0)
        _FresnelColor ("Fresnel Color", Color) = (0.7, 0.9, 1.0, 1.0)
        
        _WaveSpeed ("Wave Speed", Range(0, 2)) = 0.5
        _WaveAmplitude ("Wave Amplitude", Range(0, 0.5)) = 0.1
        _WaveFrequency ("Wave Frequency", Range(0, 10)) = 2.0
        
        _Glossiness ("Smoothness", Range(0, 1)) = 0.8
        _FresnelPower ("Fresnel Power", Range(0, 5)) = 2.0
    }
    
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        LOD 200
        
        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }
            
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Back
            
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            
            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float2 uv : TEXCOORD0;
            };
            
            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float3 positionWS : TEXCOORD0;
                float3 normalWS : TEXCOORD1;
                float2 uv : TEXCOORD2;
                float3 viewDirWS : TEXCOORD3;
                float fogFactor : TEXCOORD4;
            };
            
            CBUFFER_START(UnityPerMaterial)
                float4 _WaterColor;
                float4 _DeepWaterColor;
                float4 _FresnelColor;
                float _WaveSpeed;
                float _WaveAmplitude;
                float _WaveFrequency;
                float _Glossiness;
                float _FresnelPower;
            CBUFFER_END
            
            // Simple noise function for waves
            float noise(float2 uv)
            {
                return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
            }
            
            // Fake wave displacement (vertex shader)
            float3 CalculateWaveOffset(float3 positionWS, float2 uv)
            {
                float time = _Time.y * _WaveSpeed;
                
                // Multiple sine waves for more natural look
                float wave1 = sin(positionWS.x * _WaveFrequency + time) * _WaveAmplitude;
                float wave2 = sin(positionWS.z * _WaveFrequency * 0.7 + time * 1.3) * _WaveAmplitude * 0.5;
                float wave3 = sin((positionWS.x + positionWS.z) * _WaveFrequency * 0.5 + time * 0.8) * _WaveAmplitude * 0.3;
                
                return float3(0, wave1 + wave2 + wave3, 0);
            }
            
            Varyings vert(Attributes input)
            {
                Varyings output;
                
                // Transform to world space
                float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
                
                // Apply fake wave displacement
                positionWS += CalculateWaveOffset(positionWS, input.uv);
                
                // Transform to clip space
                output.positionCS = TransformWorldToHClip(positionWS);
                output.positionWS = positionWS;
                
                // Transform normal to world space
                output.normalWS = TransformObjectToWorldNormal(input.normalOS);
                
                // Calculate view direction
                output.viewDirWS = GetWorldSpaceViewDir(positionWS);
                
                // Pass through UVs
                output.uv = input.uv;
                
                // Fog
                output.fogFactor = ComputeFogFactor(output.positionCS.z);
                
                return output;
            }
            
            half4 frag(Varyings input) : SV_Target
            {
                // Normalize vectors
                float3 normalWS = normalize(input.normalWS);
                float3 viewDirWS = normalize(input.viewDirWS);
                
                // Animated UV for fake water texture movement
                float time = _Time.y * _WaveSpeed * 0.5;
                float2 uv1 = input.uv + float2(time * 0.1, time * 0.05);
                float2 uv2 = input.uv + float2(-time * 0.08, time * 0.06);
                
                // Simple noise-based "texture"
                float pattern1 = noise(uv1 * 10.0);
                float pattern2 = noise(uv2 * 8.0);
                float waterPattern = (pattern1 + pattern2) * 0.5;
                
                // Fresnel effect (edges are brighter)
                float fresnel = pow(1.0 - saturate(dot(normalWS, viewDirWS)), _FresnelPower);
                
                // Mix colors based on depth and fresnel
                float4 waterColor = lerp(_DeepWaterColor, _WaterColor, waterPattern);
                waterColor = lerp(waterColor, _FresnelColor, fresnel * 0.5);
                
                // Simple specular highlight
                Light mainLight = GetMainLight();
                float3 halfDir = normalize(mainLight.direction + viewDirWS);
                float specular = pow(saturate(dot(normalWS, halfDir)), 32.0) * _Glossiness;
                
                // Add specular
                waterColor.rgb += specular * mainLight.color;
                
                // Apply fog
                waterColor.rgb = MixFog(waterColor.rgb, input.fogFactor);
                
                return waterColor;
            }
            ENDHLSL
        }
    }
    
    FallBack "Universal Render Pipeline/Lit"
}
