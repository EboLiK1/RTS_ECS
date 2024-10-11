using Ecs;
using System.Collections.Generic;
using UnityEngine;

public class SelectionBehaviour : EntityBehaviour
{
    protected override IEnumerable<ISystem> ProvideSystems()
    {
        MouseInput mouse = new MouseInput();
        yield return mouse;

        RectSelectionInputSystem rectSelectionInputSystem = new RectSelectionInputSystem(mouse);
        yield return rectSelectionInputSystem;

        GameObject selectionArea = Resources.Load<GameObject>("SelectionArea");
        GameObject area = Instantiate(selectionArea);
        RectSelectionView rectSelectionView = new RectSelectionView(area.transform);

        yield return new RectSelectionViewControllerSystem(rectSelectionView);

        PointUnitSelector pointUnitSelector = new PointUnitSelector(EcsModule.Instance.SelectedUnitsStack);
        RectUnitSelector rectUnitSelector = new RectUnitSelector(EcsModule.Instance.SelectedUnitsStack);

        yield return new UnitSelectionSystem(rectSelectionInputSystem, pointUnitSelector, rectUnitSelector);
        yield return new SelectedUnitsMoveControllSystem(mouse);
    }
}