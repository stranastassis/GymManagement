using GymManagement.Application.Admins.Commands.CreateAdmin;
using GymManagement.Application.Gyms.Queries.GetAdmin;
using GymManagement.Contracts.Admins;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
    [Route("admin")]

    public class AdminController : ApiController
    {
        private readonly ISender _mediator;

        public AdminController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdmin(CreateAdminRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    detail: "Name cannot be null or empty");
            }

            var command = new CreateAdminCommand(request.Name);

            var adminResult = await _mediator.Send(command);

            return adminResult.Match(
                subscription => CreatedAtAction(
                    nameof(GetAdmin),
                    new { adminId = adminResult.Value.Id },
                    new CreateAdminResponse(
                        adminResult.Value.Id,
                        adminResult.Value.Name)),
                Problem);
        }

        [HttpGet("{adminId:guid}")]
        public async Task<IActionResult> GetAdmin(Guid adminId)
        {
            var query = new GetAdminQuery(adminId);

            var adminResult = await _mediator.Send(query);

            return adminResult.Match(
                subscription => CreatedAtAction(
                    nameof(GetAdmin),
                    new { adminId = adminResult.Value.Id },
                    new CreateAdminResponse(
                        adminResult.Value.Id,
                        adminResult.Value.Name)),
                Problem);
        }

    }
}
