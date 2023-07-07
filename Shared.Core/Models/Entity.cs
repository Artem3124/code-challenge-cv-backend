namespace Shared.Core.Models
{
    public class Entity
    {
        public int Id { get; set; }

        public Guid UUID { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? RemovedAtUtc { get; set; }
    }
}
