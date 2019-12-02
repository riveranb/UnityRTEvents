using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BlitScreen : MonoBehaviour
{
    private Camera mCam;
    public RenderTexture mRT;

    private void Start()
    {
        mCam = GetComponent<Camera>();
    }

    private void OnPreRender()
    {
        mCam.targetTexture = mRT;
    }

    private void OnPostRender()
    {
        mCam.targetTexture = null;
        Graphics.Blit(mRT, null as RenderTexture);
    }
}
