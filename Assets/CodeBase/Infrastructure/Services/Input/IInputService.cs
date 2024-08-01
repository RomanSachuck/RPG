using Assets.CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Services.Input
{
    public interface IInputService : IService
    {
        public Vector2 MoveAxis { get; }
        public float LookAxisX { get; }
        public float LookAxisY { get; }
        public bool IsMainAttack { get; }
        public bool IsJump { get; }
    }
}
