using UnityEngine;

namespace TW
{
  public class CursorTracker : MonoBehaviour
  {
    private void Update() => transform.position = MouseEvents.CursorWorldPosition();
  } 
}
