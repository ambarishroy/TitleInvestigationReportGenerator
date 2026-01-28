namespace TIR.ProjectService.Application
{
    public sealed class CreateProjectResponse
    {
        public Guid ProjectId { get; init; }
        public string Status { get; init; } = default!;
    }
}
