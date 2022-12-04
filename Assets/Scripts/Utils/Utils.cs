using UnityEngine;

public static class Utils
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;

        return camera.ScreenToWorldPoint(position);
    }

    public static Color SetSpriteAlpha(SpriteRenderer sprite, float alpha)
    {
        return new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }
}
