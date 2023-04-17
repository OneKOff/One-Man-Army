using UnityEngine;

public class UtilityForVector
{
    public static Vector3 ClampVector(Vector3 vector, float maxMagnitude)
    {
        float innateMagnitude = vector.magnitude;

        if (innateMagnitude > maxMagnitude)
        {
            vector = vector.normalized * maxMagnitude;
        }

        return vector;
    }
}
