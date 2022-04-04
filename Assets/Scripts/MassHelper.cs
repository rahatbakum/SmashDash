using UnityEngine;

public static class MathHelper
{
    ///<summary>
    ///returns 1/x
    ///</summary>
    public static float InverseValue(float value) => 1 / value;

    public static float MiddlePoint(float point1, float point2) => (point1 + point2) / 2;
    public static Vector3 MiddlePoint(Vector3 point1, Vector3 point2) => (point1 + point2) / 2;
}
