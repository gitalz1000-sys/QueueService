using MediatR;
using QueueService.Features.Appointments.Commands;
using QueueService.Repositories;

namespace QueueService.Features.Appointments.Handlers;

public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, bool>
{
    private readonly IAppointmentRepository _repository;

    public DeleteAppointmentHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
    {
        return await _repository.DeleteAsync(request.Id);
    }
}
