
using GymManagement.Application.Subscriptions.Commands.CreateSubscription;
using GymManagement.Application.Subscriptions.Commands.DeleteSubscription;
using GymManagement.Application.Subscriptions.Queries.GetSubscription;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GymManagement.Api.Controllers
{
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

            var command = new CreateSubscriptionCommand(
                subscriptionType,
                request.AdminId);

            var createSubscriptionResult = await _mediator.Send(command);

            return createSubscriptionResult.Match(
                subscription => CreatedAtAction(
                    nameof(GetSubscription),
                    new { subscriptionId = subscription.Id },
                    new SubscriptionResponse(
                        subscription.Id,
                        ToDto(subscription.SubscriptionType))),
                Problem);
        }

        [HttpGet("{subscriptionId:guid}")]
        public async Task<IActionResult> GetSubscription(Guid subscriptionId)
        {
            var query = new GetSubscriptionQuery(subscriptionId);

            var getSubscriptionsResult = await _mediator.Send(query);

            return getSubscriptionsResult.Match(
                subscription => Ok(new SubscriptionResponse(
                    subscription.Id,
                    ToDto(subscription.SubscriptionType))),
                Problem);
        }

        [HttpDelete("{subscriptionId:guid}")]
        public async Task<IActionResult> DeleteSubscription(Guid subscriptionId)
        {
            var command = new DeleteSubscriptionCommand(subscriptionId);

            var createSubscriptionResult = await _mediator.Send(command);

            return createSubscriptionResult.Match(
                _ => NoContent(),
                Problem);
        }

        private static SubscriptionType ToDto(DomainSubscriptionType subscriptionType)
        {
            return subscriptionType.Name switch
            {
                nameof(DomainSubscriptionType.Free) => SubscriptionType.Free,
                nameof(DomainSubscriptionType.Starter) => SubscriptionType.Starter,
                nameof(DomainSubscriptionType.Pro) => SubscriptionType.Pro,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
