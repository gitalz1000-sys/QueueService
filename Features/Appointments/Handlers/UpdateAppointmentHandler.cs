using MediatR;
using QueueService.Features.Appointments.Commands;
using QueueService.Models;
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
        var existingAppointment = await _repository.GetByIdAsync(request.Id);
        
        if (existingAppointment == null)
        {
            return false;
        }

        existingAppointment.CustomerName = request.CustomerName;
        existingAppointment.PhoneNumber = request.PhoneNumber;
        existingAppointment.AppointmentDate = request.AppointmentDate;
        existingAppointment.ServiceType = request.ServiceType;
        existingAppointment.Status = request.Status;
        existingAppointment.Notes = request.Notes;

        return await _repository.UpdateAsync(request.Id, existingAppointment);
    }
}
