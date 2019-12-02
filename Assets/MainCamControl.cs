using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamControl : MonoBehaviour
{
    public Camera[] mOtherCams;
    private bool mFixedDone = false;

    private void FixedUpdate()
    {
        if (mFixedDone)
        {
            return;
        }

        foreach (var c in mOtherCams)
        {
            c.gameObject.SetActive(false);
        }
        mFixedDone = true;
    }

    private void LateUpdate()
    {
        mFixedDone = false;
        foreach (var c in mOtherCams)
        {
            c.gameObject.SetActive(true);
        }
    }
}
