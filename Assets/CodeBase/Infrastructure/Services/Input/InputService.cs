using UnityEngine;

namespace CodeBase.Services.Input
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        protected const string Attack = "Attack";
        protected const string HorizontalLook = "HorizontalLook";
        protected const string VerticalLook = "VerticalLook";
        protected const string MouseX = "Mouse X";
        protected const string MouseY = "Mouse Y";
        protected const float HorizontalLimiter = .2f;
        public abstract Vector2 MoveAxis { get; }
        public abstract float LookAxisX { get; }
        public abstract float LookAxisY { get; }
        public abstract bool IsMainAttack { get; }
        public abstract bool IsJump { get; } 
    }
}