using Assets.CodeBase.Data;
using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase;
using CodeBase.Services.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.CodeBase.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private HeroAnimator _animator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _movementSpeed;

        [SerializeField] private float _jumpSpeed = 5.0f;
        [SerializeField] private float _gravity = -9.8f;
        [SerializeField] private float _terminalVelocity = -10.0f;
        [SerializeField] private float _minFall = -1.5f;
        private float _verticalSpeed;

        private IInputService _inputService;
        private Camera _camera;

        private bool _isJumpingState;
        private ControllerColliderHit _contact;

        void OnControllerColliderHit(ControllerColliderHit hit) =>
            _contact = hit;

        private void Awake() =>
            _inputService = AllServices.Container.Single<IInputService>();

        private void Start() =>
            _camera = Camera.main;

        private void Update()
        {
            Vector3 movementVector;
            float rotateSpeed = 10f;
            float currentSpeed = _movementSpeed;

            movementVector = CalculateMovementVector();

            if (_inputService.MoveAxis.sqrMagnitude > Consts.Epsilon)
            {
                if (IsMovingForward())
                {
                    LookMovementDirection(movementVector, rotateSpeed);
                    _animator.PlayMoveForward(_characterController.velocity.magnitude);
                }
                else
                {
                    LookMovementDirection(-movementVector, rotateSpeed);
                    _animator.PlayMoveBack();
                }
            }
            else
            {
                _animator.ResetToIdle();
            }

            if (_animator.IsAttacking && !_isJumpingState)
                currentSpeed /= 4;
            _characterController.Move(currentSpeed * movementVector * Time.deltaTime);
        }

        private Vector3 CalculateMovementVector()
        {
            Quaternion cameraRotation = _camera.transform.rotation;
            _camera.transform.eulerAngles = new Vector3(20, _camera.transform.eulerAngles.y, 0);
            Vector3 movementVector = _camera.transform.TransformDirection(_inputService.MoveAxis);
            _camera.transform.rotation = cameraRotation;

            movementVector = CalculateJumpAndColission(movementVector);
            movementVector.Normalize();
            return movementVector;
        }

        public Vector3 CalculateJumpAndColission(Vector3 movementVector)
        {
            bool hitGround = RayCastGround();

            if (hitGround)
            {
                if (_inputService.IsJump)
                {
                    _verticalSpeed = _jumpSpeed;
                    StartJump();
                }
                else
                {
                    _verticalSpeed = _minFall;

                    if (_isJumpingState)
                        EndJump();
                }
            }
            else
            {
                _verticalSpeed += _gravity * 5 * Time.deltaTime;

                if (_verticalSpeed < _terminalVelocity)
                    _verticalSpeed = _terminalVelocity;

                if (_characterController.isGrounded)
                {
                    if (Vector3.Dot(movementVector, _contact.normal) < 0)
                    {
                        movementVector = _contact.normal;
                        EndJump();
                    }
                    else
                    {
                        movementVector += _contact.normal;
                        EndJump();
                    }
                }
            }

            return new Vector3(movementVector.x, _verticalSpeed, movementVector.z);
        }

        private bool RayCastGround()
        {
            RaycastHit hit;

            if (_verticalSpeed < 0 && Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Vector3.down, out hit))
            {
                float check = _characterController.height / 1.55f;
                return hit.distance <= check;
            }
            else
                return false;
        }

        private void StartJump()
        {
            _animator.PlayJumpStart();
            _isJumpingState = true;
        }

        private void EndJump()
        {
            _animator.PlayJumpEnd();
            _isJumpingState = false;
        }

        private bool IsMovingForward() =>
            _inputService.MoveAxis.y >= 0;

        private void LookForward(float rotateSpeed)
        {
            Vector3 forward = _camera.transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            transform.forward = Vector3.Slerp(transform.forward, forward, rotateSpeed * Time.deltaTime);
        }

        private void LookMovementDirection(Vector3 movementVector, float rotateSpeed) =>
            transform.forward = Vector3.Slerp(transform.forward, new Vector3(movementVector.x, 0, movementVector.z), rotateSpeed * Time.deltaTime);

        public void UpdateProgress(PlayerProgress progress) => 
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVector3Data());

        public void LoadProgress(PlayerProgress progress)
        {
            if(CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null)
                    Warp(to: savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsVector3Unity().AddY(_characterController.height);
            _characterController.enabled = true;
        }

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}