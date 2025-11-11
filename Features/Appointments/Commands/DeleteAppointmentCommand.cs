using MediatR;

namespace QueueService.Features.Appointments.Commands;

public class DeleteAppointmentCommand : IRequest<bool>
{
    public string Id { get; set; } = string.Empty;
}
