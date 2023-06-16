public class Campaign
{
    public int campaign_id { get; set; }
    public string name { get; set; }
    public List<Group> groups { get; set; }
    public string status { get; set; }
    public List<Content> content { get; set; }
    public string duration_type { get; set; }
    public string start_date { get; set; }
    public string end_date { get; set; }
    public object relative_duration { get; set; }
    public bool auto_enroll { get; set; }
    public bool allow_multiple_enrollments { get; set; }
    public int completion_percentage { get; set; }
}