
    using System.ComponentModel.DataAnnotations;

    using System.ComponentModel.DataAnnotations.Schema;

namespace CloudPart1.Models
{ 

public class Booking
{
    [Key]
    public int BookingId { get; set; }

    [Required(ErrorMessage = "Booking date is required.")]
    [DataType(DataType.DateTime)]
    [Display(Name = "Booking Date")]
    public DateTime BookingDate { get; set; }

    [Display(Name = "Event")]
    [ForeignKey("Event")]
    public int EventId { get; set; } // Foreign key

    [Display(Name = "Venue")]
    [ForeignKey("Venue")]
    public int VenueId { get; set; } // Foreign key

    // Navigation properties
    public Event? Event { get; set; }
    public Venue? Venue { get; set; }
}
}
