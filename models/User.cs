public class User
{
    public int id { get; set; }
    public string employee_number { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string job_title { get; set; }
    public string email { get; set; }
    public float phish_prone_percentage { get; set; }
    public string phone_number { get; set; }
    public string extension { get; set; }
    public string mobile_phone_number { get; set; }
    public string location { get; set; }
    public string division { get; set; }
    public string manager_name { get; set; }
    public string manager_email { get; set; }
    public bool provisioning_managed { get; set; }
    public string? provisioning_guid { get; set; }
    public int[] groups { get; set; }
    public float current_risk_score { get; set; }
    public string[] aliases { get; set; }
    public DateTime? joined_on { get; set; }
    public DateTime? last_sign_in { get; set; }
    public string status { get; set; }
    public string organization { get; set; }
    public string department { get; set; }
    public string language { get; set; }
    public string comment { get; set; }
    public DateTime? employee_start_date { get; set; }
    public DateTime? archived_at { get; set; }
    public string? custom_field_1 { get; set; }
    public string? custom_field_2 { get; set; }
    public string? custom_field_3 { get; set; }
    public string? custom_field_4 { get; set; }
    public DateTime? custom_date_1 { get; set; }
    public DateTime? custom_date_2 { get; set; }
}