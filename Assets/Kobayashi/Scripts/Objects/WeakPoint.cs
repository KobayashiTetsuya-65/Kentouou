using UnityEngine;
using UnityEngine.EventSystems;

public class WeakPoint : MonoBehaviour,IPointerClickHandler
{
    public int damage = 1;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.Hit = true;
        GameManager.Instance.Damege = damage;
        Destroy(gameObject);
    }
}
