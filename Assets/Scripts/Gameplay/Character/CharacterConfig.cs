using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig",menuName = "Gameplay/New CharacterConfig")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private float _minDistance;
    [SerializeField] private float _attackRadius;
    [SerializeField] private float _minTimeBetweenAttack;
    [SerializeField] private float _maxTimeBetweenAttack;
    [SerializeField] private float _animationTime;
    [SerializeField] private DamageType _damageType;

    public float MoveSpeed => _moveSpeed;
    public int Damage => _damage;
    public float MinDistance => _minDistance;
    public float AttackRadius => _attackRadius;
    public float MinTimeBetweenAttack => _minTimeBetweenAttack;
    public float MaxTimeBetweenAttack => _maxTimeBetweenAttack;
    public float AnimationTime => _animationTime;
    public DamageType DamageType => _damageType;
}