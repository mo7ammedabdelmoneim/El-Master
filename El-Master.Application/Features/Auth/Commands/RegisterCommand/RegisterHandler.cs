using El_Master.Application.Common.Results;
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

        public RegisterHandler(IGradeRepository gradeRepository, IAuthService authService, IStudentRepository studentRepository)
        {
            this.gradeRepository = gradeRepository;
            _authService = authService;
            this.studentRepository = studentRepository;
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

            // create student 
            var student = new Student
            {
                ApplicationUserId = result.UserId,
                GradeId = grade.Id,
            };
            await studentRepository.Command.AddAsync(student);
            await studentRepository.Command.SaveChangesAsync();

            return Result<AuthModel>.Success(result,result.Message);
        }


    }
}
