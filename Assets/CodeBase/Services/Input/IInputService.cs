using UnityEngine;

public interface IInputService
{
    public Vector2 MoveAxis { get; }
    public float LookAxisX { get; }
    public float LookAxisY { get; }
    public bool IsAttack { get; }
}
