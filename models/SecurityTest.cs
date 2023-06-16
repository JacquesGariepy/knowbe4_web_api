public class SecurityTest
{
    public int campaign_id { get; set; }
    public int pst_id { get; set; }
    public string status { get; set; }
    public string name { get; set; }
    public List<SecurityTestGroup> groups { get; set; }
    public float phish_prone_percentage { get; set; }
    public DateTime started_at { get; set; }
    public int duration { get; set; }
    public List<SecurityTestCategory> categories { get; set; }
    public SecurityTestTemplate template { get; set; }
    public SecurityTestLandingPage landing_page { get; set; }
    public int scheduled_count { get; set; }
    public int delivered_count { get; set; }
    public int opened_count { get; set; }
    public int clicked_count { get; set; }
    public int replied_count { get; set; }
    public int attachment_open_count { get; set; }
    public int macro_enabled_count { get; set; }
    public int data_entered_count { get; set; }
    public int qr_code_scanned_count { get; set; }
    public int reported_count { get; set; }
    public int bounced_count { get; set; }
}