using UnityEngine;
using UnityEngine.EventSystems;

public abstract class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    virtual public void OnDrag(PointerEventData eventData) { }

    virtual public void OnPointerDown(PointerEventData eventData) { }

    virtual public void OnPointerUp(PointerEventData eventData) { }

}
