using UnityEngine;

public class EnemyPartsForCheckPlayer : MonoBehaviour
{

    public delegate void Lose();
    public static event Lose OnLosedEvent;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMover playerMover))
        {
            OnLosedEvent?.Invoke();
        }
    }
}
