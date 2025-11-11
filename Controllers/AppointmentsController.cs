using MediatR;
using Microsoft.AspNetCore.Mvc;
using QueueService.Features.Appointments.Commands;
using QueueService.Features.Appointments.Queries;
using QueueService.Models;

namespace QueueService.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// קבלת כל התורים במערכת
    /// </summary>
    /// <returns>רשימת כל התורים</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<Appointment>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Appointment>>> GetAll()
    {
        var appointments = await _mediator.Send(new GetAllAppointmentsQuery());
        return Ok(appointments);
    }

    /// <summary>
    /// קבלת תור לפי מזהה
    /// </summary>
    /// <param name="id">מזהה התור</param>
    /// <returns>פרטי התור</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Appointment), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Appointment>> GetById(string id)
    {
        var appointment = await _mediator.Send(new GetAppointmentByIdQuery { Id = id });
        
        if (appointment == null)
        {
            return NotFound(new { message = "תור לא נמצא" });
        }
        
        return Ok(appointment);
    }

    /// <summary>
    /// קביעת תור חדש
    /// </summary>
    /// <param name="command">פרטי התור החדש</param>
    /// <returns>התור שנוצר</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Appointment), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Appointment>> Create([FromBody] CreateAppointmentCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var appointment = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, appointment);
    }

    /// <summary>
    /// עדכון תור קיים
    /// </summary>
    /// <param name="id">מזהה התור</param>
    /// <param name="command">פרטי העדכון</param>
    /// <returns>הצלחה או כישלון</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(string id, [FromBody] UpdateAppointmentCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        command.Id = id;
        var result = await _mediator.Send(command);
        
        if (!result)
        {
            return NotFound(new { message = "תור לא נמצא" });
        }
        
        return NoContent();
    }

    /// <summary>
    /// מחיקת תור
    /// </summary>
    /// <param name="id">מזהה התור למחיקה</param>
    /// <returns>הצלחה או כישלון</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteAppointmentCommand { Id = id });
        
        if (!result)
        {
            return NotFound(new { message = "תור לא נמצא" });
        }
        
        return NoContent();
    }
}
