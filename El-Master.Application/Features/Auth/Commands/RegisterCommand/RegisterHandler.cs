using El_Master.Application.Common.Results;
using El_Master.Application.DTOs;
using El_Master.Application.Features.Auth.DTOs;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Domain.Common;
using MediatR;

namespace El_Master.Application.Features.Auth.Commands.RegisterCommand
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<AuthModel>>
    {
        private readonly IGradeRepository gradeRepository;
        private readonly IAuthService _authService;

        public RegisterHandler(IGradeRepository gradeRepository, IAuthService authService)
        {
            this.gradeRepository = gradeRepository;
            _authService = authService;
        }

        public async Task<Result<AuthModel>> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            //check grade
            var grade = await gradeRepository.GetByNameAsync(request.RegisterDto.Grade);
            if (grade == null)
                return Result<AuthModel>.Failure("Invalid Grade");


            var result = await _authService.RegisterAsync(new RegisterDto
            {
                FirstName = request.RegisterDto.FirstName,
                LastName = request.RegisterDto.LastName,
                Grade = request.RegisterDto.Grade,
                PhoneNumber = request.RegisterDto.PhoneNumber,
                Email = request.RegisterDto.Email,
                Password = request.RegisterDto.Password
            });

            if (!result.IsAuthenticated)
                return Result<AuthModel>.Failure(result.Message);

            return Result<AuthModel>.Success(result,result.Message);
        }


    }
}
