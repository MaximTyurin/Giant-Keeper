using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offSet;

    private void Start()
    {
        _target = FindObjectOfType<PlayerMover>().transform;
    }

    private void LateUpdate()
    {
        transform.position = _target.position + _offSet;
    }
}
