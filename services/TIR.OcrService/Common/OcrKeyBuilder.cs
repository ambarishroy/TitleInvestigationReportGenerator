namespace TIR.OcrService.Common
{
    public static class OcrKeyBuilder
    {
        public static string TextKey(Guid tenantId, Guid projectId, Guid documentId)
        {
            return $"tenant/{tenantId}/project/{projectId}/documents/{documentId}/ocr/ocr.txt";
        }
    }
}
