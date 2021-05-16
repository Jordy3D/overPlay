using UnityEngine;
using UnityEngine.UI;

namespace TW
{
  public class ButtonMover : MonoBehaviour
  {
    #region Bounds
    Vector2 screenSize; // Size of the screen
    Vector2 moveBounds; // Size of the space the button can move to 
    #endregion
    #region References
    RectTransform my; // The Transform of the button
    Button myBody;  // The actual Button component of the button
    #endregion

    void Awake()
    {
      // Obtain the button's references
      my = GetComponent<RectTransform>();
      myBody = GetComponent<Button>();

      // Store the current screen resolution and size of the button
      var res = Screen.currentResolution;
      var mySize = my.sizeDelta;

      // Store the screen size and generate the potential movement bounds from that and the object size
      screenSize = new Vector2(res.width, res.height);
      moveBounds = new Vector2(screenSize.x - mySize.x, screenSize.y - mySize.y);
    }

    public void MoveButton()
    {
      // Generate a new X and Y position for the button to move to
      float newX = Random.Range(-moveBounds.x / 2, moveBounds.x / 2);
      float newY = Random.Range(-moveBounds.y / 2, moveBounds.y / 2);

      // Move the button to the newly generated position
      my.anchoredPosition = new Vector2(newX, newY);

      // This looks strange, but resets the button graphic to Normal (open to a one-line for that if found!)
      myBody.interactable = false;
      myBody.interactable = true;
    }
  } 
}
