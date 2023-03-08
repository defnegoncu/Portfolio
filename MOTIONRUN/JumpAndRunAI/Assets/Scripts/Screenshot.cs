using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Screenshot : MonoBehaviour{

    private bool takeScreenshot;

    private void OnEnable(){
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
    }

    private void OnDisable(){
        RenderPipelineManager.endCameraRendering -= RenderPipelineManager_endCameraRendering;
    }

    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext arg1, Camera arg2){

        if (takeScreenshot){

            takeScreenshot = false;

            int width = Screen.width;
            int heigt = Screen.height;

            Texture2D screenshotTexture = new Texture2D(width, heigt, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, width, heigt);

            screenshotTexture.ReadPixels(rect, 0, 0);
            screenshotTexture.Apply();

            byte[] byteArray = screenshotTexture.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.dataPath + "/CameraScreenshot.png", byteArray);
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.T)){
            takeScreenshot = true;
        }
    }


}
