using System;
using System.IO;
using System.Xml;
using TIR.ReportDraftService.Domain;

namespace TIR.ReportDraftService.Infrastructure;

public sealed class DocumentGenerator
{
    public string GenerateDraftTir(TirProjectSummary projectSummary)
    {
        // Example: generate simple DOCX text for now
        string fileName = $"TIR_{projectSummary.ProjectName}_{DateTime.UtcNow:yyyyMMddHHmm}.docx";
        string filePath = Path.Combine("GeneratedDrafts", fileName);

        Directory.CreateDirectory("GeneratedDrafts");
        //Replace StreamWriter with OpenXML / DocX library for real DOCX
        using var writer = new StreamWriter(filePath);
        writer.WriteLine($"TITLE INVESTIGATION REPORT - {projectSummary.ProjectName}");
        writer.WriteLine($"Bank: {projectSummary.BankTenantId}");
        writer.WriteLine($"Jurisdiction: {projectSummary.JurisdictionCode}");
        writer.WriteLine($"Loan Type: {projectSummary.LoanType}");
        writer.WriteLine();
        writer.WriteLine("Documents Reviewed:");
        foreach (var doc in projectSummary.Documents)
        {
            writer.WriteLine($"- {doc.FileName} ({doc.Type})");
        }

        writer.WriteLine();
        writer.WriteLine("Ownership Timeline:");
        foreach (var evt in projectSummary.Timeline.Events)
        {
            writer.WriteLine($"- {evt.EventDate?.ToShortDateString() ?? "UNKNOWN"}: {evt.FromOwner} → {evt.ToOwner} ({evt.InstrumentType})");
        }

        writer.WriteLine();
        writer.WriteLine("Risks / Gaps:");
        foreach (var risk in projectSummary.Risks)
        {
            writer.WriteLine($"- {risk.Description}");
        }

        writer.WriteLine();
        writer.WriteLine("Legal Facts:");
        foreach (var fact in projectSummary.Facts)
        {
            writer.WriteLine($"- {fact.Type}: {fact.Value}");
        }
        //TODO: Add PDF export

        //TODO: Apply SBI TIR template formatting
        return filePath;
    }
}
