using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueueService.Features.Appointments.Commands;
using QueueService.Features.Appointments.Queries;
using QueueService.Models;

namespace QueueService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // החזרת כל התורים הקיימים
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _mediator.Send(new GetAllAppointmentsQuery());

        // במקרה ואין תוצאות — מחזירים רשימה ריקה (בחירה מודעת)
        return Ok(items ?? new List<Appointment>());
    }

    // החזרת תור לפי מזהה
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("חסר מזהה תור.");

        var item = await _mediator.Send(new GetAppointmentByIdQuery { Id = id });

        if (item == null)
            return NotFound($"לא נמצא תור עם מזהה {id}.");

        return Ok(item);
    }

    // יצירת תור חדש
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // עדכון תור קיים
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateAppointmentCommand command)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("מזהה לא תקין.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        command.Id = id;
        var updated = await _mediator.Send(command);

        if (!updated)
            return NotFound($"תור עם מזהה {id} לא נמצא.");

        return NoContent();
    }

    // מחיקת תור
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return BadRequest("מזהה לא תקין.");

        var deleted = await _mediator.Send(new DeleteAppointmentCommand { Id = id });

        if (!deleted)
            return NotFound($"אין תור עם מזהה {id}.");

        return NoContent();
    }
}
