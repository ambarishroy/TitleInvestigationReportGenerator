using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIR.SharedKernel.Enums;

namespace TIR.SharedKernel.Events
{
    public sealed record DocumentUploadedEvent
   (
        Guid TenantId,
        Guid ProjectId,
        Guid DocumentId,
        DocumentType DocumentType,
        string FileName,
        int PageCount,
        string Language,
        string S3Bucket,
        string S3Key,
        DateTime UploadedAtUtc,
        string CorrelationId
   );
}
