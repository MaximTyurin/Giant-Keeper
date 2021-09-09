using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private WayPoint[] _targets;
    [SerializeField] private float _detectionDistance = 3f;
    [SerializeField] [Range(0, 360)] private float _angle;
    [SerializeField] private float _radius;

    private NavMeshAgent _meshAgent;
    [SerializeField] private GameObject _targetPlayer;

    private int _currentTargetIndex;
    private bool _canSeePlayer;
    
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstructionMask;

    public float Radius => _radius;
    public float Angle => _angle;
    
    public GameObject TargetPlayer => _targetPlayer;

    public bool CanSeePlayer => _canSeePlayer;

    private void Start()
    {
        _meshAgent = GetComponent<NavMeshAgent>();
        _targets = FindObjectsOfType(typeof(WayPoint)) as WayPoint[];
        _targetPlayer = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(_targetPlayer.transform.position, _meshAgent.transform.position);
        if (distanceToPlayer <= _detectionDistance || _canSeePlayer)
            MoveToPlayer();
        else
            MovementEnemyPatrol();
    }

    private void MoveToPlayer()
    {
        _meshAgent.SetDestination(_targetPlayer.transform.position);
    }

    private void MovementEnemyPatrol()
    {
        if (Vector3.Distance(transform.position, _targets[_currentTargetIndex].transform.position) < 1f)
            ChangeCurrentTargetIndex();
        if (Vector3.Distance(transform.position, _targets[_currentTargetIndex].transform.position) >= 1f)
            _meshAgent.SetDestination(_targets[_currentTargetIndex].transform.position);
    }
    
    private void ChangeCurrentTargetIndex()
    {
        _currentTargetIndex = Random.Range(0, _targets.Length);
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) == false)
                    _canSeePlayer = true;
                else
                    _canSeePlayer = false;
            }
            else
                _canSeePlayer = false;
        }
        else if (_canSeePlayer)
            _canSeePlayer = false;
    }
}
