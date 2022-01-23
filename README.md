# URPHDRP_DrawMeshInstancedIndirectExample
![GPUInstanceExample](https://i.imgur.com/2CzvTgO.gif)
This repository show how to use ```DrawMeshInstancedIndirect``` with ShaderGraph and grab a ```StructuredBuffer``` directly via shadergraph.
It use HDRP 13.X (also tested with HDRP 12.X)

## Notes on stability
Unity 2021.X seems to be unstable for this as the editor and build crash after a few runs due to a graph execution order error. 
The issue as been pointed out to Unity: [1379350](https://issuetracker.unity3d.com/issues/hdrp-render-graph-execution-error-on-renderpass-render-shadow-maps-was-not-provided-after-leaving-editor-open-for-a-while). This should be resolvef on latest 2021 version (2021.9)

Unity 2022.X works well and does not report this error.


## Reference used to make this example:
* https://forum.unity.com/threads/problem-with-drawmeshinstance-with-hdrp-or-lwrp.646723/
* https://www.uproomgames.com/dev-log/procedural-terrain
* https://twitter.com/Cyanilux/status/1396848736022802435