using Assets.CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure;
using CodeBase.Services.Input;
using UnityEngine;

namespace Assets.CodeBase.CameraLogic
{
    public class OrbitCamera : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 2;
        [SerializeField] private float _sensitivityVert = 1.1f;
        [SerializeField] private float _minimumVert = 1;
        [SerializeField] private float _maximumVert = 25;

        private Transform _target;
        private IInputService _inputService;

        private float _rotateX = 0;
        private float _rotateY = 0;
        [SerializeField] private float _offsetX;
        [SerializeField] private float _offsetY;
        [SerializeField] private float _offsetZ;

        private Vector3 _offset = new Vector3();

        private void Awake() =>
            _inputService = AllServices.Container.Single<IInputService>();

        void LateUpdate()
        {
            _rotateY += _inputService.LookAxisX * _rotateSpeed;
            _rotateX -= _inputService.LookAxisY * _sensitivityVert;
            _rotateX = Mathf.Clamp(_rotateX, _minimumVert, _maximumVert);

            Quaternion rotation = Quaternion.Euler(_rotateX, _rotateY, 0);
            UpdateOffset();
            transform.position = _target.position - rotation * _offset;

            transform.localEulerAngles = new Vector3(_rotateX, _rotateY, transform.localEulerAngles.z);
        }

        public void Follow(Transform target) =>
            _target = target;

        private void UpdateOffset()
        {
            _offset.x = _offsetX;
            _offset.y = _offsetY;
            _offset.z = _offsetZ;
        }
    }
}