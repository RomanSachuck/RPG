using UnityEngine;

public class HeroMove : MonoBehaviour
{
    [SerializeField] private HeroAnimator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed;
    

    private IInputService _inputService;
    private Camera _camera;
    private float _heroRotationLimit;

    private void Awake() =>
        _inputService = Game.InputService;

    private void Start() =>
        _camera = Camera.main;

    private void Update()
    {
        Vector3 movementVector = Vector3.zero;
        float rotateSpeed = 10f;
        float currentSpeed = _movementSpeed;

        if (_inputService.MoveAxis.sqrMagnitude > Consts.Epsilon)
        {
            movementVector = CalculateMovementVector();

            if (MovingForward())
            {
                LookMovementDirection(movementVector, rotateSpeed);
                _animator.PlayMoveForward(_characterController.velocity.sqrMagnitude);
            } 
            else
            {
                LookForward(rotateSpeed);

                if (MovingRight())
                    _animator.PlayMoveRight();
                else if(MovingLeft())
                    _animator.PlayMoveLeft();
                else
                    _animator.PlayMoveBack();

                //currentSpeed = _movementSpeed / 3;
            }     
        }
        else
        {
            _animator.ResetToIdle();
        }

        movementVector += Physics.gravity;
        _characterController.Move((currentSpeed * movementVector * Time.deltaTime));
    }

    private Vector3 CalculateMovementVector()
    {
        Vector3 movementVector = _camera.transform.TransformDirection(_inputService.MoveAxis);
        movementVector.y = 0;
        movementVector.Normalize();
        return movementVector;
    }

    private bool MovingForward() =>
        _inputService.MoveAxis.y > _heroRotationLimit;

    private bool MovingRight() =>
        _inputService.MoveAxis.y <= _heroRotationLimit && _inputService.MoveAxis.x > 0;

    private bool MovingLeft() =>
        _inputService.MoveAxis.y <= _heroRotationLimit && _inputService.MoveAxis.x < 0;

    private void LookForward(float rotateSpeed)
    {
        Vector3 forward = _camera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        transform.forward = Vector3.Slerp(transform.forward, forward, rotateSpeed * Time.deltaTime);
    }

    private void LookMovementDirection(Vector3 movementVector, float rotateSpeed) =>
        transform.forward = Vector3.Slerp(transform.forward, movementVector, rotateSpeed * Time.deltaTime);
}
