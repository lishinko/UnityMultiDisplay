using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Horizon.HMI
{
    public class PremultiplyColorPass : ScriptableRenderPass
    {
        public PremultiplyColorPass(Material material, RenderTexture rt)
        {
            renderPassEvent = RenderPassEvent.AfterRendering;
            _fullScreenTexture = rt;
            _material = material;
            _fullScreenMesh = new Mesh();
            //ndc空间mesh
            var v = new Vector3[4];
            v[0] = new Vector3(-1.0f, -1.0f, -1.0f);
            v[1] = new Vector3(1.0f, -1.0f, -1.0f);
            v[1] = new Vector3(-1.0f, 1.0f, -1.0f);
            v[1] = new Vector3(1.0f, 1.0f, -1.0f);
            _fullScreenMesh.vertices = v;
            var indices = new int[6]
            {
                0, 1, 2,
                2,1,3,
            };
            _fullScreenMesh.triangles = indices;
            _fullScreenMesh.MarkModified();
        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            ref var cameraData = ref renderingData.cameraData;
            var cmd = CommandBufferPool.Get("PremultiplyColorPass");

            cmd.Clear();
            //cmd.CopyTexture(cameraData.renderer.cameraColorTargetHandle, 0, 0, _fullScreenTexture, 0, 0);
            CoreUtils.SetRenderTarget(cmd, _fullScreenTexture);
            //Blitter.BlitTexture(cmd, cameraData.renderer.cameraColorTargetHandle, new Vector4(1, 1, 0, 0), 0.0f, false);
            ////将所得的贴图用于渲染
            _material.SetTexture("_MainTex", _fullScreenTexture);
            cmd.DrawMesh(_fullScreenMesh, Matrix4x4.identity, _material);

            CoreUtils.SetRenderTarget(cmd, cameraData.renderer.cameraColorTargetHandle);
            context.ExecuteCommandBuffer(cmd);
            context.Submit();
            CommandBufferPool.Release(cmd);

        }
        private Mesh _fullScreenMesh;
        private Material _material;
        private RenderTexture _fullScreenTexture;
    }
}
