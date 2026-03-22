using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces.Repositories;
using MediatR;

namespace El_Master.Application.Features.Subscriptions.Queries.GetMySubscriptionsQuery
{

    public class GetMySubscriptionsHandler
        : IRequestHandler<GetMySubscriptionsQuery, Result<List<MySubscriptionDto>>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public GetMySubscriptionsHandler(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<Result<List<MySubscriptionDto>>> Handle(
            GetMySubscriptionsQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var data = await _subscriptionRepository
                    .GetStudentSubscriptionsAsync(request.StudentId);

                return Result<List<MySubscriptionDto>>.Success(data, "Subscriptions retrieved successfully");
            }
            catch
            {
                return Result<List<MySubscriptionDto>>.Failure("Failed to retrieve subscriptions");
            }
        }
    }
}
