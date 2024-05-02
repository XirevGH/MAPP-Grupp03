using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{

    public static Vector2 QuadraticBezierCurve(Vector2 startPosition, Vector2 endPosition, Vector2 controlPoint, float time)
    {
        Vector2 p0 = Vector2.Lerp(startPosition, controlPoint, time);
        Vector2 p1 = Vector2.Lerp(controlPoint, endPosition, time);

        return Vector2.Lerp(p0, p1, time);
    }

    public static Vector2 CalculateControlPoint(Vector2 startPosition, Vector2 endPosition, float controlPointOffSet, bool aboveLine)
    {
        Vector2 controlPoint = startPosition + (startPosition + endPosition).normalized * 0.5f;
        float sign = aboveLine ? -1f : 1f;
        controlPoint -= new Vector2(0, sign * controlPointOffSet);

        return controlPoint;
    }
}
