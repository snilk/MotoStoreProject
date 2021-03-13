using System.ComponentModel;

namespace BookStore.Domain.Models
{
    public enum Department
    {
        [Description(".NET")]
        DotNet,
        [Description("Quality Assurance")]
        QA,
        [Description("Business Analysis")]
        BusinessAnalysis,
        [Description("Front End")]
        FrontEnd,
        [Description("Java")]
        Java,
        [Description("Human Resources")]
        HumanResources,
        [Description("Mobile")]
        Mobile,
        [Description("Project Management")]
        ProjectManagement,
        [Description("Sales")]
        Sales,
        [Description("DevOps")]
        DevOps,
        [Description("QA Automation")]
        QAAutomation,
    }
}