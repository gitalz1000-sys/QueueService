using MediatR;
using QueueService.Features.Appointments.Commands;
using QueueService.Repositories;

namespace QueueService.Features.Appointments.Handlers;

public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointmentCommand, bool>
{
    private readonly IAppointmentRepository _repository;

    public UpdateAppointmentHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(request.Id);
        if (existing == null)
            return false;

        existing.NationalId = request.NationalId;
        existing.CustomerName = request.CustomerName;
        existing.PhoneNumber = request.PhoneNumber;
        existing.AppointmentDate = request.AppointmentDate;

        existing.Ministry = request.Ministry;
        existing.ServiceCategory = request.ServiceCategory;
        existing.SubService = request.SubService;

        existing.Status = request.Status;
        existing.Notes = request.Notes;

        existing.UpdatedAt = DateTime.UtcNow;

        return await _repository.UpdateAsync(request.Id, existing);
    }
}
