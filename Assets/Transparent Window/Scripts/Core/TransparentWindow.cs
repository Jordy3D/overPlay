using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TW
{
  public class TransparentWindow : MonoBehaviour
  {
    #region DLL Imports
    [DllImport("User32")] private static extern IntPtr GetActiveWindow();
    [DllImport("User32")] private static extern int SetWindowLong(IntPtr _hWnd, int _nIndex, uint _dwNewLong);
    [DllImport("User32", SetLastError = true)] static extern bool SetWindowPos(IntPtr _hWnd, IntPtr _hWindInsertAfter, int _x, int _y, int _cx, int _cy, uint _uFlags);
    [DllImport("Dwmapi.dll")] private static extern uint DwmExtendFrameIntoClientArea(IntPtr _hWnd, ref Margins _margins); 
    #endregion
    private struct Margins
    {
      public int cxLeftWidth;
      public int cxRightWidth;
      public int cyTopHeight;
      public int cyBottomHeight;
    }

    const int GWL_EXSTYLE = -20;

    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    private IntPtr hWnd;

    private void Start()
    {
      #region User-Proofing
      Camera.main.clearFlags = CameraClearFlags.SolidColor; // Set the Camera's clear flags to solid colour to allow transparency
      Camera.main.backgroundColor = new Color(0, 0, 0, 0);  // Set the background color to Black and Transparent

      if (!FindObjectOfType<EventSystem>())                 // If there's no EventSystem component in the scene...
        gameObject.AddComponent<EventSystem>();             // ... add one, since it's needed for the UI and cursor tracking to work.

      if (Application.isEditor) return;                     // If you're running this in Editor, don't make it transparent! 
      #endregion

      hWnd = GetActiveWindow();

      Margins margins = new Margins { cxLeftWidth = -1 };
      DwmExtendFrameIntoClientArea(hWnd, ref margins);

      SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
      SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);

      Application.runInBackground = true;
    }

    #region Clickthrough Checking
    //private void Update() => SetClickThrough(!(MouseEvents.IsPointerOverUIObject() ||     // Enable Clicking through if the Pointer
    //                                           MouseEvents.IsPointerOverGameObject()) ||  // is NOT over a UI or Game Object
    //                                           MouseEvents.IsPointerOverGameObject2D()    //
    //                                        );   
    private void Update() => SetClickThrough(!MouseEvents.IsPointerOverObjectAtAll());

    private void SetClickThrough(bool _clickThough)
    {
      if (_clickThough) SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
      else SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
    }

    #endregion

#if UNITY_EDITOR
    /// <summary>
    /// Disables the Flip Model Swapchain setting in PlayerSettings
    /// </summary>
    public static void DisableFlipModelSwapchain() => UnityEditor.PlayerSettings.useFlipModelSwapchain = false; 
#endif
  }

  public static class MouseEvents
  {
    /// <summary>
    /// Returns true if pointer is over an element on the Canvas
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverUIObject()
    {
      PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
      eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
      List<RaycastResult> rayObjects = new List<RaycastResult>();
      EventSystem.current.RaycastAll(eventDataCurrentPosition, rayObjects);
      
      List<RaycastResult> results = new List<RaycastResult>();
      foreach (var rayObject in rayObjects)
        if (!rayObject.gameObject.CompareTag("IgnoreCursor"))
          results.Add(rayObject);

      return results.Count > 0;
    }

    /// <summary>
    /// Returns true if pointer is over a GameObject with a Collider
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverGameObject()
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      return Physics.Raycast(ray, out RaycastHit hit);
    }

    /// <summary>
    /// Returns true if pointer is over a 2D GameObject with a Collider
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverGameObject2D()
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);
      return hit;
    }

    /// <summary>
    /// Returns true if any of the other IsPointerOver functions return true
    /// </summary>
    /// <returns></returns>
    public static bool IsPointerOverObjectAtAll()
    {
      return IsPointerOverGameObject() || IsPointerOverGameObject2D() || IsPointerOverUIObject();
    }

    /// <summary>
    /// Returns the cursor's position on the screen directly
    /// </summary>
    /// <returns></returns>
    public static Vector2 CursorScreenPosition()
    {
      return Input.mousePosition;
    }
    /// <summary>
    /// Returns the cursor's position on the screen converted to World Space
    /// </summary>
    /// <returns></returns>
    public static Vector3 CursorWorldPosition()
    {
      Vector3 cursorPos = Input.mousePosition;
      cursorPos.z = Camera.main.nearClipPlane;
      return Camera.main.ScreenToWorldPoint(cursorPos);
    }
  } 
}