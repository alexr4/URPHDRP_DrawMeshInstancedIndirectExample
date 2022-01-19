//https://docs.unity3d.com/Packages/com.unity.shadergraph@12.0/manual/Custom-Function-Node.html
void GetColorFromInstance_float(out float4 Out){
    #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
    MeshPropertiesData props = _Props[unity_InstanceID];
    Out = props.color;
    #else
    Out = float4(1, 1, 1, 1);
    #endif
}