using MediatR;
using QueueService.Features.Appointments.Queries;
using QueueService.Models;
using QueueService.Repositories;

namespace QueueService.Features.Appointments.Handlers;

public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, Appointment?>
{
    private readonly IAppointmentRepository _repository;

    public GetAppointmentByIdHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Appointment?> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}
