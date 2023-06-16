using static KnowBe4ApiWrapper;

public class SearchEnrollmentSpecification : ISpecification<Enrollment>
{
    public string SearchTerm { get; set; }
    public CompletionStatus CompletionStatus { get; set; }
    public int TrainingId { get; set; }
    public string? Range { get; set; }
    public int UserId { get; internal set; }
    public int CampaignId { get; internal set; }
    public int StorePurchaseId { get; internal set; }

    public SearchEnrollmentSpecification()
    {

    }
}