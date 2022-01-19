void GetRandomFromInstance_float(in int useRamp, out float2 Out){
    Out = float2(0.5, .5);
    #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
    CubePropertiesData props = _Props[unity_InstanceID];
    if(useRamp != 0) Out.x = saturate(props.color.w);
    #endif
}