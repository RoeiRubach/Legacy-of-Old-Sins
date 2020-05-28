using UnityEngine;

public class CursorController : Singleton<CursorController>
{
    [SerializeField] private Texture2D _standardCursor;
    [SerializeField] private Texture2D _HighlightCursor;
    [SerializeField] private Texture2D _douglasShootingCursor;
    [SerializeField] private Texture2D _elenaAssassinCursor;
    [SerializeField] private Texture2D _HectorTechCursor;


    public void SetInteractableCursor()
    {
        Cursor.SetCursor(_HighlightCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetStandardCursor()
    {
        Cursor.SetCursor(_standardCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetShootingCursor()
    {
        Cursor.SetCursor(_douglasShootingCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetAssassinCursor()
    {
        Cursor.SetCursor(_elenaAssassinCursor, Vector2.zero, CursorMode.Auto);
    }

    public void SetTechCursor()
    {
        Cursor.SetCursor(_HectorTechCursor, Vector2.zero, CursorMode.Auto);
    }
}
