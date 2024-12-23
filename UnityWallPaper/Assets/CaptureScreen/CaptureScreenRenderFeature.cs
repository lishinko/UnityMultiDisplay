using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Horizon.HMI
{
    public class CaptureScreenRenderFeature : ScriptableRendererFeature
    {
        public bool PremultiplyColor = false;
        public Material PremultiplyMaterial;
#if UNITY_EDITOR
        public RenderTexture RT;
#endif
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            if (PremultiplyColor)
            {
                if (renderingData.cameraData.renderType == CameraRenderType.Base)
                {
                    if (renderingData.cameraData.cameraType == CameraType.Game)
                    {
                        renderer.EnqueuePass(_premultiplyColorPass);
                    }
                }
            }

#if UNITY_EDITOR
            if (!_capture)
            {
                return;
            }
            if (renderingData.cameraData.renderType == CameraRenderType.Base)
            {
                if (renderingData.cameraData.cameraType == CameraType.Game)
                {
                    renderer.EnqueuePass(_capturePass);
                    _capture = false;
                    Debug.Log($"截屏命令已经传递到渲染线程");
                }
            }
#endif
        }
#if UNITY_EDITOR
        public void CaptureOnce()
        {
            if (_capture)
            {
                Debug.LogWarning($"已经在截屏了");
                return;
            }
            _capture = true;
        }
#endif

        public override void Create()
        {
#if UNITY_EDITOR
            _capturePass = new CaptureScreenPass(RT);
#endif
            _premultiplyRT = new RenderTexture(Screen.width, Screen.height, RT.depth);
            _premultiplyColorPass = new PremultiplyColorPass(PremultiplyMaterial, _premultiplyRT);
        }
        private PremultiplyColorPass _premultiplyColorPass;
#if UNITY_EDITOR
        private RenderTexture _premultiplyRT;
        private CaptureScreenPass _capturePass;
        private bool _capture;
#endif
    }
}
