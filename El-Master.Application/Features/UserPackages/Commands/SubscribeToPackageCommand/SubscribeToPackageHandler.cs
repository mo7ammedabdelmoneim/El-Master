using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Domain.Entities;
using MediatR;

namespace El_Master.Application.Features.UserPackages.Commands.SubscribeToPackageCommand
{

    public class SubscribeToPackageHandler
        : IRequestHandler<SubscribeToPackageCommand, Result<bool>>
    {
        private readonly IPackageRepository _packageRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SubscribeToPackageHandler(
            IPackageRepository packageRepository,
            ISubscriptionRepository subscriptionRepository,
            IStudentRepository studentRepository,
            IUnitOfWork unitOfWork)
        {
            _packageRepository = packageRepository;
            _subscriptionRepository = subscriptionRepository;
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<bool>> Handle(
            SubscribeToPackageCommand request,
            CancellationToken cancellationToken)
        {
            try
            {
                // Check Student
                var student = await _studentRepository.GetAsync(x=>x.Id == request.StudentId);
                if (student == null)
                    return Result<bool>.Failure("Invalid StudentId");

                // Check Package
                var package = await _packageRepository.GetAsync(x=>x.Id == request.PackageId);
                if (package == null)
                    return Result<bool>.Failure("Package not found");

                if (!package.IsActive)
                    return Result<bool>.Failure("Package is not active");

                // Check already subscribed
                var exists = await _subscriptionRepository
                    .ExistsAsync(request.StudentId, request.PackageId);

                if (exists)
                    return Result<bool>.Failure("Already subscribed to this package");

                // Create Subscription
                var subscription = new Subscription
                {
                    Id = Guid.NewGuid(),
                    StudentId = request.StudentId,
                    PackageId = request.PackageId,
                    StartDate = DateTime.UtcNow,
                    IsActive = true
                };

                await _subscriptionRepository.AddAsync(subscription);
                await _subscriptionRepository.SaveChangesAsync();

                return Result<bool>.Success(true, "Subscribed successfully");
            }
            catch
            {
                return Result<bool>.Failure("Failed to subscribe");
            }
        }
    }
}
