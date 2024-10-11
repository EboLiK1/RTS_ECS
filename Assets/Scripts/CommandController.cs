using CodeMonkey.Utils;
using Ecs;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private Entity _entity;

    [SerializeField] private Vector3 _point;

    [SerializeField] private Entity _enemy;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MoveToPosition(UtilsClass.GetMouseWorldPosition());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetMouseButtonDown(1))
        {
            PatrolPoints();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            AttackTarget(_enemy);
        }
    }

    public void MoveToPosition(Vector3 point)
    {
        _entity.SetData(new CommandRequest
        {
            Type = CommandType.MOVE_TO_POSITION,
            Args = point,
            Status = CommandStatus.IDLE
        });
    }

    public void PatrolPoints()
    {
        _entity.SetData(new CommandRequest()
        {
            Type = CommandType.PATROL_BY_POINTS,
            Args = _point,
            Status = CommandStatus.IDLE
        });
    }

    public void AttackTarget(Entity target)
    {
        _entity.SetData(new CommandRequest
        {
            Type = CommandType.ATTACK_TARGET,
            Args = target,
            Status = CommandStatus.IDLE
        });
    }
}