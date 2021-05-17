using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TW
{
  public class UIPointerEvents : PointerEvents
  {
    public Animator animator;

    string param;

    void Start()
    {
      animator = GetComponent<Animator>();
    }

    public void SetAnimatorParamater(string _param) => param = _param;
    public void SetAnimatorState(bool _state) => animator.SetBool(param, _state);
  }

}