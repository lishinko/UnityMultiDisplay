using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;
namespace Horizon.HMI
{
    public class RTSaver : MonoBehaviour
    {
        public RenderTexture RT;
        public CaptureScreenRenderFeature SRF;
        [ContextMenu("保存当前画面")]
        public void Save()
        {
            SRF.CaptureOnce();
            //ScreenCapture.CaptureScreenshot("D:/rt.png");
            return;
            if (RT != null)
            {
                var tex = new Texture2D(RT.width, RT.height, TextureFormat.RGBA32, false);
                Rect rect = new Rect { width = tex.width, height = tex.height };
                Debug.Log($"rect = {rect}, w = {Screen.width}, h = {Screen.height}");
                Camera.main.targetTexture = RT;
                //Camera.main.Render();
                tex.ReadPixels(rect, 0, 0);
                Camera.main.targetTexture = null;
                var bytes = tex.EncodeToPNG();
                tex.Apply();
                File.WriteAllBytes("D:/rt.png", bytes);
            }
            else
            {
                var tex = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
                Rect rect = new Rect { width = tex.width, height = tex.height };
                Debug.Log($"rect = {rect}, w = {Screen.width}, h = {Screen.height}");
                tex.ReadPixels(rect, 0, 0);
                var bytes = tex.EncodeToPNG();
                tex.Apply();
                File.WriteAllBytes("D:/rt.png", bytes);
            }
        }
    }
}
