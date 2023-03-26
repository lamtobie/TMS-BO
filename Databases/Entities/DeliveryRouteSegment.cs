using Databases;

namespace Databases.Entities
{
    public class DeliveryRouteSegment : AggregateRoot<int>
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public short? Order { get; set; }
        public float? Length { get; set; }
        public int DeliveryRouteId { get; set; }
        public string? StartStationId { get; set; }
        public string? EndStationId { get; set; }
        public string? DriverCode { get; set; }
        public int? ExpectedStartTime { get; set; }
        public int? ExpectedArrivalTime { get; set; }
        public int? ExpectedTimeConsumed { get; set; }
        public int? ActualStartTime { get; set; }
        public int? ActualArrivalTime { get; set; }
        public int? ActualTimeConsumed { get; set; }

        public virtual DeliveryRoute DeliveryRoute { get; set; }
        public virtual Station StartStation { get; set; }
        public virtual Station EndStation { get; set; }
        public virtual Employee Driver { get; set; }
    }
}