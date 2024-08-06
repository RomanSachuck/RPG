using UnityEngine;

namespace Assets.CodeBase.Tools
{
    public static class PhysicsDebug
    {
        public static void DrawSphereDebug(Vector3 worldPos, float radius, float timeInSec)
        {
            Debug.DrawRay(worldPos, Vector3.up, Color.red, timeInSec);
            Debug.DrawRay(worldPos, Vector3.down, Color.red, timeInSec);
            Debug.DrawRay(worldPos, Vector3.left, Color.red, timeInSec);
            Debug.DrawRay(worldPos, Vector3.right, Color.red, timeInSec);
            Debug.DrawRay(worldPos, Vector3.forward, Color.red, timeInSec);
            Debug.DrawRay(worldPos, Vector3.back, Color.red, timeInSec);
        }
    }
}
