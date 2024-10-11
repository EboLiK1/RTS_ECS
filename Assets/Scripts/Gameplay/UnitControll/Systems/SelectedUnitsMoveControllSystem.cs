using CodeMonkey.Utils;
using Ecs;
using System.Collections.Generic;
using UnityEngine;

public sealed class SelectedUnitsMoveControllSystem : IFixedUpdateSystem
{
    #region Первый варик
    //private List<MoveAgent> _selectedAgents = new List<MoveAgent>();

    //public void OnUpdate(int entityIndex)
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        var units = EcsModule.Instance.SelectedUnitsStack.GetUnits();

    //        if (units.Count > 0)
    //        {
    //            _selectedAgents.Clear();
    //            foreach (var unit in units)
    //            {
    //                if (unit.TryGetComponent(out MoveAgent agent))
    //                {
    //                    _selectedAgents.Add(agent);
    //                }
    //            }
    //            var destination = UtilsClass.GetMouseWorldPosition();

    //            MoveToPosition(_selectedAgents, destination);
    //        }
    //    }
    //}

    //private void MoveToPosition(List<MoveAgent> agents, Vector3 destination)
    //{
    //    foreach (var agent in agents)
    //    {
    //        agent.MoveToPosition(destination);
    //    }
    //}
    #endregion

    private MouseInput _mouse;

    private List<MoveAgent> _selectedAgents = new List<MoveAgent>();
    private Vector3 _movePosition;

    public SelectedUnitsMoveControllSystem(MouseInput mouse)
    {
        _mouse = mouse;

        _mouse.OnRightClicked += MoveToPosition;
    }

    public void OnFixedUpdate(int entityIndex)
    {
        
    }

    private void MoveToPosition(Vector3 movePosition)
    {
        var units = EcsModule.Instance.SelectedUnitsStack.GetUnits();

        if (units.Count > 0)
        {
            _selectedAgents.Clear();
            foreach (var unit in units)
            {
                if (unit.TryGetComponent(out MoveAgent agent))
                {
                    _selectedAgents.Add(agent);
                }
            }

            MoveToPosition(_selectedAgents, movePosition);
        }
    }

    private void MoveToPosition(List<MoveAgent> agents, Vector3 movePosition)
    {
        foreach (var agent in agents)
        {
            agent.MoveToPosition(movePosition);
        }
    }
}