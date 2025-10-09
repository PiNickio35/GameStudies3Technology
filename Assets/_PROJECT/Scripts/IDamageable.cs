namespace _PROJECT.Scripts
{
    public interface IDamageable
    {
        int Health { get; set; }
        void Damage();
    }
}
