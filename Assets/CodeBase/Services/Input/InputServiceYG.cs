using UnityEngine;

public class InputServiceYG : IInputService
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string Attack = "Attack";
    private const float MobileSensetive = .3f;

    public Vector2 MoveAxis { get => GetMoveAxis(); }
    public float LookAxisX { get => GetLookX(); }
    public float LookAxisY { get => GetLookY(); }
    public bool IsAttack { get => GetButtonAttackUp(); }

    private Vector2 GetMoveAxis()
    {
        Vector2 simpleInput = SimpleInputAxis();

        if (simpleInput.sqrMagnitude <= Consts.Epsilon)
            return StandaloneInputAxis();
        else 
            return simpleInput;
    }
    private float GetLookX()
    {
        float mobileTouch = MobileTouchLookAxisX();

        if (mobileTouch == 0)
            return StandaloneLookAxisX();
        else
            return mobileTouch;
    }
    private float GetLookY()
    {
        float mobileTouch = MobileTouchLookAxisY();

        if (mobileTouch == 0)
            return StandaloneLookAxisY();
        else
            return mobileTouch;
    }
    private bool GetButtonAttackUp()
    {
        bool simpleInput = GetSimpleInputButtonUp();
        bool standoloneInput = GetMouseButtonUp();

        return simpleInput || standoloneInput;
    }

    private Vector2 SimpleInputAxis() =>
        new(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    private Vector2 StandaloneInputAxis() =>
        new(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    private float MobileTouchLookAxisX()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if (PointerOverUiElement(Input.GetTouch(0).fingerId))
                return 0;
            else
                return Input.touches[0].deltaPosition.x * MobileSensetive;
        }  
        else 
            return 0;
    }
    private float MobileTouchLookAxisY()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            if (PointerOverUiElement(Input.GetTouch(0).fingerId))
                return 0;
            else
                return Input.touches[0].deltaPosition.y * MobileSensetive;
        }
        else 
            return 0;
    }
    private float StandaloneLookAxisX() =>
        Input.GetAxis("Mouse X");
    private float StandaloneLookAxisY() =>
        Input.GetAxis("Mouse Y");
    private bool GetSimpleInputButtonUp() =>
        SimpleInput.GetButtonUp(Attack);
    private bool GetMouseButtonUp()
    {
        if (!PointerOverUiElement())
            return Input.GetMouseButtonUp(0);
        else return false;
    }
    private static bool PointerOverUiElement() =>
        UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    private static bool PointerOverUiElement(int touchCount) =>
        UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(touchCount);
}
