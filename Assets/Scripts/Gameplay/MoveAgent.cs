using Ecs;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public sealed class MoveAgent : MonoBehaviour
{
    private const float STOPPING_DISTANCE = 0.3f;

    private Entity _unit;
    private Vector3 _destination;
    private NavMeshPath _navMeshPath;

    private Coroutine _moveCoroutine;

    private Vector3[] _pointPath;
    private int _pointer;

    private void Awake()
    {
        _unit = GetComponent<Entity>();
        _navMeshPath = new NavMeshPath();
    }

    #region Move
    public void MoveToPosition(Vector3 destination)
    {
        StopMove();
        StartMove(destination);
    }

    private void StartMove(Vector3 destination)
    {
        var pathGenerated = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, _navMeshPath);

        if (!pathGenerated)
        {
            return;
        }

        _pointer = 0;
        _pointPath = _navMeshPath.corners;

        _moveCoroutine = StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        var framePeriod = new WaitForFixedUpdate();

        while (_pointer < _pointPath.Length)
        {
            yield return framePeriod;

            MoveByPath();
        }

        StopMove();
    }

    private void MoveByPath()
    {
        var currentPosiiton = transform.position;
        var targetPosition = _pointPath[_pointer];
        var distanceVector = targetPosition - currentPosiiton;

        var isTargetReached = distanceVector.sqrMagnitude <= STOPPING_DISTANCE;
        if (isTargetReached)
        {
            _pointer++;
            return;
        }

        MoveUnit(targetPosition);
    }

    private void MoveUnit(Vector3 destination)
    {
        _unit.SetData(new CommandRequest
        {
            Type = CommandType.MOVE_TO_POSITION,
            Args = destination,
            Status = CommandStatus.IDLE
        });
    }

    private void StopMove()
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }
    #endregion
}