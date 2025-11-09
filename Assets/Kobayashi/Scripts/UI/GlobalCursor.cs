using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GlobalCursor : MonoBehaviour
{
    public static GlobalCursor Instance { get; private set; }

    [Header("カーソル画像")]
    [SerializeField] private Image _cursorImage;

    [Header("オフセット")]
    [SerializeField] private Vector2 _offset = Vector2.zero;

    [Header("感度")]
    [SerializeField] public float Sensitivity = 1f;

    private Canvas _canvas;
    private Vector2 _mousePos;
    private Vector2 _cursorPos;
    private Vector2 _prevMousePos;
    private Vector2 _delta;
    private Vector2 _canvasPos;
    private GraphicRaycaster _raycaster;
    private EventSystem _eventSystem;
    private void Awake()
    {
        if(Instance != null && Instance == this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Cursor.visible = false;
    }
    private void Start()
    {
        _canvas = _cursorImage.canvas;
        _raycaster = _canvas.GetComponent<GraphicRaycaster>();
        _eventSystem = EventSystem.current;
        _cursorPos = Input.mousePosition;
        _prevMousePos = _cursorPos;
    }
    // Update is called once per frame
    void Update()
    {
        if (_cursorImage == null) return;

        _mousePos = Input.mousePosition;
        _delta = _mousePos - _prevMousePos;
        _cursorPos += _delta * Sensitivity;
        _cursorPos.x = Mathf.Clamp(_cursorPos.x, 0, Screen.width);
        _cursorPos.y = Mathf.Clamp(_cursorPos.y, 0, Screen.height);
        _prevMousePos = _mousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            _cursorPos,
            _canvas.worldCamera,
            out _canvasPos
        );
        _cursorImage.rectTransform.anchoredPosition = _canvasPos + _offset;
        if (Input.GetMouseButtonDown(0))
        {
            TryClickUI(_cursorPos);
        }
    }
    private void TryClickUI(Vector2 screenPos)
    {
        if (_raycaster == null || _eventSystem == null)
            return;

        PointerEventData pointerData = new PointerEventData(_eventSystem)
        {
            position = screenPos
        };

        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(pointerData, results);

        if (results.Count > 0)
        {
            // 最前面のUI要素にクリックイベントを送る
            GameObject target = results[0].gameObject;
            ExecuteEvents.ExecuteHierarchy(target, pointerData, ExecuteEvents.pointerClickHandler);
        }
    }
    public void SetSensitivity(float value)
    {
        Sensitivity = value;
    }
}
