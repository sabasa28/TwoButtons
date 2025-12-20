using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public static class CheckMobile
{
    [DllImport("__Internal")]
    private static extern bool IsMobile();
    public static bool isMobile()
    {
#if !UNITY_EDITOR && UNITY_WEBGL
            return IsMobile();
#endif
        return false;
    }
}
