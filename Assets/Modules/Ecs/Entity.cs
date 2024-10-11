using UnityEngine;

namespace Ecs
{
    [DefaultExecutionOrder(-5000)]
    public class Entity : MonoBehaviour
    {
        private const int UNDEFINED = -1;

        private int _id = UNDEFINED;
        public int Id => _id;

        private void OnEnable()
        {
            _id = EcsModule.World.CreateEntity();
            Init();
        }

        private void OnDisable()
        {
            EcsModule.World.DestroyEntity(_id);
            _id = UNDEFINED;
        }

        protected virtual void Init() { }

        public bool IsExists() => _id >= 0;

        public ref T GetData<T>() where T : struct
        {
            return ref EcsModule.World.GetComponent<T>(_id);
        }

        public void SetData<T>(T component) where T : struct
        {
            EcsModule.World.SetComponent(_id, ref component);
        }

        public void SendEvent<T>(T data) where T : struct
        {
            EcsModule.World.SendEvent<T>(_id, data);
        }
    }
}