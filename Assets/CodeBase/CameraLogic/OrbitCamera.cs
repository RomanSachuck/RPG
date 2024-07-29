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
        private Vector3 _offset = new Vector3(0.03f, -2.50f, 4.00f);

        private void Awake() =>
            _inputService = Game.InputService;

        void LateUpdate()
        {
            _rotateY += _inputService.LookAxisX * _rotateSpeed;

            Quaternion rotation = Quaternion.Euler(0, _rotateY, 0);
            transform.position = _target.position - rotation * _offset;

            _rotateX -= _inputService.LookAxisY * _sensitivityVert;
            _rotateX = Mathf.Clamp(_rotateX, _minimumVert, _maximumVert);

            transform.localEulerAngles = new Vector3(_rotateX, _rotateY, transform.localEulerAngles.z);
        }

        public void Follow(Transform target) => 
            _target = target;
    }
}