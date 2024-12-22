using ErrorOr;
using MediatR;
using GymManagement.Domain.Admins;

namespace GymManagement.Application.Gyms.Queries.GetAdmin
{
    public record GetAdminQuery(Guid AdminId) : IRequest<ErrorOr<Admin>>;
}
