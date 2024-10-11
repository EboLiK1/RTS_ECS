using Ecs;
using UnityEngine;

public sealed class CharacterEntity : Entity
{
    [SerializeField] private CharacterConfig _config;
    [SerializeField] private AnimatorMachine _animator;

    protected override void Init()
    {
        SetData(new MoveSpeedComponent
        {
            Value = _config.MoveSpeed
        });

        SetData(new CombatComponent
        {
            Damage = _config.Damage,
            AttackDistance = _config.MinDistance,
            MinTimeBetweenAttack = _config.MinTimeBetweenAttack,
            MaxTimeBetweenAttack = _config.MaxTimeBetweenAttack,
            DamageType = _config.DamageType
        });

        if (_animator != null)
        {
            SetData(new AnimatorComponent
            {
                Value = _animator
            });
        }

        SetData(new Rigidbody2DComponent
        {
            Value = GetComponent<Rigidbody2D>()
        });

        SetData(new TransformComponent()
        {
            Value = GetComponent<Transform>()
        });
    }
}