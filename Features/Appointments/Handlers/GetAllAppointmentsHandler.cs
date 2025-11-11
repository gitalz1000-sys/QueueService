using MediatR;
using QueueService.Features.Appointments.Queries;
using QueueService.Models;
using QueueService.Repositories;

namespace QueueService.Features.Appointments.Handlers;

public class GetAllAppointmentsHandler : IRequestHandler<GetAllAppointmentsQuery, List<Appointment>>
{
    private readonly IAppointmentRepository _repository;

    public GetAllAppointmentsHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Appointment>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}
