Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        Color_816a89c1c97a4080b2a084f920fb0e57("InnerColor", Color) = (0.7830189, 0.5898768, 0.129272, 1)
        Color_e12935967060477e940097c6e44cef0c("OuterColor", Color) = (0.8113208, 0.2785871, 0.1033286, 1)
        Vector2_d5bad6c71f044480bad90f20a3ace79a("Noise Tiling", Vector) = (1, 1, 0, 0)
        Vector1_e2a40ba1ccc346febf63d5b2ae43f41a("speed", Float) = 1
        Vector2_cbaffc9ca7ea49a6bd7c98ecf689134d("Tiling", Vector) = (0, 1, 0, 0)
        Vector2_bfe039aafd5b4fd98c0f020b61b19045("Offset", Vector) = (0, 0, 0, 0)
        Color_e33a7c0bdbe944dd9d613d87127761f4("Alpha", Color) = (1, 1, 1, 1)
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}

        _StencilComp("Stencil Comparison", Float) = 8
        _Stencil("Stencil ID", Float) = 0
        _StencilOp("Stencil Operation", Float) = 0
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _StencilReadMask("Stencil Read Mask", Float) = 255
        _ColorMask("Color Mask", Float) = 15
    }
        SubShader
    {
        Tags
        {
            "RenderPipeline" = "UniversalPipeline"
            "RenderType" = "Transparent"
            "UniversalMaterialType" = "Unlit"
            "Queue" = "Transparent"
        }
        Pass
        {
            Name "Sprite Unlit"
            Tags
            {
                "LightMode" = "Universal2D"
            }

        // Render State
        Cull Off
    Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
    ZTest [unity_GUIZTestMode]
    ZWrite Off

    Stencil
   {
        Ref[_Stencil]
        Comp[_StencilComp]
        Pass[_StencilOp]
        ReadMask[_StencilReadMask]
        WriteMask[_StencilWriteMask]
   }
   ColorMask[_ColorMask]
        // Debug
        // <None>

        // --------------------------------------------------
        // Pass

        HLSLPROGRAM

        // Pragmas
        #pragma target 2.0
    #pragma exclude_renderers d3d11_9x
    #pragma vertex vert
    #pragma fragment frag

        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>

        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>

        // Defines
        #define _SURFACE_TYPE_TRANSPARENT 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_COLOR
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_COLOR
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_SPRITEUNLIT
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

        // --------------------------------------------------
        // Structs and Packing

        struct Attributes
    {
        float3 positionOS : POSITION;
        float3 normalOS : NORMAL;
        float4 tangentOS : TANGENT;
        float4 uv0 : TEXCOORD0;
        float4 color : COLOR;
        #if UNITY_ANY_INSTANCING_ENABLED
        uint instanceID : INSTANCEID_SEMANTIC;
        #endif
    };
    struct Varyings
    {
        float4 positionCS : SV_POSITION;
        float4 texCoord0;
        float4 color;
        #if UNITY_ANY_INSTANCING_ENABLED
        uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };
    struct SurfaceDescriptionInputs
    {
        float4 uv0;
        float3 TimeParameters;
    };
    struct VertexDescriptionInputs
    {
        float3 ObjectSpaceNormal;
        float3 ObjectSpaceTangent;
        float3 ObjectSpacePosition;
    };
    struct PackedVaryings
    {
        float4 positionCS : SV_POSITION;
        float4 interp0 : TEXCOORD0;
        float4 interp1 : TEXCOORD1;
        #if UNITY_ANY_INSTANCING_ENABLED
        uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };

        PackedVaryings PackVaryings(Varyings input)
    {
        PackedVaryings output;
        output.positionCS = input.positionCS;
        output.interp0.xyzw = input.texCoord0;
        output.interp1.xyzw = input.color;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }
    Varyings UnpackVaryings(PackedVaryings input)
    {
        Varyings output;
        output.positionCS = input.positionCS;
        output.texCoord0 = input.interp0.xyzw;
        output.color = input.interp1.xyzw;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }

    // --------------------------------------------------
    // Graph

    // Graph Properties
    CBUFFER_START(UnityPerMaterial)
float4 Color_816a89c1c97a4080b2a084f920fb0e57;
float4 Color_e12935967060477e940097c6e44cef0c;
float2 Vector2_d5bad6c71f044480bad90f20a3ace79a;
float Vector1_e2a40ba1ccc346febf63d5b2ae43f41a;
float2 Vector2_cbaffc9ca7ea49a6bd7c98ecf689134d;
float2 Vector2_bfe039aafd5b4fd98c0f020b61b19045;
float4 Color_e33a7c0bdbe944dd9d613d87127761f4;
CBUFFER_END

// Object and Global properties

    // Graph Functions

void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
{
    Out = UV * Tiling + Offset;
}

void Unity_ColorMask_float(float3 In, float3 MaskColor, float Range, out float Out, float Fuzziness)
{
    float Distance = distance(MaskColor, In);
    Out = saturate(1 - (Distance - Range) / max(Fuzziness, 1e-5));
}

void Unity_Multiply_float(float A, float B, out float Out)
{
    Out = A * B;
}

void Unity_Multiply_float(float2 A, float2 B, out float2 Out)
{
    Out = A * B;
}


inline float Unity_SimpleNoise_RandomValue_float(float2 uv)
{
    return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
}

inline float Unity_SimpleNnoise_Interpolate_float(float a, float b, float t)
{
    return (1.0 - t) * a + (t * b);
}


inline float Unity_SimpleNoise_ValueNoise_float(float2 uv)
{
    float2 i = floor(uv);
    float2 f = frac(uv);
    f = f * f * (3.0 - 2.0 * f);

    uv = abs(frac(uv) - 0.5);
    float2 c0 = i + float2(0.0, 0.0);
    float2 c1 = i + float2(1.0, 0.0);
    float2 c2 = i + float2(0.0, 1.0);
    float2 c3 = i + float2(1.0, 1.0);
    float r0 = Unity_SimpleNoise_RandomValue_float(c0);
    float r1 = Unity_SimpleNoise_RandomValue_float(c1);
    float r2 = Unity_SimpleNoise_RandomValue_float(c2);
    float r3 = Unity_SimpleNoise_RandomValue_float(c3);

    float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
    float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
    float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
    return t;
}
void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
{
    float t = 0.0;

    float freq = pow(2.0, float(0));
    float amp = pow(0.5, float(3 - 0));
    t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

    freq = pow(2.0, float(1));
    amp = pow(0.5, float(3 - 1));
    t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

    freq = pow(2.0, float(2));
    amp = pow(0.5, float(3 - 2));
    t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

    Out = t;
}

void Unity_InvertColors_float(float In, float InvertColors, out float Out)
{
    Out = abs(InvertColors - In);
}

void Unity_Step_float(float Edge, float In, out float Out)
{
    Out = step(Edge, In);
}

void Unity_Subtract_float(float A, float B, out float Out)
{
    Out = A - B;
}

void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
{
    Out = lerp(A, B, T);
}

// Graph Vertex
struct VertexDescription
{
    float3 Position;
    float3 Normal;
    float3 Tangent;
};

VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
{
    VertexDescription description = (VertexDescription)0;
    description.Position = IN.ObjectSpacePosition;
    description.Normal = IN.ObjectSpaceNormal;
    description.Tangent = IN.ObjectSpaceTangent;
    return description;
}

// Graph Pixel
struct SurfaceDescription
{
    float3 BaseColor;
    float Alpha;
};

SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
{
    SurfaceDescription surface = (SurfaceDescription)0;
    float4 _Property_a5a5b05ffc0c492384b4cf3ba17beee0_Out_0 = Color_816a89c1c97a4080b2a084f920fb0e57;
    float4 _Property_cb6c4a89fb1f4b9685691fef7a3a914c_Out_0 = Color_e12935967060477e940097c6e44cef0c;
    float4 _UV_c951e84aabc84629826c5c8786956aad_Out_0 = IN.uv0;
    float2 _Property_8217c7c44da6420580bda81169d05f4b_Out_0 = Vector2_cbaffc9ca7ea49a6bd7c98ecf689134d;
    float2 _Property_88a5631585e8408fac91172837503399_Out_0 = Vector2_bfe039aafd5b4fd98c0f020b61b19045;
    float2 _TilingAndOffset_f3bb93582c4746f89e4de8d155edcdeb_Out_3;
    Unity_TilingAndOffset_float((_UV_c951e84aabc84629826c5c8786956aad_Out_0.xy), _Property_8217c7c44da6420580bda81169d05f4b_Out_0, _Property_88a5631585e8408fac91172837503399_Out_0, _TilingAndOffset_f3bb93582c4746f89e4de8d155edcdeb_Out_3);
    float _ColorMask_944869770e8146eeba1bf3f285cd94b4_Out_3;
    Unity_ColorMask_float((float3(_TilingAndOffset_f3bb93582c4746f89e4de8d155edcdeb_Out_3, 0.0)), IsGammaSpace() ? float3(0, 0, 0) : SRGBToLinear(float3(0, 0, 0)), 0, _ColorMask_944869770e8146eeba1bf3f285cd94b4_Out_3, 1.01);
    float _Multiply_4bae29101975427190d3d5a8474e3408_Out_2;
    Unity_Multiply_float(_ColorMask_944869770e8146eeba1bf3f285cd94b4_Out_3, 0.15, _Multiply_4bae29101975427190d3d5a8474e3408_Out_2);
    float2 _Property_aaba7eaba3ae415db170533ec8d79e25_Out_0 = Vector2_d5bad6c71f044480bad90f20a3ace79a;
    float _Property_e08f68b37b0847019d41a9624288b824_Out_0 = Vector1_e2a40ba1ccc346febf63d5b2ae43f41a;
    float _Multiply_57ad694eb4d248d989cb06fb2910c643_Out_2;
    Unity_Multiply_float(_Property_e08f68b37b0847019d41a9624288b824_Out_0, -1, _Multiply_57ad694eb4d248d989cb06fb2910c643_Out_2);
    float2 _Vector2_023abf684c5e4c479cc6ee370823f637_Out_0 = float2(0, _Multiply_57ad694eb4d248d989cb06fb2910c643_Out_2);
    float2 _Multiply_c79bf63b43384b8e99b447f22fb5fbec_Out_2;
    Unity_Multiply_float(_Vector2_023abf684c5e4c479cc6ee370823f637_Out_0, (IN.TimeParameters.x.xx), _Multiply_c79bf63b43384b8e99b447f22fb5fbec_Out_2);
    float2 _TilingAndOffset_9ddd838bfd244f55a750a6108b690efe_Out_3;
    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_aaba7eaba3ae415db170533ec8d79e25_Out_0, _Multiply_c79bf63b43384b8e99b447f22fb5fbec_Out_2, _TilingAndOffset_9ddd838bfd244f55a750a6108b690efe_Out_3);
    float _SimpleNoise_1af52715d423420c89b5b0a5cf93b9ca_Out_2;
    Unity_SimpleNoise_float(_TilingAndOffset_9ddd838bfd244f55a750a6108b690efe_Out_3, 44, _SimpleNoise_1af52715d423420c89b5b0a5cf93b9ca_Out_2);
    float _InvertColors_6fec04fc82fc49d1b86b8276db343164_Out_1;
    float _InvertColors_6fec04fc82fc49d1b86b8276db343164_InvertColors = float(1
);    Unity_InvertColors_float(_SimpleNoise_1af52715d423420c89b5b0a5cf93b9ca_Out_2, _InvertColors_6fec04fc82fc49d1b86b8276db343164_InvertColors, _InvertColors_6fec04fc82fc49d1b86b8276db343164_Out_1);
    float _Multiply_057616e47d8e46cf809a7a4b526f628a_Out_2;
    Unity_Multiply_float(_Multiply_4bae29101975427190d3d5a8474e3408_Out_2, _InvertColors_6fec04fc82fc49d1b86b8276db343164_Out_1, _Multiply_057616e47d8e46cf809a7a4b526f628a_Out_2);
    float _Step_6e0caadf3e8844908837e8805a0f3c56_Out_2;
    Unity_Step_float(0.03, _Multiply_057616e47d8e46cf809a7a4b526f628a_Out_2, _Step_6e0caadf3e8844908837e8805a0f3c56_Out_2);
    float _Step_a33ee2e7ae624fbda13cbc0e09cdabd1_Out_2;
    Unity_Step_float(0.06, _Multiply_057616e47d8e46cf809a7a4b526f628a_Out_2, _Step_a33ee2e7ae624fbda13cbc0e09cdabd1_Out_2);
    float _Subtract_b581dc4d80f149428e6ab2706fec6101_Out_2;
    Unity_Subtract_float(_Step_6e0caadf3e8844908837e8805a0f3c56_Out_2, _Step_a33ee2e7ae624fbda13cbc0e09cdabd1_Out_2, _Subtract_b581dc4d80f149428e6ab2706fec6101_Out_2);
    float4 _Lerp_2882cd8c720b406ab46ee52cba9c58bb_Out_3;
    Unity_Lerp_float4(_Property_a5a5b05ffc0c492384b4cf3ba17beee0_Out_0, _Property_cb6c4a89fb1f4b9685691fef7a3a914c_Out_0, (_Subtract_b581dc4d80f149428e6ab2706fec6101_Out_2.xxxx), _Lerp_2882cd8c720b406ab46ee52cba9c58bb_Out_3);
    float4 Color_141aceccc3f64747b2f60f45630baa49 = IsGammaSpace() ? float4(0, 0, 0, 1) : float4(SRGBToLinear(float3(0, 0, 0)), 1);
    float4 _Property_23bf6fa254b44ce7871c69b3dc5f4a52_Out_0 = Color_e33a7c0bdbe944dd9d613d87127761f4;
    float4 _Lerp_358e35bcc94d485cb6f0f83fb4ab463e_Out_3;
    Unity_Lerp_float4(Color_141aceccc3f64747b2f60f45630baa49, (_Step_6e0caadf3e8844908837e8805a0f3c56_Out_2.xxxx), _Property_23bf6fa254b44ce7871c69b3dc5f4a52_Out_0, _Lerp_358e35bcc94d485cb6f0f83fb4ab463e_Out_3);
    surface.BaseColor = (_Lerp_2882cd8c720b406ab46ee52cba9c58bb_Out_3.xyz);
    surface.Alpha = (_Lerp_358e35bcc94d485cb6f0f83fb4ab463e_Out_3).x;
    return surface;
}

// --------------------------------------------------
// Build Graph Inputs

VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
{
    VertexDescriptionInputs output;
    ZERO_INITIALIZE(VertexDescriptionInputs, output);

    output.ObjectSpaceNormal = input.normalOS;
    output.ObjectSpaceTangent = input.tangentOS.xyz;
    output.ObjectSpacePosition = input.positionOS;

    return output;
}
    SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
{
    SurfaceDescriptionInputs output;
    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





    output.uv0 = input.texCoord0;
    output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
#else
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
#endif
#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

    return output;
}

    // --------------------------------------------------
    // Main

    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SpriteUnlitPass.hlsl"

    ENDHLSL
}
Pass
{
    Name "Sprite Unlit"
    Tags
    {
        "LightMode" = "UniversalForward"
    }

        // Render State
        Cull Off
    Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
    ZTest [unity_GUIZTestMode]
    ZWrite Off
    Stencil
   {
        Ref[_Stencil]
        Comp[_StencilComp]
        Pass[_StencilOp]
        ReadMask[_StencilReadMask]
        WriteMask[_StencilWriteMask]
   }
   ColorMask[_ColorMask]

        // Debug
        // <None>

        // --------------------------------------------------
        // Pass

        HLSLPROGRAM

        // Pragmas
        #pragma target 2.0
    #pragma exclude_renderers d3d11_9x
    #pragma vertex vert
    #pragma fragment frag

        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>

        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>

        // Defines
        #define _SURFACE_TYPE_TRANSPARENT 1
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_COLOR
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_COLOR
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_SPRITEFORWARD
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

        // --------------------------------------------------
        // Structs and Packing

        struct Attributes
    {
        float3 positionOS : POSITION;
        float3 normalOS : NORMAL;
        float4 tangentOS : TANGENT;
        float4 uv0 : TEXCOORD0;
        float4 color : COLOR;
        #if UNITY_ANY_INSTANCING_ENABLED
        uint instanceID : INSTANCEID_SEMANTIC;
        #endif
    };
    struct Varyings
    {
        float4 positionCS : SV_POSITION;
        float4 texCoord0;
        float4 color;
        #if UNITY_ANY_INSTANCING_ENABLED
        uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };
    struct SurfaceDescriptionInputs
    {
        float4 uv0;
        float3 TimeParameters;
    };
    struct VertexDescriptionInputs
    {
        float3 ObjectSpaceNormal;
        float3 ObjectSpaceTangent;
        float3 ObjectSpacePosition;
    };
    struct PackedVaryings
    {
        float4 positionCS : SV_POSITION;
        float4 interp0 : TEXCOORD0;
        float4 interp1 : TEXCOORD1;
        #if UNITY_ANY_INSTANCING_ENABLED
        uint instanceID : CUSTOM_INSTANCE_ID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
        #endif
    };

        PackedVaryings PackVaryings(Varyings input)
    {
        PackedVaryings output;
        output.positionCS = input.positionCS;
        output.interp0.xyzw = input.texCoord0;
        output.interp1.xyzw = input.color;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }
    Varyings UnpackVaryings(PackedVaryings input)
    {
        Varyings output;
        output.positionCS = input.positionCS;
        output.texCoord0 = input.interp0.xyzw;
        output.color = input.interp1.xyzw;
        #if UNITY_ANY_INSTANCING_ENABLED
        output.instanceID = input.instanceID;
        #endif
        #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
        output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
        #endif
        #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
        output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
        #endif
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        output.cullFace = input.cullFace;
        #endif
        return output;
    }

    // --------------------------------------------------
    // Graph

    // Graph Properties
    CBUFFER_START(UnityPerMaterial)
float4 Color_816a89c1c97a4080b2a084f920fb0e57;
float4 Color_e12935967060477e940097c6e44cef0c;
float2 Vector2_d5bad6c71f044480bad90f20a3ace79a;
float Vector1_e2a40ba1ccc346febf63d5b2ae43f41a;
float2 Vector2_cbaffc9ca7ea49a6bd7c98ecf689134d;
float2 Vector2_bfe039aafd5b4fd98c0f020b61b19045;
float4 Color_e33a7c0bdbe944dd9d613d87127761f4;
CBUFFER_END

// Object and Global properties

    // Graph Functions

void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
{
    Out = UV * Tiling + Offset;
}

void Unity_ColorMask_float(float3 In, float3 MaskColor, float Range, out float Out, float Fuzziness)
{
    float Distance = distance(MaskColor, In);
    Out = saturate(1 - (Distance - Range) / max(Fuzziness, 1e-5));
}

void Unity_Multiply_float(float A, float B, out float Out)
{
    Out = A * B;
}

void Unity_Multiply_float(float2 A, float2 B, out float2 Out)
{
    Out = A * B;
}


inline float Unity_SimpleNoise_RandomValue_float(float2 uv)
{
    return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
}

inline float Unity_SimpleNnoise_Interpolate_float(float a, float b, float t)
{
    return (1.0 - t) * a + (t * b);
}


inline float Unity_SimpleNoise_ValueNoise_float(float2 uv)
{
    float2 i = floor(uv);
    float2 f = frac(uv);
    f = f * f * (3.0 - 2.0 * f);

    uv = abs(frac(uv) - 0.5);
    float2 c0 = i + float2(0.0, 0.0);
    float2 c1 = i + float2(1.0, 0.0);
    float2 c2 = i + float2(0.0, 1.0);
    float2 c3 = i + float2(1.0, 1.0);
    float r0 = Unity_SimpleNoise_RandomValue_float(c0);
    float r1 = Unity_SimpleNoise_RandomValue_float(c1);
    float r2 = Unity_SimpleNoise_RandomValue_float(c2);
    float r3 = Unity_SimpleNoise_RandomValue_float(c3);

    float bottomOfGrid = Unity_SimpleNnoise_Interpolate_float(r0, r1, f.x);
    float topOfGrid = Unity_SimpleNnoise_Interpolate_float(r2, r3, f.x);
    float t = Unity_SimpleNnoise_Interpolate_float(bottomOfGrid, topOfGrid, f.y);
    return t;
}
void Unity_SimpleNoise_float(float2 UV, float Scale, out float Out)
{
    float t = 0.0;

    float freq = pow(2.0, float(0));
    float amp = pow(0.5, float(3 - 0));
    t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

    freq = pow(2.0, float(1));
    amp = pow(0.5, float(3 - 1));
    t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

    freq = pow(2.0, float(2));
    amp = pow(0.5, float(3 - 2));
    t += Unity_SimpleNoise_ValueNoise_float(float2(UV.x * Scale / freq, UV.y * Scale / freq)) * amp;

    Out = t;
}

void Unity_InvertColors_float(float In, float InvertColors, out float Out)
{
    Out = abs(InvertColors - In);
}

void Unity_Step_float(float Edge, float In, out float Out)
{
    Out = step(Edge, In);
}

void Unity_Subtract_float(float A, float B, out float Out)
{
    Out = A - B;
}

void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
{
    Out = lerp(A, B, T);
}

// Graph Vertex
struct VertexDescription
{
    float3 Position;
    float3 Normal;
    float3 Tangent;
};

VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
{
    VertexDescription description = (VertexDescription)0;
    description.Position = IN.ObjectSpacePosition;
    description.Normal = IN.ObjectSpaceNormal;
    description.Tangent = IN.ObjectSpaceTangent;
    return description;
}

// Graph Pixel
struct SurfaceDescription
{
    float3 BaseColor;
    float Alpha;
};

SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
{
    SurfaceDescription surface = (SurfaceDescription)0;
    float4 _Property_a5a5b05ffc0c492384b4cf3ba17beee0_Out_0 = Color_816a89c1c97a4080b2a084f920fb0e57;
    float4 _Property_cb6c4a89fb1f4b9685691fef7a3a914c_Out_0 = Color_e12935967060477e940097c6e44cef0c;
    float4 _UV_c951e84aabc84629826c5c8786956aad_Out_0 = IN.uv0;
    float2 _Property_8217c7c44da6420580bda81169d05f4b_Out_0 = Vector2_cbaffc9ca7ea49a6bd7c98ecf689134d;
    float2 _Property_88a5631585e8408fac91172837503399_Out_0 = Vector2_bfe039aafd5b4fd98c0f020b61b19045;
    float2 _TilingAndOffset_f3bb93582c4746f89e4de8d155edcdeb_Out_3;
    Unity_TilingAndOffset_float((_UV_c951e84aabc84629826c5c8786956aad_Out_0.xy), _Property_8217c7c44da6420580bda81169d05f4b_Out_0, _Property_88a5631585e8408fac91172837503399_Out_0, _TilingAndOffset_f3bb93582c4746f89e4de8d155edcdeb_Out_3);
    float _ColorMask_944869770e8146eeba1bf3f285cd94b4_Out_3;
    Unity_ColorMask_float((float3(_TilingAndOffset_f3bb93582c4746f89e4de8d155edcdeb_Out_3, 0.0)), IsGammaSpace() ? float3(0, 0, 0) : SRGBToLinear(float3(0, 0, 0)), 0, _ColorMask_944869770e8146eeba1bf3f285cd94b4_Out_3, 1.01);
    float _Multiply_4bae29101975427190d3d5a8474e3408_Out_2;
    Unity_Multiply_float(_ColorMask_944869770e8146eeba1bf3f285cd94b4_Out_3, 0.15, _Multiply_4bae29101975427190d3d5a8474e3408_Out_2);
    float2 _Property_aaba7eaba3ae415db170533ec8d79e25_Out_0 = Vector2_d5bad6c71f044480bad90f20a3ace79a;
    float _Property_e08f68b37b0847019d41a9624288b824_Out_0 = Vector1_e2a40ba1ccc346febf63d5b2ae43f41a;
    float _Multiply_57ad694eb4d248d989cb06fb2910c643_Out_2;
    Unity_Multiply_float(_Property_e08f68b37b0847019d41a9624288b824_Out_0, -1, _Multiply_57ad694eb4d248d989cb06fb2910c643_Out_2);
    float2 _Vector2_023abf684c5e4c479cc6ee370823f637_Out_0 = float2(0, _Multiply_57ad694eb4d248d989cb06fb2910c643_Out_2);
    float2 _Multiply_c79bf63b43384b8e99b447f22fb5fbec_Out_2;
    Unity_Multiply_float(_Vector2_023abf684c5e4c479cc6ee370823f637_Out_0, (IN.TimeParameters.x.xx), _Multiply_c79bf63b43384b8e99b447f22fb5fbec_Out_2);
    float2 _TilingAndOffset_9ddd838bfd244f55a750a6108b690efe_Out_3;
    Unity_TilingAndOffset_float(IN.uv0.xy, _Property_aaba7eaba3ae415db170533ec8d79e25_Out_0, _Multiply_c79bf63b43384b8e99b447f22fb5fbec_Out_2, _TilingAndOffset_9ddd838bfd244f55a750a6108b690efe_Out_3);
    float _SimpleNoise_1af52715d423420c89b5b0a5cf93b9ca_Out_2;
    Unity_SimpleNoise_float(_TilingAndOffset_9ddd838bfd244f55a750a6108b690efe_Out_3, 44, _SimpleNoise_1af52715d423420c89b5b0a5cf93b9ca_Out_2);
    float _InvertColors_6fec04fc82fc49d1b86b8276db343164_Out_1;
    float _InvertColors_6fec04fc82fc49d1b86b8276db343164_InvertColors = float(1
);    Unity_InvertColors_float(_SimpleNoise_1af52715d423420c89b5b0a5cf93b9ca_Out_2, _InvertColors_6fec04fc82fc49d1b86b8276db343164_InvertColors, _InvertColors_6fec04fc82fc49d1b86b8276db343164_Out_1);
    float _Multiply_057616e47d8e46cf809a7a4b526f628a_Out_2;
    Unity_Multiply_float(_Multiply_4bae29101975427190d3d5a8474e3408_Out_2, _InvertColors_6fec04fc82fc49d1b86b8276db343164_Out_1, _Multiply_057616e47d8e46cf809a7a4b526f628a_Out_2);
    float _Step_6e0caadf3e8844908837e8805a0f3c56_Out_2;
    Unity_Step_float(0.03, _Multiply_057616e47d8e46cf809a7a4b526f628a_Out_2, _Step_6e0caadf3e8844908837e8805a0f3c56_Out_2);
    float _Step_a33ee2e7ae624fbda13cbc0e09cdabd1_Out_2;
    Unity_Step_float(0.06, _Multiply_057616e47d8e46cf809a7a4b526f628a_Out_2, _Step_a33ee2e7ae624fbda13cbc0e09cdabd1_Out_2);
    float _Subtract_b581dc4d80f149428e6ab2706fec6101_Out_2;
    Unity_Subtract_float(_Step_6e0caadf3e8844908837e8805a0f3c56_Out_2, _Step_a33ee2e7ae624fbda13cbc0e09cdabd1_Out_2, _Subtract_b581dc4d80f149428e6ab2706fec6101_Out_2);
    float4 _Lerp_2882cd8c720b406ab46ee52cba9c58bb_Out_3;
    Unity_Lerp_float4(_Property_a5a5b05ffc0c492384b4cf3ba17beee0_Out_0, _Property_cb6c4a89fb1f4b9685691fef7a3a914c_Out_0, (_Subtract_b581dc4d80f149428e6ab2706fec6101_Out_2.xxxx), _Lerp_2882cd8c720b406ab46ee52cba9c58bb_Out_3);
    float4 Color_141aceccc3f64747b2f60f45630baa49 = IsGammaSpace() ? float4(0, 0, 0, 1) : float4(SRGBToLinear(float3(0, 0, 0)), 1);
    float4 _Property_23bf6fa254b44ce7871c69b3dc5f4a52_Out_0 = Color_e33a7c0bdbe944dd9d613d87127761f4;
    float4 _Lerp_358e35bcc94d485cb6f0f83fb4ab463e_Out_3;
    Unity_Lerp_float4(Color_141aceccc3f64747b2f60f45630baa49, (_Step_6e0caadf3e8844908837e8805a0f3c56_Out_2.xxxx), _Property_23bf6fa254b44ce7871c69b3dc5f4a52_Out_0, _Lerp_358e35bcc94d485cb6f0f83fb4ab463e_Out_3);
    surface.BaseColor = (_Lerp_2882cd8c720b406ab46ee52cba9c58bb_Out_3.xyz);
    surface.Alpha = (_Lerp_358e35bcc94d485cb6f0f83fb4ab463e_Out_3).x;
    return surface;
}

// --------------------------------------------------
// Build Graph Inputs

VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
{
    VertexDescriptionInputs output;
    ZERO_INITIALIZE(VertexDescriptionInputs, output);

    output.ObjectSpaceNormal = input.normalOS;
    output.ObjectSpaceTangent = input.tangentOS.xyz;
    output.ObjectSpacePosition = input.positionOS;

    return output;
}
    SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
{
    SurfaceDescriptionInputs output;
    ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





    output.uv0 = input.texCoord0;
    output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
#if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
#else
#define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
#endif
#undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

    return output;
}

    // --------------------------------------------------
    // Main

    #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SpriteUnlitPass.hlsl"

    ENDHLSL
}
    }
    FallBack "Diffuse"
}
