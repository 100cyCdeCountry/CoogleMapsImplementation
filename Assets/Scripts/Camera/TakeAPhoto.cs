using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAPhoto : MonoBehaviour
{
    
    public RenderTexture photography;
    public Camera photoCamera;
    public Texture2D textureTemp;
    public Rect screenRect;
    public Renderer m_Display;

    private bool takePhoto;

    void Start() {
        screenRect = photoCamera.rect;
        textureTemp = new Texture2D((int)screenRect.width, (int)screenRect.height);
    }

    public void TakePhoto() {
        photoCamera.targetTexture = photography;
        RenderTexture.active = photography;

        takePhoto = true;
    }

    private void OnPostRender()
    {
        if (takePhoto)
        {
            //Create a new texture with the width and height of the screen
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            texture.Apply();

            //Reset the grab state
            takePhoto = false;
                
            RenderTexture.active = null;
            photoCamera.targetTexture = null;
        }
    }


    private IEnumerator RenderToPhoto2() {
        photoCamera.targetTexture = photography;
        RenderTexture.active = photography;

        yield return 0;
        yield return new WaitForEndOfFrame();

        photoCamera.Render();

        textureTemp.ReadPixels(screenRect, 0, 0);
        textureTemp.Apply ();

        RenderTexture.active = null;
        photoCamera.targetTexture = null;

    }
}
