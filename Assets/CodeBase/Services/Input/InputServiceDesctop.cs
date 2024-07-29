using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputServiceDesctop : InputService
    {
        public override Vector2 MoveAxis => GetMoveAxis();
        public override float LookAxisX => GetLookX();
        public override float LookAxisY => GetLookY();
        public override bool IsMainAttack => GetButtonAttackUp();
        public override bool IsJump => GetButtonJump();

        private Vector2 GetMoveAxis() =>
            new(UnityEngine.Input.GetAxis(Horizontal) * HorizontalLimiter, UnityEngine.Input.GetAxis(Vertical));
        private float GetLookX() =>
            UnityEngine.Input.GetAxis(MouseX);
        private float GetLookY() =>
            UnityEngine.Input.GetAxis(MouseY);
        private bool GetButtonAttackUp()
        {
            if (!PointerOverUiElement())
                return UnityEngine.Input.GetMouseButtonUp(0);
            else return false;
        }
        private bool GetButtonJump() =>
            UnityEngine.Input.GetKey(KeyCode.Space);
        private static bool PointerOverUiElement() =>
            UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }
}
