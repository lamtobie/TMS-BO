namespace Databases
{
    public abstract class Entity<TKey> : IHasKey<TKey>, ITrackable
    {
        public TKey Key { get; set; }

        public long CreatedAt { get; set; }

        public long? UpdatedAt { get; set; }

        public long? CreatedBy { get; set; } = 0;

        public long? UpdatedBy { get; set; }
    }
}
