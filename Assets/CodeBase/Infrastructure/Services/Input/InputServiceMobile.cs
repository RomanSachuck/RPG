using UnityEngine;

namespace CodeBase.Services.Input
{
    public class InputServiceMobile : InputService
    {
        private const string ButtonAttackMainTag = "ButtonAttackMain";
        private const string ButtonJumpTag = "ButtonJump";
        public override Vector2 MoveAxis => GetMoveAxis();
        public override float LookAxisX => GetLookX();
        public override float LookAxisY => GetLookY();
        public override bool IsMainAttack => GetButtonAttackUp();
        public override bool IsJump => GetButtonJumpUp();

        private float _lookSensetive = .8f;
        private ButtonEvents _buttonAttackMain;
        private ButtonEvents _buttonJump;

        private Vector2 GetMoveAxis() =>
            new(SimpleInput.GetAxis(Horizontal) * HorizontalLimiter, SimpleInput.GetAxis(Vertical));
        private float GetLookX() =>
            (SimpleInput.GetAxis(HorizontalLook)) * _lookSensetive;
        private float GetLookY() =>
            (SimpleInput.GetAxis(VerticalLook)) * _lookSensetive;
        private bool GetButtonAttackUp()
        {
            if (_buttonAttackMain == null)
                _buttonAttackMain = FindButton(ButtonAttackMainTag);

            return _buttonAttackMain.ButtonIsPressed;
        }
        private bool GetButtonJumpUp()
        {
            if (_buttonJump == null)
                _buttonJump = FindButton(ButtonJumpTag);

            return _buttonJump.ButtonIsPressed;
        }
        private ButtonEvents FindButton(string buttonTag) =>
            GameObject.FindWithTag(buttonTag).GetComponent<ButtonEvents>();
    }
}
