using UnityEngine;

public class CursorController : Singleton<CursorController>
{
    [SerializeField] private Texture2D _standardCursor;
    [SerializeField] private Texture2D _HighlightCursor;
    
    public void SetInteractableCursor()
    {
        Cursor.SetCursor(_HighlightCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetStandardCursor()
    {
        Cursor.SetCursor(_standardCursor, Vector2.zero, CursorMode.Auto);
    }
}
