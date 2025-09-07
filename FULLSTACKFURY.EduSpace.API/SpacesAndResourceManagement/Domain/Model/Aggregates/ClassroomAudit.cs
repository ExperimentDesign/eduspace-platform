using System.ComponentModel.DataAnnotations.Schema;
using DateTimeOffset = System.DateTimeOffset;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.Aggregates;

public partial class ClassroomAudit : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}