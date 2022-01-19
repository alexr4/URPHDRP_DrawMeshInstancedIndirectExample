using UnityEngine;

namespace Bonjour.GPU{
    public struct CubePropertiesData{
        public Matrix4x4        TRS;
        public Matrix4x4        transform;
        public Matrix4x4        rotate;
        public Matrix4x4        scale;
        public Matrix4x4        otransform;
        public Matrix4x4        oscale;
        public Vector4          color;
        public Vector4          data;
        public static int Size(){
                return
                sizeof(float) * 4 * 4 + //TRS matrix
                sizeof(float) * 4 * 4 + //translation matrix
                sizeof(float) * 4 * 4 + //rotation matrix
                sizeof(float) * 4 * 4 + //scale matrix
                sizeof(float) * 4 * 4 + //otransform matrix
                sizeof(float) * 4 * 4 + //oscale matrix
                sizeof(float) * 4 + // colors
                sizeof(float) * 4; // data
        }
    }
}