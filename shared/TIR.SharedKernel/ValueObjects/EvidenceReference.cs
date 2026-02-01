using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TIR.SharedKernel.ValueObjects
{
    public sealed class EvidenceReference
    {
        public Guid DocumentId { get; private set; }
        public int PageNumber { get; private set; }
        public float OcrConfidence { get; private set; }
        public EvidenceReference(Guid documentId, int pageNumber, float confidence)
        {
            DocumentId = documentId;
            PageNumber = pageNumber;
            OcrConfidence = confidence;
        }
    }
}
