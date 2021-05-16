using UnityEngine;
using UnityEditor;

namespace TW
{
  [CustomEditor(typeof(TransparentWindow))]
  public class TransparentWindowEditor : UnityEditor.Editor
  {
    public override void OnInspectorGUI()
    {
      GUILayout.Label("As long as this script is in your scene when you create a Build, it should work just fine.\n" +
                      "This will automatically set the Camera's background colour, as well as change a setting that needs to be disabled in Player Settings.\n" +
                      "\nIf you want a GameObject to be ignored by the cursor, add the object to the 'IgnoreRaycast' layer.\n" +
                      "If you want a UI element to be ignored by your cursor, adding an 'IgnoreCursor' tag will do the trick!\n" + 
                      "\nFeel free to minimise this script.", 
                      EditorStyles.wordWrappedLabel);
    }
  }
}