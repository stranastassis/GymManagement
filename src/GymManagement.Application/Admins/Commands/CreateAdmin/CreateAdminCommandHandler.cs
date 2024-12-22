using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;
using AdminN = GymManagement.Domain.Admins;

namespace GymManagement.Application.Admins.Commands.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, ErrorOr<AdminN.Admin>>
    {
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IAdminsRepository _adminsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAdminCommandHandler(ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork, IAdminsRepository adminsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
            _adminsRepository = adminsRepository;
        }

        public async Task<ErrorOr<Domain.Admins.Admin>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            var id = new Guid();

            var admin = new AdminN.Admin(id, name: request.Name);
            await _adminsRepository.AddAsync(admin);
            await _unitOfWork.CommitChangesAsync();

            return admin;
        }
    }
}
