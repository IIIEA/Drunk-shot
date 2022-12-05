using TMPro;
using UnityEngine;

public static class Utils
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;

        return camera.ScreenToWorldPoint(position);
    }

    public static Color SetAlpha(SpriteRenderer sprite, float alpha)
    {
        return new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }

    public static Color SetAlpha(TMP_Text text, float alpha)
    {
        return new Color(text.color.r, text.color.g, text.color.b, alpha);
    }

    public static Vector2 GetPositionWithYOffset(Vector2 position, float yOffset)
    {
        return new Vector2(position.x, position.y - yOffset);
    }

    public static float GetUpperScreenBorder(float offset = 0)
    {
        var rightUpBorder = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        return rightUpBorder.y + offset;
    }
}
