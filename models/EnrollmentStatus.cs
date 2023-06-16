using System.ComponentModel;

public enum CompletionStatus
{
    [Description("Not Started")]
    NotStarted,
    [Description("In Progress")]
    InProgress,
    [Description("Completed")]
    Completed,
    [Description("Passed")]
    Passed,
    [Description("Past Due")]
    PastDue,
    NotDefined = 999
}