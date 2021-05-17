using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace TW
{
  public class PointerEvents : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IPointerExitHandler
  {
    public UnityEvent onPointerDown, onPointerUp, onPointerClick, onPointerEnter, onPointerExit;

    public void OnPointerDown(PointerEventData eventData) => onPointerDown.Invoke();
    public void OnPointerUp(PointerEventData eventData) => onPointerUp.Invoke();
    public void OnPointerClick(PointerEventData eventData) => onPointerClick.Invoke();
    public void OnPointerEnter(PointerEventData eventData) => onPointerEnter.Invoke();
    public void OnPointerExit(PointerEventData eventData) => onPointerExit.Invoke();

    public void DebugPrint(string _output) => print(_output);
  } 
}
