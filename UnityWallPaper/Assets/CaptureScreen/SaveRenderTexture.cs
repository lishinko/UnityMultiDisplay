using UnityEngine;
using System.IO;

public class SaveRenderTexture : MonoBehaviour
{
    public Camera renderCamera;
    public RenderTexture renderTexture;

    [ContextMenu("保存当前画面")]
    void SaveData()
    {
        if (renderCamera == null || renderTexture == null)
        {
            Debug.LogError("Camera or RenderTexture is not assigned!");
            return;
        }

        renderCamera.targetTexture = renderTexture;

        renderCamera.Render();
        SaveAsImage();
    }

    void SaveAsImage()
    {
        Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);

        RenderTexture currentRT = RenderTexture.active;

        RenderTexture.active = renderTexture;

        texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();

        RenderTexture.active = currentRT;

        byte[] pngData = texture.EncodeToPNG();
        //File.WriteAllBytes(Application.dataPath + "/SavedImage.png", pngData);
        File.WriteAllBytes("D:/SavedImage.png", pngData);

        Debug.Log("Image saved to " + Application.dataPath + "/SavedImage.png");
    }
}
