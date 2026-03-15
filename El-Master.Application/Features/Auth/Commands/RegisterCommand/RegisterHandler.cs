using El_Master.Application.Common.Results;
using El_Master.Application.Interfaces;
using El_Master.Application.Interfaces.Repositories;
using El_Master.Application.Interfaces.Services;
using El_Master.Domain.Common;
using El_Master.Domain.Entities;
using MediatR;

namespace El_Master.Application.Features.Auth.Commands.RegisterCommand
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<AuthModel>>
    {
        private readonly IGradeRepository gradeRepository;
        private readonly IAuthService _authService;
        private readonly IStudentRepository studentRepository;
        private readonly IUnitOfWork unitOfWork;

        public RegisterHandler(IGradeRepository gradeRepository, IAuthService authService, IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            this.gradeRepository = gradeRepository;
            _authService = authService;
            this.studentRepository = studentRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<AuthModel>> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            await unitOfWork.BeginTransactionAsync();

            try
            {
                // check grade
                var grade = await gradeRepository.GetByNameAsync(request.RegisterDto.Grade);
                if (grade == null)
                    return Result<AuthModel>.Failure("Invalid Grade");


                var result = await _authService.RegisterAsync(request.RegisterDto);

                if (!result.IsAuthenticated)
                    return Result<AuthModel>.Failure(result.Message);

                // create student 
                var student = new Student
                {
                    ApplicationUserId = result.UserId,
                    GradeId = grade.Id,
                };
                await studentRepository.AddAsync(student);
                await studentRepository.SaveChangesAsync();

                return Result<AuthModel>.Success(result,result.Message);
            }
            catch
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }


    }
}
