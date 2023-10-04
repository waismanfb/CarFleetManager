using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RentalEventEntity
    {
        public RentalEventEntity()
        {

        }

        public int RentalEventId { get; set; }
        public string EventType { get; set; }
        public DateTime EventDate { get; set; }
    }
}
