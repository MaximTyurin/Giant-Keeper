using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private FixedJoystick _fixedJoystick;

    private string _moveAnim = "Move";
    private string _dyingAnim = "Dying";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _fixedJoystick = FindObjectOfType<FixedJoystick>();
    }

    private void OnEnable()
    {
        EnemyPartsForCheckPlayer.OnLosedEvent += DyingAnimation;
    }

    private void OnDisable()
    {
        EnemyPartsForCheckPlayer.OnLosedEvent -= DyingAnimation;
    }

    private void Update()
    {
        MoveAnimation();
    }

    private void MoveAnimation()
    {
        if(_fixedJoystick.Horizontal != 0 || _fixedJoystick.Vertical != 0)
        {
            _animator.SetBool(_moveAnim, true);
        }
        else
        {
            _animator.SetBool(_moveAnim, false);
        }

    }

    private void DyingAnimation()
    {
        _animator.SetTrigger(_dyingAnim);
    }
}
