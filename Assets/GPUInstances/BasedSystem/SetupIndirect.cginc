#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
#include "./DataStructs.hlsl"
// #if defined(SHADER_API_GLCORE) || defined(SHADER_API_D3D11) || defined(SHADER_API_GLES3) || defined(SHADER_API_METAL) || defined(SHADER_API_VULKAN) || defined(SHADER_API_PSSL) || defined(SHADER_API_XBOXONE)
StructuredBuffer<MeshPropertiesData> _Props;
// To get instanding working, we must use UNITY_MATRIX_M / UNITY_MATRIX_I_M as UnityInstancing.hlsl redefine them
#endif


void TransformPosition_float(in float3 ObjectPosition, out float3 InstancePosition){
    InstancePosition = ObjectPosition;
}

// /** When redefining the unity_ObjectToWorld Matrix, its inverse is not redefine, which produce an error in the lighting
// https://forum.unity.com/threads/trying-to-rotate-instances-with-drawmeshinstancedindirect-shader-but-the-normals-get-messed-up.707600/
// */
float4x4 inverse(float4x4 input){
    #define minor(a,b,c) determinant(float3x3(input.a, input.b, input.c))
    float4x4 cofactors = float4x4(
        minor(_22_23_24, _32_33_34, _42_43_44),
        -minor(_21_23_24, _31_33_34, _41_43_44),
        minor(_21_22_24, _31_32_34, _41_42_44),
        -minor(_21_22_23, _31_32_33, _41_42_43),
        -minor(_12_13_14, _32_33_34, _42_43_44),
        minor(_11_13_14, _31_33_34, _41_43_44),
        -minor(_11_12_14, _31_32_34, _41_42_44),
        minor(_11_12_13, _31_32_33, _41_42_43),
        minor(_12_13_14, _22_23_24, _42_43_44),
        -minor(_11_13_14, _21_23_24, _41_43_44),
        minor(_11_12_14, _21_22_24, _41_42_44),
        -minor(_11_12_13, _21_22_23, _41_42_43),
        -minor(_12_13_14, _22_23_24, _32_33_34),
        minor(_11_13_14, _21_23_24, _31_33_34),
        -minor(_11_12_14, _21_22_24, _31_32_34),
        minor(_11_12_13, _21_22_23, _31_32_33)
        );
    #undef minor
    return transpose(cofactors) / determinant(input);
}


void setup(){
    #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
    #ifdef unity_ObjectToWorld
    #undef unity_ObjectToWorld
    #endif

    #ifdef unity_WorldToObject
    #undef unity_WorldToObject
    #endif

    MeshPropertiesData data                = _Props[unity_InstanceID];                           //retreive the data per instance from the structuredBuffer. unity_InstanceID is the index of each instance
                       unity_ObjectToWorld = mul(data.transform, mul(data.rotate, data.scale));  //redefine the Object to World matrix based on the matrices bounds to the shader via the StructuredBuffer
                    //    unity_WorldToObject = inverse(unity_ObjectToWorld);                       //define the World to Object as inverse of Object to World to retreive proper lighting. Without this line, the shadow are completly false as it doesn't take to account the rotation matrix
    #endif
}