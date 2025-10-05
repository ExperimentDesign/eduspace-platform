using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.EventsScheduling.Domain.Services;
using FULLSTACKFURY.EduSpace.API.EventsScheduling.Interface.REST.Resources;
using FULLSTACKFURY.EduSpace.API.EventsScheduling.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FULLSTACKFURY.EduSpace.API.EventsScheduling.Interface.REST;

[ApiController]
[Route("api/v1/")]
[Produces(MediaTypeNames.Application.Json)]
public class ReservationsController(IReservationCommandService reservationCommandService, IReservationQueryService reservationQueryService)
    : ControllerBase
{
    [HttpPost("teachers/{teacherId:int}/areas/{areaId:int}/reservations")]
    [SwaggerOperation(
        Summary = "Creates a reservation",
        Description = "Creates a reservation to a specific area",
        OperationId = "CreateReservation"
    )]
    [SwaggerResponse(201, "The category was created", typeof(ReservationResource))]
    public async Task<IActionResult> CreateReservation([FromRoute] int teacherId, [FromRoute] int areaId , [FromBody] CreateReservationResource resource)
    {
        var createReservationCommand = CreateReservationCommandFromResourceAssembler.ToCommandFromResource(areaId, teacherId, resource);
        var reservation = await reservationCommandService.Handle(createReservationCommand);
       
        if (reservation is null) return BadRequest();
        
        var reservationResource = ReservationResourceFromEntityAssembler.ToResourceFromEntity(reservation);
        return Ok(reservationResource);
    }

    [HttpGet("[controller]")]
    public async Task<IActionResult> GetAllReservations()
    {
        var getAllReservationsQuery = new GetAllReservationsQuery();
        var reservations = await reservationQueryService.Handle(getAllReservationsQuery);
        var resources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpGet("areas/{areaId:int}/[controller]")]
    public async Task<IActionResult> GetAllReservationsByAreaId([FromRoute] int areaId)
    {
        var getAllReservationsByAreaIdQuery = new GetAllReservationsByAreaIdQuery(areaId);
        var reservations = await reservationQueryService.Handle(getAllReservationsByAreaIdQuery);

        var resources = reservations.Select(ReservationResourceFromEntityAssembler.ToResourceFromEntity);

        return Ok(resources);
    }
    //
    // [HttpGet("areas/{areaId:int}")]
    // public async Task<IActionResult> GetAllReservationsByAreaIdMonthAndDay(int areaId, [FromQuery] int month,
    //     [FromQuery] int day)
    // {
    //     var getAllReservationsByAreaIdMonthAndDayQuery = new GetAllReservationsByAreaIdAn
    // }
    //
}