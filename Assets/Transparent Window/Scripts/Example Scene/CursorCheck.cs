using UnityEngine;
using UnityEngine.UI;

namespace TW
{
  public class CursorCheck : MonoBehaviour
  {
    public CheckType checkType = CheckType.UI;
    Text text;

    void Start() => text = GetComponent<Text>();

    void Update()
    {
      if (!text) return; // If there's no text element attached, don't do anything

      // Set text value based on the mode of the check, as well as whether or not that check succeeds
      switch (checkType)
      {
        case CheckType.UI:
          text.text = $"Cursor over UI: {MouseEvents.IsPointerOverUIObject()}";
          break;
        case CheckType.GO3D:
          text.text = $"Cursor over 3D: {MouseEvents.IsPointerOverGameObject()}";
          break;
        case CheckType.GO2D:
          text.text = $"Cursor over 2D: {MouseEvents.IsPointerOverGameObject2D()}";
          break;
        case CheckType.Any:
          text.text = $"Cursor over ANY: {MouseEvents.IsPointerOverObjectAtAll()}";
          break;
      }
    }
  }

  public enum CheckType
  {
    UI = 0,
    GO3D = 1,
    GO2D = 2,
    Any = 3
  }
}