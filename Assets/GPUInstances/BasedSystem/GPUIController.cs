using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bonjour.GPU{
    public class GPUIController : MonoBehaviour
    {
        //based params
        public int maxInstances;
        public Vector3 world;
        public Mesh basedMesh;
        public Material instanceMaterial;

        //data

        //privates
        private Bounds bounds;
        protected ComputeBuffer meshPropertiesBuffer;
        protected ComputeBuffer argsBuffer;
        

        //Compute Shader
        public ComputeShader compute;
        private int kernel;

        private void Start() {
            Init();
        }

        private void Update() {
            BindDataToComputeShader();
            //Run compute shader
            ComputeShaderExtensions.DispatchThreads(ref compute, ref kernel, new Vector3Int(maxInstances, 1, 1));
            //call DMII
            Graphics.DrawMeshInstancedIndirect(basedMesh, 0, instanceMaterial, bounds, argsBuffer);
        }

        private void OnDisable() {
            meshPropertiesBuffer?.Dispose();
            meshPropertiesBuffer = null;

            argsBuffer?.Dispose();
            argsBuffer = null;
        }

        protected void Init(){
            // Boundary surrounding the meshes we will be drawing.  Used for occlusion.
            bounds = new Bounds(transform.position, world);
            
            InitArgsBuffer();
            InitPropertiesBuffer();

            compute = Instantiate(compute);
            kernel = compute.FindKernel("CSMain");
            compute.SetBuffer(kernel, "_Props", meshPropertiesBuffer);

            instanceMaterial = Instantiate(instanceMaterial);
            instanceMaterial.SetBuffer("_Props", meshPropertiesBuffer);
        }

        protected void InitArgsBuffer(){
            //Set ComputeBuffer used for DrawMeshInstanceIndirect with the based mesh
            uint[] args = new uint[5]{0, 0, 0, 0, 0};
            //arguments for drawing mesh
            //0 = number of triangle indices, 1 = maxInstance, other are only relevant for drawing submeshes
            args[0] = (uint) basedMesh.GetIndexCount(0);
            args[1] = (uint) maxInstances;
            args[2] = (uint) basedMesh.GetIndexStart(0);
            args[3] = (uint) basedMesh.GetBaseVertex(0);

            argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
            argsBuffer.SetData(args);
        }

        protected virtual void InitPropertiesBuffer(){
            MeshPropertiesData[] props = new MeshPropertiesData[maxInstances];
            for(int i=0; i<maxInstances; i++){
                MeshPropertiesData data = new MeshPropertiesData();

                //Set params for each Instance ...
                Vector3 pos = new Vector3(
                    (Random.value * 2 - 1) * world.x * 0.5f,
                    (Random.value * 2 - 1) * world.y * 0.5f,
                    (Random.value * 2 - 1) * world.z * 0.5f
                );
                pos += transform.position;
                
                Quaternion rotation = Quaternion.AngleAxis(Random.value * 180f, Random.insideUnitSphere);

                float uniformScale = Random.value;
                Vector3 scale = new Vector3(uniformScale, uniformScale, uniformScale);

                Vector4 color = new Vector4(Random.value, Random.value, Random.value, Random.value);

                data.transform  = Matrix4x4.Translate(pos);
                data.rotate     = Matrix4x4.Rotate(rotation);
                data.scale      = Matrix4x4.Scale(scale);
                data.color      = color;
                
                props[i] = data;
            }

            //Create Compute Buffer and feed it with MeshPropertiesData Array
            meshPropertiesBuffer = new ComputeBuffer(maxInstances, MeshPropertiesData.Size());
            meshPropertiesBuffer.SetData(props);
        }

        protected virtual void BindDataToComputeShader(){
            //... Bind uniform to Compute Shader Here

            Matrix4x4 TRS = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            compute.SetMatrix("_TRS", TRS);
        }
    }

}
