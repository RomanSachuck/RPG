using UnityEngine;

namespace Assets.CodeBase.Data
{
    public static class DateExtensions
    {
        public static Vector3Data AsVector3Data(this Vector3 vector3) =>
            new(vector3.x, vector3.y, vector3.z);

        public static Vector3 AsVector3Unity(this Vector3Data vector) =>
            new(vector.X, vector.Y, vector.Z);

        public static Vector3 AddY(this Vector3 vector, float y)
        {
            vector.y += y;
            return vector; 
        }

        public static string ToJson(this object obj) =>
            JsonUtility.ToJson(obj);

        public static T ToDeserialized<T>(this string json) =>
            JsonUtility.FromJson<T>(json);
    }
}
