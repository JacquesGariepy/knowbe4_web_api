public class PhishingCampaign
{
    public int campaign_id { get; set; }
    public string name { get; set; }
    public List<PhishingGroup> groups { get; set; }
    public float last_phish_prone_percentage { get; set; }
    public DateTime last_run { get; set; }
    public string status { get; set; }
    public bool hidden { get; set; }
    public string send_duration { get; set; }
    public string track_duration { get; set; }
    public string frequency { get; set; }
    public List<int> difficulty_filter { get; set; }
    public DateTime create_date { get; set; }
    public int psts_count { get; set; }
    public List<PhishingPST> psts { get; set; }
}

