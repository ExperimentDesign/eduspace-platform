using System.ComponentModel.DataAnnotations;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.ValueObjects;
using FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.Commands.Classroom;

namespace FULLSTACKFURY.EduSpace.API.SpacesAndResourceManagement.Domain.Model.Aggregates;

/// <summary>
/// Classroom aggregate root entity
/// </summary>
/// <remarks>
/// This class is used to represent a classroom in the application.
/// </remarks>
public partial class Classroom
{
    [Key]
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public TeacherId TeacherId { get; private set; }
    
    public ICollection<Resource> Resources { get; private set; } = new List<Resource>();
    
    /// <summary>
    /// Default constructor for the classroom entity
    /// </summary>
    /// <param name="name">
    /// The name of the classroom
    /// </param>
    /// <param name="description">
    /// The description of the classroom
    /// </param>
    /// <param name="teacherId">
    /// The teacher id for the classroom
    /// </param>
    
    public Classroom() {}
    public Classroom(string name, string description, int teacherId)
    {
        Name = name;
        Description = description;
        TeacherId = new TeacherId(teacherId);
    }
    
    public Classroom(CreateClassroomCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        TeacherId = new TeacherId(command.TeacherId);
    }

    public Classroom(UpdateClassroomCommand command)
    {
        Name = command.Name;
        Description = command.Description;
        TeacherId = new TeacherId(command.TeacherId);
    }
    
    public void UpdateName(string name)
    {
        if (!string.IsNullOrEmpty(name))
            Name = name;
    }
    
    public void UpdateDescription(string description)
    {
        if (!string.IsNullOrEmpty(description))
            Description = description;
    }
    
    public void UpdateTeacherId(int? teacherId, Func<int, bool> verifyProfile)
    {
        if (teacherId.HasValue && verifyProfile(teacherId.Value))
            TeacherId = new TeacherId(teacherId.Value);
    }
}