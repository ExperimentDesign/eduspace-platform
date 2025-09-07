using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Application.Internal.OutboundServices;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Aggregates;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Model.Commands;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Repositories;
using FULLSTACKFURY.EduSpace.API.ReservationScheduling.Domain.Services;
using FULLSTACKFURY.EduSpace.API.Shared.Domain.Repositories;

namespace FULLSTACKFURY.EduSpace.API.ReservationScheduling.Application.Internal.CommandServices;


public class MeetingCommandService (IMeetingRepository meetingRepository
    , IUnitOfWork unitOfWork, 
    IRExternalProfileService externalProfileService, 
    IExternalClassroomService externalClassroomService) : IMeetingCommandService
{
    public async Task<Meeting?> Handle(CreateMeetingCommand command)
    {
        // var teacherIds = command.Teachers.Select(t => t.Id).ToList();
        // if (!externalProfileService.ValidateTeachersExistence(teacherIds))
        //     throw new ArgumentException("One or more teachers do not exist.");
        
        if (!externalProfileService.ValidateAdminIdExistence(command.AdministratorId))
            throw new ArgumentException("Admin ID does not exist.");
        
        if (!externalClassroomService.ValidateClassroomId(command.ClassroomId))
            throw new ArgumentException("Classroom does not exist.");
        
        // Create a new Meeting object
        var meeting = new Meeting(command);

        // Add the new meeting to the repository
        await meetingRepository.AddAsync(meeting);

        // Complete the transaction
        await unitOfWork.CompleteAsync();

        return meeting;
    }

    public async Task Handle(DeleteMeetingCommand command)
    {
        var meeting = await meetingRepository.FindByIdAsync(command.MeetingId);
        if (meeting == null) throw new ArgumentException("Meeting not found.");

        meetingRepository.Remove(meeting);

        await unitOfWork.CompleteAsync();
    }

    public async Task<Meeting?> Handle(UpdateMeetingCommand command)
    {
        var meeting = await meetingRepository.FindByIdAsync(command.MeetingId);
        if (meeting == null)
            throw new ArgumentException("Meeting not found.");

        meeting.UpdateTitle(command.Title);
        meeting.UpdateDescription(command.Description);
        meeting.UpdateDate(command.Date);
        meeting.UpdateTime(command.Start, command.End);

        meeting.UpdateAdministrator(command.AdministratorId, externalProfileService.ValidateAdminIdExistence);
        meeting.UpdateClassroom(command.ClassroomId, externalClassroomService.ValidateClassroomId);

        meetingRepository.Update(meeting);
        await unitOfWork.CompleteAsync();

        return meeting;
    }

    public async Task Handle(AddTeacherToMeetingCommand command)
    {
        var meeting = await meetingRepository.FindByIdAsync(command.MeetingId);
        
        if (meeting == null)
            throw new ArgumentException("Meeting not found.");
        if (!externalProfileService.ValidateTeacherExistence(command.TeacherId))
            throw new ArgumentException("Teacher does not exist.");
        try
        {
            meeting.TeacherIdBuilder(command.TeacherId);
            meeting.AddTeacherToMeeting(command.MeetingId);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}