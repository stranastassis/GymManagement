using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using GymManagement.Domain.Admins;
using MediatR;

namespace GymManagement.Application.Gyms.Queries.GetAdmin
{
    public class GetAdminQueryHandler : IRequestHandler<GetAdminQuery, ErrorOr<Admin>>
    {

        private readonly IAdminsRepository _adminsRepository;

        public GetAdminQueryHandler(
            IAdminsRepository adminsRepository)
        {
            _adminsRepository = adminsRepository;
        }

        public async Task<ErrorOr<Admin>> Handle(GetAdminQuery request, CancellationToken cancellationToken)
        {

            if (await _adminsRepository.GetByIdAsync(request.AdminId) is not Admin admin)
            {
                return Error.NotFound(description: "admin not found");
            }

            return admin;
        }
    }
}
