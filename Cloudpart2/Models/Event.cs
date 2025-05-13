using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CloudPart1.Models
{
  

    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(100, ErrorMessage = "Event name cannot exceed 100 characters.")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Event date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Description { get; set; } // Optional

        [Display(Name = "Venue")]
        [ForeignKey("Venue")]
        public int VenueId { get; set; } // Foreign key

        // Navigation property (an event belongs to one venue)
        public Venue? Venue { get; set; }

        // Navigation property (an event can have one booking)
        public Booking? Booking { get; set; }
    }
}
