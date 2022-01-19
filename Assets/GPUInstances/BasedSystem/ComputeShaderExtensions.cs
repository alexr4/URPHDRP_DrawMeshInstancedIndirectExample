using UnityEngine;

static class ComputeShaderExtensions
{
    // Execute a compute shader with specifying a minimum number of thread
    // count not by a thread GROUP count.
    public static void DispatchThreads(ref ComputeShader compute, ref int kernel, Vector3Int resolutions)
    {
        uint x, y, z;
        compute.GetKernelThreadGroupSizes(kernel, out x, out y, out z);
        
        // Debug.Log($"Try dispatch kernel with {x}×{y}×{z}");
        
        int threadGroupSizeX = Mathf.CeilToInt(resolutions.x / x);
        int threadGroupSizeY = Mathf.CeilToInt(resolutions.y / y);
        int threadGroupSizeZ = Mathf.CeilToInt(resolutions.z / z);

        compute.Dispatch(kernel, threadGroupSizeX, threadGroupSizeY, threadGroupSizeZ);
    }
}