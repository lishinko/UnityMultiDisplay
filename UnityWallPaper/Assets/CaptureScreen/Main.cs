using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(UrpSetting());
    }

    IEnumerator UrpSetting()
    {
        yield return new WaitForSeconds(1.0f);
        var urp = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        if (urp != null)
        {
            urp.msaaSampleCount = 4;
            Debug.Log($"msaa = 4");
        }

        var _cam = Camera.main;
        var _data = _cam.GetComponent<UniversalAdditionalCameraData>();
        _data.antialiasing = AntialiasingMode.None;
    }
}
