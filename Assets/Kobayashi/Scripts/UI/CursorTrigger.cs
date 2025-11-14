using UnityEngine;
using UnityEngine.EventSystems;

public class CursorTrigger : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        GlobalCursor.Instance.SetAttackCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GlobalCursor.Instance.SetNormalCursor();
    }
}
