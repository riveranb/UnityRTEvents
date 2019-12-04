/// <summary>
/// Unity script component class creating and holding a full-window sized render target for
/// corresponding camera.
/// </summary>

using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Camera))]
public class FullRenderTarget : MonoBehaviour
{
    #region _Members
    private Camera mCam;
    #endregion

    #region _Methods
    public RenderTexture RecreateRenderTexture()
    {
        if (mCam.targetTexture)
        {
            mCam.targetTexture.Release();
        }

        var rt = new RenderTexture(Screen.width, Screen.height, 24,
            RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
        rt.useMipMap = false;
        rt.Create();

        mCam.targetTexture = rt;
        return rt;
    }
    #endregion

    #region _Unity_MonoBehavior
    private void Awake()
    {
        mCam = GetComponent<Camera>();

        RecreateRenderTexture();
    }
    #endregion

}

