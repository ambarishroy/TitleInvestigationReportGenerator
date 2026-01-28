using System.Text.Json.Serialization;
using TIR.ProjectService.Domain;

namespace TIR.ProjectService.Application
{
    public sealed class CreateProjectRequest
    {
        public string ProjectName { get; init; } = default!;
        public LoanType LoanType { get; init; }
    }
}
