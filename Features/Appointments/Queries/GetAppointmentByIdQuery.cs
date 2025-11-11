using MediatR;
using QueueService.Models;

namespace QueueService.Features.Appointments.Queries;

public class GetAppointmentByIdQuery : IRequest<Appointment?>
{
    public string Id { get; set; } = string.Empty;
}
