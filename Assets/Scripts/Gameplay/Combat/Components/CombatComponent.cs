using System;

[Serializable]
public struct CombatComponent
{
    public int Damage;
    public float AttackDistance;
    public float MinTimeBetweenAttack;
    public float MaxTimeBetweenAttack;
    public float AnimationTime;
    public DamageType DamageType;
}