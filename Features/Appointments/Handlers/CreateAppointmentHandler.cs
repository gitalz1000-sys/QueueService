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
            NationalId = request.NationalId,
            CustomerName = request.CustomerName,
            PhoneNumber = request.PhoneNumber,
            AppointmentDate = request.AppointmentDate,

            Ministry = request.Ministry,
            ServiceCategory = request.ServiceCategory,
            SubService = request.SubService,

            Notes = request.Notes,
            Status = "Scheduled",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        return await _repository.CreateAsync(appointment);
    }
}
