using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonjour.GPU{
    public class GPUICube : GPUIController
    {
        [Header("Per instance params")]
        public float minSize;
        public float maxSize;
        public float minRot;
        public float maxRot;
        public Color colorA;
        public Color colorB;

        [Header("Behaviors Params")]
        public float offsetTime;
        public float minTime;
        public float maxTime;
        public float amplitude;
        public float maxAmplitude;

        protected override void InitPropertiesBuffer()
        {
            CubePropertiesData[] props = new CubePropertiesData[maxInstances];

            for(int i=0; i<maxInstances; i++){
                CubePropertiesData data = new CubePropertiesData();

                Vector3 pos = new Vector3(
                    (Random.value * 2 - 1) * world.x * 0.5f,
                    (Random.value * 2 - 1) * world.y * 0.5f,
                    (Random.value * 2 - 1) * world.z * 0.5f
                );
                pos += transform.position;

                Quaternion rotation = Quaternion.identity;

                Vector3 scale = new Vector3(
                    Random.value * (maxSize - minSize) + minSize,
                    Random.value * (maxSize - minSize) + minSize,
                    Random.value * (maxSize - minSize) + minSize
                );

                Vector4 color = new Vector4(Random.value, Random.value, Random.value, Random.value);

                float orientation = Random.value;
                Vector4 direction = Vector4.zero;
                if(orientation < 0.33f){
                    direction = new Vector4(1, 0, 0, 0);
                }else if(orientation < 0.66f){
                    direction = new Vector4(0, 1, 0, 0);
                }else{
                    direction = new Vector4(0, 0, 1, 0);
                }

                data.TRS        = Matrix4x4.TRS(pos, rotation, scale);
                data.transform  = Matrix4x4.Translate(pos);
                data.rotate     = Matrix4x4.Rotate(rotation);
                data.scale      = Matrix4x4.Scale(scale);
                data.otransform = Matrix4x4.Translate(pos);
                data.oscale     = Matrix4x4.Scale(scale);
                data.color      = color;
                data.data       = new Vector4(i, 0, 0, Random.value);

                props[i] = data;
            }
            meshPropertiesBuffer = new ComputeBuffer(maxInstances, CubePropertiesData.Size());
            meshPropertiesBuffer.SetData(props);
        }

        protected override void BindDataToComputeShader()
        {
            compute.SetFloat("_OffsetTime", offsetTime);
            compute.SetFloat("_MinTime", minTime);
            compute.SetFloat("_MaxTime", maxTime);
            compute.SetFloat("_MinSize", minSize);
            compute.SetFloat("_MaxSize", maxSize);
            compute.SetFloat("_Time", Time.time);
            compute.SetFloat("_Amplitude", amplitude);
            compute.SetFloat("_MaxAmplitude", maxAmplitude);
            base.BindDataToComputeShader();
        }
    }
}

