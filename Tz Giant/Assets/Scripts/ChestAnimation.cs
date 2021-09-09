using UnityEngine;

public class ChestAnimation : MonoBehaviour
{
    private Animator _animator;
    private string _openChestTrigger = "OpenChest";

    public delegate void ChestOpen();
    public static event ChestOpen OnWinEvent;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            _animator.SetTrigger(_openChestTrigger);
            OnWinEvent?.Invoke();
            
        }
    }
}
