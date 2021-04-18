using UnityEngine;

public static class QuaternionExtensions 
{
    public static Quaternion With(this Quaternion original, float? x = null, float? y = null, float? z = null, float? w = null)
    {
        return new Quaternion(x ?? original.x, y ?? original.y, z ?? original.z, w ?? original.w);
    }
}
