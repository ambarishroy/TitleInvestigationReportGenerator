using TIR.SharedKernel.Enums;

namespace TIR.ProjectService.Domain
{
    public sealed class TIRProject
    {
        public Guid ProjectId { get; private set; }
        public string ProjectName { get; private set; }
        public string BankTenantId { get; private set; }
        public string JurisdictionCode { get; private set; }
        public ProjectStatus Status { get; private set; }
        public LoanType LoanType { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public string CreatedByUserId { get; private set; }

        private TIRProject() { }
        public TIRProject(string projectName, string bankTenantId, string jurisdictionCode, LoanType loanType, string createdByUserId)
        {
            ProjectId = Guid.NewGuid();
            ProjectName = projectName;
            BankTenantId = bankTenantId;
            JurisdictionCode = jurisdictionCode;
            LoanType = loanType;
            Status = ProjectStatus.Draft;
            CreatedAtUtc = DateTime.UtcNow;
            CreatedByUserId = createdByUserId;
        }
        public void MarkProcessing()
        {
            if (Status != ProjectStatus.Draft)
            {
                throw new InvalidOperationException("Project must be in Draft state.");
            }
            Status = ProjectStatus.Processing;
        }
        public void MarkActionRequired()
        {
            Status = ProjectStatus.ActionRequired;
        }
        public void MarkUnderReview()
        {
            if (Status != ProjectStatus.ActionRequired)
            {
                throw new InvalidOperationException("Processing must complete first.");
            }
            Status = ProjectStatus.UnderReview;
        }
        public void MarkReportGenerated()
        {
            if (Status != ProjectStatus.UnderReview)
                throw new InvalidOperationException("Draft must be reviewed first.");
            Status = ProjectStatus.ReportGenerated;
        }
    }
}
