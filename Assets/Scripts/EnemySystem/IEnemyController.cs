namespace EnemySystem
{
    public interface IEnemyController
    {
        public bool IsHit { get; }
        public bool IsDead { get; }
        public bool IsChasing { get; }
    }
}