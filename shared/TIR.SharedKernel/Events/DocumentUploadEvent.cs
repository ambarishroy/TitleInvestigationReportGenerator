using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TIR.SharedKernel.Enums;

namespace TIR.SharedKernel.Events
{
    public sealed record DocumentUploadEvent
    (
        Guid TenantId,
        Guid ProjectId,
        Guid DocumentId,
        string FileName,
        DocumentType DocumentType,
        int PageCount,
        string Language,
        DateTime UploadedAtUtc,
        string CorrelationId
    );          
}
