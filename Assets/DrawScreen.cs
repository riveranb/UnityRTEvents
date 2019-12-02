using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class DrawScreen : MonoBehaviour
{
    public RenderTexture mRT;

    // Start is called before the first frame update
    private void OnPreRender()
    {
        Graphics.Blit(mRT, null as RenderTexture);
    }
}
