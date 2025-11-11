using MediatR;
using QueueService.Models;

namespace QueueService.Features.Appointments.Queries;

public class GetAllAppointmentsQuery : IRequest<List<Appointment>>
{
}
