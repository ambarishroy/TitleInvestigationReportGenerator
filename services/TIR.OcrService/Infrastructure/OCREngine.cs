using Microsoft.VisualBasic;
using TIR.OcrService.Domain;
using Tesseract;

namespace TIR.OcrService.Infrastructure
{
    public sealed class OCREngine
    {
        private readonly string _tessDataPath;
        public OCREngine(IConfiguration config)
        {
            _tessDataPath = config["Tesseract:TessDataPath"]?? throw new InvalidOperationException("Tesseract tessdata path not configured.");
        }
        public OCRDocument RunOcr(Guid DocumentId, string filepath, OCRLanguage language)
        {
            
            var doc = new OCRDocument(DocumentId, language);
            var tessLang = MapLanguage(language);
            using var engine = new TesseractEngine(_tessDataPath,tessLang,EngineMode.Default);
            // NOTE:
            // This assumes the input is a single-page image or a PDF already rasterized.
            // Multi-page PDF rasterization is a later enhancement.
            using var pix = Pix.LoadFromFile(filepath);
            using var page = engine.Process(pix);
            var text = page.GetText();
            var confidence = page.GetMeanConfidence();

            doc.AddPageResult(
                new OCRPageResult(
                    pageNumber: 1,
                    confidence: confidence,
                    extractedText: text));

            return doc;
        }
        private static string MapLanguage(OCRLanguage lang) =>
           lang switch
           {
               OCRLanguage.EN => "eng",
               OCRLanguage.AS => "asm",
               _ => "eng"
           };
    }
}
