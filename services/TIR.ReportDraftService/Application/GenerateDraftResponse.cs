namespace TIR.ReportDraftService.Application
{
    public sealed class GenerateDraftResponse
    {
        public Guid ProjectId { get; init; }
        public string FileName { get; init; } = default!;
        public string Status { get; init; } = default!;
    }
}
