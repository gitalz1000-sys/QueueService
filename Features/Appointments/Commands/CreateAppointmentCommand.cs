using MediatR;
using QueueService.Models;

namespace QueueService.Features.Appointments.Commands;

public class CreateAppointmentCommand : IRequest<Appointment>
{
    public string NationalId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }

    public string Ministry { get; set; } = string.Empty;
    public string ServiceCategory { get; set; } = string.Empty;
    public string SubService { get; set; } = string.Empty;

    public string? Notes { get; set; }
}
