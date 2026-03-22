namespace El_Master.Application.Features.UserPackages.Commands.UpdatePackageCommand
{
    public class UpdatePackageDto
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Order { get; set; }

        public bool IsActive { get; set; }
    }
}
