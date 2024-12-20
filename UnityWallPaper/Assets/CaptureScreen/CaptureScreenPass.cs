using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
namespace Horizon.HMI
{
    public class CaptureScreenPass : ScriptableRenderPass
    {
        public CaptureScreenPass(RenderTexture rt)
        {
            renderPassEvent = RenderPassEvent.AfterRendering;
            _rt = rt;
            if (rt != null)
            {
                _tex = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
            }
        }
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if(_tex == null)
            {
                return;
            }
            Rect rect = new Rect { width = _tex.width, height = _tex.height };
            Debug.Log($"rect = {rect}");
            //renderingData.cameraData.targetTexture;
            //Camera.main.targetTexture = _rt;
            //Camera.main.Render();
            _tex.ReadPixels(rect, 0, 0);
            //var col = _tex.GetPixel(1280 - 400, 200);
            var col = _tex.GetPixel( 100, 200);
            Debug.Log($"col = {col}");
            //Camera.main.targetTexture = null;
            _tex.Apply();
            var bytes = _tex.EncodeToPNG();
            File.WriteAllBytes("D:/rt.png", bytes);
        }
        private Texture2D _tex;
        private RenderTexture _rt;
    }
}
