using MediatR;
using QueueService.Models;

namespace QueueService.Features.Appointments.Commands;

public class CreateAppointmentCommand : IRequest<Appointment>
{
    public string CustomerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string ServiceType { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
