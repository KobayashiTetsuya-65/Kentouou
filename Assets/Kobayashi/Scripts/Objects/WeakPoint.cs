using UnityEngine;
using UnityEngine.EventSystems;

public class WeakPoint : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private bool _isWeak;
    public int damage = 1;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_isWeak)
        {
            GameManager.Instance.Hit = true;
        }
        else
        {
            GameManager.Instance.Defence = true;
        }
        GameManager.Instance.Damage = damage;
        DestroyWeakPoint();
    }
    private void OnDisable()
    {
        if (GlobalCursor.Instance != null)
        {
            GlobalCursor.Instance.SetNormalCursor();
        }
    }
    public void DestroyWeakPoint()
    {
        Destroy(gameObject);
    }
}
