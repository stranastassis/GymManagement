using ErrorOr;
using MediatR;

namespace GymManagement.Application.Admins.Commands.CreateAdmin
{
    public record CreateAdminCommand(
        string Name): IRequest<ErrorOr<Domain.Admins.Admin>>;
}
