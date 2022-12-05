using UnityEngine;

public static class MathBezier
{
    public static Vector3 GetMidlePoint(Vector3 startPoint, Vector3 endPoint, float height = 0)
    {
        float middlePointRange = Vector3.Distance(startPoint, endPoint) / 2;

        Vector3 middlePoint = startPoint + middlePointRange / (startPoint - endPoint).magnitude * (endPoint - startPoint);

        Vector3 targetPoint = new Vector3(middlePoint.x, middlePoint.y + height, middlePoint.z);

        return targetPoint;
    }

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
            3f * oneMinusT * oneMinusT * t * p1 +
            3f * oneMinusT * t * t * p2 +
            t * t * t * p3;
    }

    public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;

        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
            6f * oneMinusT * t * (p2 - p1) +
            3f * t * t * (p3 - p2);
    }
}