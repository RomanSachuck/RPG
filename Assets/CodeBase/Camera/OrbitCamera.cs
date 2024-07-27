using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _sensitivityVert;

    [SerializeField] private float _minimumVert;
    [SerializeField] private float _maximumVert;

    private IInputService _inputService;

    private float _rotateX = 0;
    private float _rotateY;
    private Vector3 _offset;
    private void Awake() =>
        _inputService = Game.InputService;
    void Start()
    {
        _rotateY = transform.eulerAngles.y;
        _offset = _target.position - transform.position;
    }
    void LateUpdate()
    {
        _rotateY += _inputService.LookAxisX * _rotateSpeed;

        Quaternion rotation = Quaternion.Euler(0, _rotateY, 0);
        transform.position = _target.position - (rotation * _offset);

        _rotateX -= _inputService.LookAxisY * _sensitivityVert;
        _rotateX = Mathf.Clamp(_rotateX, _minimumVert, _maximumVert);

        transform.localEulerAngles = new Vector3(_rotateX, _rotateY, transform.localEulerAngles.z);
    }
}
