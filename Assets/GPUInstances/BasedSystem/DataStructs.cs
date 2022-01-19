using UnityEngine;

namespace Bonjour.GPU{
    public struct MeshPropertiesData{
        public Matrix4x4 transform;
        public Matrix4x4 rotate;
        public Matrix4x4 scale;
        public Vector4 color;

        public static int Size(){
            return 
                sizeof(float) * 4 * 4 +
                sizeof(float) * 4 * 4 +
                sizeof(float) * 4 * 4 +
                sizeof(float) * 4;
        }
    }
}