using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace QueueService.Models;

public class Appointment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("nationalId")]
    public string NationalId { get; set; } = string.Empty;

    [BsonElement("customerName")]
    public string CustomerName { get; set; } = string.Empty;

    [BsonElement("phoneNumber")]
    public string PhoneNumber { get; set; } = string.Empty;

    [BsonElement("appointmentDate")]
    public DateTime AppointmentDate { get; set; }

    [BsonElement("ministry")]
    public string Ministry { get; set; } = string.Empty;

    [BsonElement("serviceCategory")]
    public string ServiceCategory { get; set; } = string.Empty;

    [BsonElement("subService")]
    public string SubService { get; set; } = string.Empty;

    [BsonElement("status")]
    public string Status { get; set; } = "Scheduled";

    [BsonElement("notes")]
    public string? Notes { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
