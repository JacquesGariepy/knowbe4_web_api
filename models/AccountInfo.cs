public class AccountInfo
{
    public string name { get; set; }
    public string type { get; set; }
    public string[] domains { get; set; }
    public Admin[] admins { get; set; }
    public string subscription_level { get; set; }
    public string subscription_end_date { get; set; }
    public int number_of_seats { get; set; }
    public float current_risk_score { get; set; }
}
