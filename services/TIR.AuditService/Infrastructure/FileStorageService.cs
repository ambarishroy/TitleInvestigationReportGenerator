namespace TIR.AuditService.Infrastructure
{
    public sealed class FileStorageService
    {
        public string SaveFinalTir(string bankTenantId, Guid projectId, int version, string fileName, byte[] content)
        {
            string folderPath = Path.Combine("FinalTIRs", bankTenantId, projectId.ToString());
            Directory.CreateDirectory(folderPath);
            string path = Path.Combine(folderPath, $"v{version}_{fileName}");
            File.WriteAllBytes(path, content);
            return path;
        }
    }
}
