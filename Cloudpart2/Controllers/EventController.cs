using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CloudPart1.Data;
using CloudPart1.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CloudPart1.Controllers
{
    public class EventController : Controller
    {

        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            // Include Venue data to display venue names
            var events = await _context.Event.Include(e => e.Venue).ToListAsync();
            return View(events);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventId == id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            // Populate the dropdown list for Venue
            // Populate the dropdown list for Venue
            ViewBag.VenueList = new SelectList(_context.Venue, "VenueId", "VenueName");
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event created successfully";

                return RedirectToAction(nameof(Index));
            }

            // Repopulate venues if validation fails
            //ViewBag.Venues = _context.Venue.ToList();
            // return View(@event);
            //ViewBag.VenueId = new SelectList(_context.Venue, "VenueId", "VenueName");


            ViewData["Venues"] = _context.Venue.ToList();
            return View(@event);

        }
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event.FindAsync(Id);
            if (@event == null)
            {
                return NotFound();
            }

            ViewBag.Venues = _context.Venue.ToList();
            return View(@event);
            ViewBag.VenueId = new SelectList(_context.Venue, "VenueId", "VenueName");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("EventId,EventName,EventDate,Description,VenueId")] Event @event)
        {
            if (Id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Venues = _context.Venue.ToList();
            return View(@event);
        }
 public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventId == Id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var @event = await _context.Event.FindAsync(Id);
            if (@event != null) return NotFound();

            var isBooked = await _context.Booking.AnyAsync(b =>b.EventId == Id);
            if (isBooked)
            {
                TempData["ErrorMessage"] = "Cannot delete event because it has existing bookings ";
                return RedirectToAction(nameof(Event));
            }
            
                _context.Event.Remove(@event);
                await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Event deleted successfully";
            return RedirectToAction(nameof(Index));
        }


        private bool EventExists(int Id)
        {
            return _context.Event.Any(e => e.EventId == Id);
        }
    }
}
