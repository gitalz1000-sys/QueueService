using MediatR;
using QueueService.Features.Appointments.Commands;
using QueueService.Models;
using QueueService.Repositories;

namespace QueueService.Features.Appointments.Handlers;

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, Appointment>
{
    private readonly IAppointmentRepository _repository;

    public CreateAppointmentHandler(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Appointment> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = new Appointment
        {
            CustomerName = request.CustomerName,
            PhoneNumber = request.PhoneNumber,
            AppointmentDate = request.AppointmentDate,
            ServiceType = request.ServiceType,
            Notes = request.Notes,
            Status = "Scheduled"
        };

        return await _repository.CreateAsync(appointment);
    }
}
