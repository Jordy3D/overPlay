using UnityEngine;

namespace TW
{
  /// <summary>
  /// Taken from https://github.com/Jordy3D/BaneTools
  /// </summary>
  public class RotateConstantly : MonoBehaviour
  {
    public Space space;
    public Axis axis;
    public float rotateSpeed;

    void Update()
    {
      transform.Rotate(new Vector3(
        axis == Axis.X || axis == Axis.XY || axis == Axis.XZ || axis == Axis.XYZ ? 1 : 0,
        axis == Axis.Y || axis == Axis.XY || axis == Axis.YZ || axis == Axis.XYZ ? 1 : 0,
        axis == Axis.Z || axis == Axis.XZ || axis == Axis.YZ || axis == Axis.XYZ ? 1 : 0)
        * rotateSpeed * Time.deltaTime,
        space == Space.Local ? UnityEngine.Space.Self : UnityEngine.Space.World);
    }
  }

  public enum Space
  {
    Local = 0,
    World = 1
  }
  public enum Axis
  {
    X = 0,
    Y = 1,
    Z = 2,
    XY = 3,
    XZ = 4,
    YZ = 5,
    XYZ = 6
  }
}