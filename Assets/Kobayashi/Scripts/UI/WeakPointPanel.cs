using UnityEngine;
using UnityEngine.EventSystems;

public class WeakPointPanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _damage = 10;
    private WeakPoint _weakPoint;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.Miss = true;
        GameManager.Instance.Damage = _damage;
        _weakPoint = FindAnyObjectByType<WeakPoint>();
        if (_weakPoint != null)
        _weakPoint.DestroyWeakPoint();
    }
}
