using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Horizon.HMI
{
    public class CaptureScreenRenderFeature : ScriptableRendererFeature
    {
        public RenderTexture RT;
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
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
                    Debug.Log($"���������Ѿ����ݵ���Ⱦ�߳�");
                }
            }
        }
        public void CaptureOnce()
        {
            if (_capture)
            {
                Debug.LogWarning($"�Ѿ��ڽ�����");
                return;
            }
            _capture = true;
        }

        public override void Create()
        {
            _capturePass = new CaptureScreenPass(RT);
        }
        private CaptureScreenPass _capturePass;
        private bool _capture;
    }
}
