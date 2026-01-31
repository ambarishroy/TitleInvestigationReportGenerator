using Microsoft.VisualBasic;
using TIR.OcrService.Domain;

namespace TIR.OcrService.Infrastructure
{
    public sealed class OCREngine
    {
        public OCRDocument RunOcr(Guid DocumentId, string filepath, OCRLanguage language)
        {
            //TODO: aws textract or tesseract  
            // Dummy implementation
            var doc = new OCRDocument(DocumentId, language);

            // Suppose document has 5 pages (replace with real PDF logic)
            for (int i = 1; i <= 5; i++)
            {
                // Here you would call real OCR engine per page
                float confidence = 0.9f; // dummy confidence
                string extractedText = $"Dummy text for page {i}";
                doc.AddPageResult(new OCRPageResult(i, confidence, extractedText));
            }

            return doc;
        }
    }
}
