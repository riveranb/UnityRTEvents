using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DrawScreen : MonoBehaviour
{
    private RenderTexture mRT;

    private void Start()
    {
        mRT = Camera.main.targetTexture;
        Camera.main.aspect = mRT.width / mRT.height;
    }

    // Start is called before the first frame update
    private void OnPreRender()
    {
        Graphics.Blit(mRT, null as RenderTexture);
    }
}
