using UnityEngine;
using UnityEngine.UI;

public class GlobalCursor : MonoBehaviour
{
    public static GlobalCursor Instance { get; private set; }

    [Header("カーソル画像")]
    [SerializeField] private Image _cursorImage;

    [Header("オフセット")]
    [SerializeField] private Vector2 _offset = Vector2.zero;

    private Canvas _canvas;
    private Vector2 _mousePos;
    private Vector2 _cursorPos;
    private Vector2 _canvasPos;
    private void Awake()
    {
        if(Instance != null && Instance != this)
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
        _cursorPos = Input.mousePosition;
    }
    // Update is called once per frame
    void Update()
    {
        if (_cursorImage == null) return;

        _mousePos = Input.mousePosition;
        _cursorPos.x = Mathf.Clamp(_cursorPos.x, 0, Screen.width);
        _cursorPos.y = Mathf.Clamp(_cursorPos.y, 0, Screen.height);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform,
            _mousePos,
            _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera,
            out _canvasPos
        );
        _cursorImage.rectTransform.anchoredPosition = _canvasPos + _offset;
    }
}
