using Ecs;

public class SelectionEntity : Entity
{
    protected override void Init()
    {
        SetData(new SelectionDataComponent 
        { 
            IsSelecting = false,
        });
    }
}