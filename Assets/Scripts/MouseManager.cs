using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static void Lock(bool isLock)
    {
        Cursor.lockState = isLock ? CursorLockMode.Locked : CursorLockMode.None;
    }

    public static void Show(bool isShow)
    {
        Cursor.visible = isShow;
    }

}
