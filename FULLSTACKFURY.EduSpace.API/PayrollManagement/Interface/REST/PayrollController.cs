using System.Net.Mime;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Model.Queries;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Domain.Services;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Interface.REST.Resources;
using FULLSTACKFURY.EduSpace.API.PayrollManagement.Interface.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FULLSTACKFURY.EduSpace.API.PayrollManagement.Interface.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Payroll Endpoints")]
public class PayrollsController(IPayrollCommandService payrollCommandService, IPayrollQueryService payrollQueryService)
    : ControllerBase
{
    /// <summary>
    /// Creates a payroll entry.
    /// </summary>
    /// <param name="resource">
    /// The <see cref="CreatePayrollResource"/> containing details for the new payroll.
    /// </param>
    /// <returns>
    /// The <see cref="PayrollResource"/> if created successfully; otherwise, <see cref="BadRequestResult"/>.
    /// </returns>
    [HttpPost("teachers{teacherId:int}")]
    [SwaggerOperation(
        Summary = "Creates a payroll entry",
        Description = "Creates a payroll entry for a specific teacher",
        OperationId = "CreatePayroll"
    )]
    [SwaggerResponse(201, "The payroll entry was created", typeof(PayrollResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The payroll entry was not created")]
    public async Task<IActionResult> CreatePayroll([FromRoute] int teacherId ,[FromBody] CreatePayrollResource resource)
    {
        var createPayrollCommand = CreatePayrollCommandFromResourceAssembler.ToCommand(teacherId, resource);
        var payroll = await payrollCommandService.Handle(createPayrollCommand);
       
        if (payroll is null) return BadRequest();
        
        var payrollResource = PayrollResourceFromEntityAssembler.ToResourceFromEntity(payroll);
        return Ok(payrollResource);
    }

    /// <summary>
    /// Retrieves all payroll entries.
    /// </summary>
    /// <returns>
    /// The list of <see cref="PayrollResource"/> entries.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all payroll entries",
        Description = "Get all payroll entries",
        OperationId = "GetAllPayrolls"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The payroll entries were successfully retrieved", typeof(IEnumerable<PayrollResource>))]
    public async Task<IActionResult> GetAllPayrolls()
    {
        var getAllPayrollsQuery = new GetAllPayrollsQuery();
        var payrolls = await payrollQueryService.Handle(getAllPayrollsQuery);
        var resources = payrolls.Select(PayrollResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    /// <summary>
    /// Retrieves a payroll entry by ID.
    /// </summary>
    /// <param name="id">
    /// The ID of the payroll entry to retrieve.
    /// </param>
    /// <returns>
    /// The <see cref="PayrollResource"/> if found, otherwise returns <see cref="NotFoundResult"/>.
    /// </returns>
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get a payroll entry by ID",
        Description = "Get a payroll entry by its ID",
        OperationId = "GetPayrollById"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The payroll entry was successfully retrieved", typeof(PayrollResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The payroll entry was not found")]
    public async Task<IActionResult> GetPayrollById([FromRoute] int id)
    {
        var getPayrollByIdQuery = new GetPayrollByIdQuery(id);
        var payroll = await payrollQueryService.Handle(getPayrollByIdQuery);
        
        if (payroll is null) return NotFound();
        
        var payrollResource = PayrollResourceFromEntityAssembler.ToResourceFromEntity(payroll);
        return Ok(payrollResource);
    }
}
