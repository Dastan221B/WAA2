using System;
using System.Collections;
using UnityEngine;

public static class VectorExtension
{
    public static Vector2Int ToVector2IntInHeroPosition(this Vector3 vector)
    {
        int x = (int)Math.Ceiling(vector.x);
        int z = (int)Math.Ceiling(vector.z);

        return new Vector2Int(x, z);
    }
}