namespace TIR.DocumentService.Common
{
    public static class DocumentKeyBuilder
    {
        public static string Build(Guid tenantId, Guid projectId, Guid documentId, string extension)
        {
            return $"tenant/{tenantId}/project/{projectId}/documents/{documentId}/original{extension}";
        }
    }
}
