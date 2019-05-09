using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnBrowser : MonoBehaviour
{
    
    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            gameObject.SetActive(false);
        }
    }

}
