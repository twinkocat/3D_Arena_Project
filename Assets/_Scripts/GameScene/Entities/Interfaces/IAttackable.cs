
public interface IAttackable
{
    float AttackPower { get; }
    float AttackDelay { get; }
    float AttackRange { get; }

    void Attack();
}
