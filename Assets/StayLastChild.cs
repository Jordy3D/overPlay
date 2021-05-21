using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StayLastChild : MonoBehaviour
{
  private void Update()
  {
    transform.SetAsLastSibling();
  }
}
