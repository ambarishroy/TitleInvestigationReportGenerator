using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIR.SharedKernel.Events
{
    public sealed record AuditEvent
     (
         Guid TenantId,
         string Actor,
         string Action,
         string EntityType,
         string EntityId,
         DateTime TimestampUtc,
         string CorrelationId
     );
}
