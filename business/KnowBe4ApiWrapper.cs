using System.Net.Http.Headers;
using System.Text.Json;

/// <summary>
/// .net Wrapper for KnowBe4 api calls
/// </summary>
/// <remarks>
/// See https://developer.knowbe4.com/rest/reporting for API documentation
/// </remarks>
/// <example>
/// <code>
/// var httpClient = new HttpClient();
/// var apiWrapper = new KnowBe4ApiWrapper(httpClient);
///
public class KnowBe4ApiWrapper : IKnowBe4ApiWrapper
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private string endpoint;

    /// <summary>
    /// Initializes a new instance of the <see cref="KnowBe4ApiWrapper"/> class.
    /// </summary>
    public KnowBe4ApiWrapper(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// Get the account info for the current account
    /// </summary>
    /// <returns></returns>
    public async Task<AccountInfo> GetAccountInfo()
    {
        var endpoint = "account";
        var accountInfo = await ExecuteApiCall<AccountInfo>(endpoint, "GetAccountInfo error");
        return accountInfo;
    }

    /// <summary>
    /// Get all users for the current account
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var endpoint = "users";
        return await ExecuteApiCall<IEnumerable<User>>(endpoint, "GetAllUsers error");
    }

    /// <summary>
    /// Get a user by ID
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<User> GetUserById(int userId)
    {
        if (userId <= 0)
        {
            throw new ArgumentException("Invalid user ID", nameof(userId));
        }
        var endpoint = $"users/{userId}";
        return await ExecuteApiCall<User>(endpoint, "GetUserById error");
    }

    /// <summary>
    /// Get all training campaigns for the current account
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Campaign>> GetAllTrainingCampaigns()
    {
        var endpoint = "training/campaigns";
        return await ExecuteApiCall<IEnumerable<Campaign>>(endpoint, "GetAllTrainingCampaigns error");
    }

    /// <summary>
    /// Get all training campaigns for the current account
    /// </summary>
    /// <param name="groupId"></param>  
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetGroupMembers(int groupId)
    {
        if (groupId <= 0)
        {
            throw new ArgumentException("Invalid group ID", nameof(groupId));
        }
        var endpoint = $"groups/{groupId}/members";
        return await ExecuteApiCall<IEnumerable<User>>(endpoint, "GetGroupMembers error");
    }

    /// <summary>
    /// Get all training campaigns for the current account
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<RiskScoreHistory>> GetUserRiskScoreHistory(int userId, bool includeFullHistory = false)
    {
        if (userId <= 0)
        {
            throw new ArgumentException("Invalid user ID", nameof(userId));
        }        
        string endpoint = $"users/{userId}/risk_score_history";

        if (includeFullHistory)
        {
            endpoint += "?full=true";
        }
        return await ExecuteApiCall<IEnumerable<RiskScoreHistory>>(endpoint, "GetUserRiskScoreHistory error");
    }

    /// <summary>
    /// Get all training campaigns for the current account
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Group>> GetGroupsByStatus(GroupStatus status)
    {
        var statusParam = status.GetDescription();
        var endpoint = $"groups?status={statusParam}";
        return await ExecuteApiCall<IEnumerable<Group>>(endpoint, "GetGroupsByStatus error");
    }

    /// <summary>
    /// Get all training campaigns for the current account
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public async Task<Group> GetGroupById(int groupId)
    {
        if (groupId <= 0)
        {
            throw new ArgumentException("Invalid group ID", nameof(groupId));
        }
        string endpoint = $"groups/{groupId}";
        return await ExecuteApiCall<Group>(endpoint, "GetGroupById error");
    }

    /// <summary>
    /// Get all training campaigns for the current account
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<GroupRiskScoreHistory>> GetGroupRiskScoreHistory(int groupId, bool includeFullHistory = false)
    {
        if (groupId <= 0)
        {
            throw new ArgumentException("Invalid group ID", nameof(groupId));
        }
        string endpoint = $"groups/{groupId}/risk_score_history";
        if (includeFullHistory)
        {
            endpoint += "?full=true";
        }
        return await ExecuteApiCall<IEnumerable<GroupRiskScoreHistory>>(endpoint, "GetGroupById error");
    }

    /// <summary>
    /// Get all training campaigns for the current account
    /// </summary>
    /// <param name="campaignId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<PhishingCampaign>> GetPhishingCampaignById(int campaignId)
    {
        if (campaignId <= 0)
        {
            throw new ArgumentException("Invalid campaign ID", nameof(campaignId));
        }
        string endpoint = $"phishing/campaigns/{campaignId}";
        return await ExecuteApiCall<IEnumerable<PhishingCampaign>>(endpoint, "GetPhishingCampaignById error");
    }

    /// <summary>
    /// Get all training campaigns for the current account
    /// </summary>
    public async Task<IEnumerable<SecurityTest>> GetSecurityTests()
    {
        string endpoint = $"phishing/security_tests";
        return await ExecuteApiCall<IEnumerable<SecurityTest>>(endpoint, "GetSecurityTests error");
    }


    /// <summary>
    /// Find training users matching the search specification
    /// </summary>
    /// <param name="spec">The search specification</param>
    /// <returns></returns>
    public async Task<IEnumerable<User>> FindTrainingUsersEnrollments(SearchEnrollmentSpecification spec)
    {
        if (spec == null)
        {
            throw new ArgumentNullException(nameof(spec));
        }
        // construct the query string
        var endpoint = "training/enrollments";
        endpoint = ConstructQueryStringForFindTrainingUsersEnrollments(spec, endpoint);

        // Obtenez toutes les inscriptions correspondant aux paramètres de requête
        var enrollments = await ExecuteApiCall<IEnumerable<Enrollment>>(endpoint, "FindTrainingUsersEnrollments error");

        if (enrollments.Any())
        {
            // Filtrer selon la spécification fournie
            var users = GetUsersByStatus(enrollments, spec.CompletionStatus);
            return (IEnumerable<User>)users;
        }
        else
        {
            // no enrollments found
            IEnumerable<User> users = null;
            return users;
        }
    }
    
    /// <summary>
    /// Get all users by status
    private IEnumerable<User> GetUsersByStatus(IEnumerable<Enrollment> enrollments, CompletionStatus status)
    {
        IEnumerable<User> users;

        if (status == CompletionStatus.NotDefined)
        {
            users = enrollments.Select(enrollment => enrollment.user);
        }
        else
        {
            var statusString = status.GetDescription();

            users = enrollments
                .Where(enrollment => enrollment.status.Equals(statusString, StringComparison.OrdinalIgnoreCase))
                .Select(enrollment => enrollment.user);
        }

        return users.Distinct();
    }

    /// <summary>
    /// Construct the query string for the FindTrainingUsersEnrollments method
    /// </summary>
    private string ConstructQueryStringForFindTrainingUsersEnrollments(SearchEnrollmentSpecification spec, string endpoint)
    {
        var queryParams = new List<string>();

        if (spec.StorePurchaseId != 0)
        {
            queryParams.Add($"store_purchase_id={spec.StorePurchaseId}");
        }
        if (spec.CampaignId != 0)
        {
            queryParams.Add($"campaign_id={spec.CampaignId}");
        }
        if (spec.UserId != 0)
        {
            queryParams.Add($"user_id={spec.UserId}");
        }

        if (queryParams.Any())
        {
            endpoint += "?" + string.Join("&", queryParams);
        }

        return endpoint;
    }

    /// <summary>
    /// Find all training enrollments matching the given specification
    /// </summary>
    public async Task<IEnumerable<Enrollment>> FindTrainingEnrollments(SearchEnrollmentSpecification spec)
    {
        if (spec == null)
        {
            throw new ArgumentNullException(nameof(spec));
        }
        var endpoint = "trainings/enrollments";
        endpoint = ConstructQueryStringForFindTrainingEnrollments(spec, endpoint);

        // Obtenez toutes les inscriptions correspondant aux paramètres de requête
        return await ExecuteApiCall<IEnumerable<Enrollment>>(endpoint, "FindTrainingEnrollments error");
    }

    /// <summary>
    /// Construct the query string for the FindTrainingEnrollments method
    /// </summary>
    private string ConstructQueryStringForFindTrainingEnrollments(SearchEnrollmentSpecification spec, string endpoint)
    {

        // Ajoutez les paramètres de requête si nécessaire
        var queryParams = new List<string>();

        if (spec.StorePurchaseId != 0)
        {
            queryParams.Add($"store_purchase_id={spec.StorePurchaseId}");
        }
        if (spec.CampaignId != 0)
        {
            queryParams.Add($"campaign_id={spec.CampaignId}");
        }
        if (queryParams.Any())
        {
            endpoint += "?" + string.Join("&", queryParams);
        }
        return endpoint;
    }

    /// <summary>
    /// Find all training users matching the given specification
    /// </summary>
    private async Task<T> ExecuteApiCall<T>(string endpoint, string errorMessage)
    {
        try
        {
            var apiUrl = $"{_configuration.GetSection("knowbe4")["api_url"]}/{endpoint}";
            var bearerToken = $"{_configuration.GetSection("knowbe4")["api_token"]}";

            var handler = new HttpClientHandler
            {
                //ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                //AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                //AllowAutoRedirect = true,
                //MaxConnectionsPerServer = 10,
                ///Proxy = new WebProxy("http://proxyserver:8888"),
                //UseProxy = true,
                //UseCookies = true,
                //Credentials = new NetworkCredential("username", "password"),
                //ClientCertificateOptions = ClientCertificateOption.Automatic
            };

            using (HttpClient client = new HttpClient(handler))
            {
                // Ajoutez l'en-tête d'autorisation
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

                // Ajoutez l'en-tête "Accept" pour spécifier le type de contenu attendu
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Effectuez l'appel à l'API
                var response = await client.GetAsync(apiUrl);
                
                // Vérifiez la réponse et traitez-la en conséquence
                if (response.IsSuccessStatusCode)
                {
                    // La requête a réussi, traitez la réponse ici
                    var jsonResponse = await response.Content.ReadAsStringAsync();

                    T deserializeResponse = JsonSerializer.Deserialize<T>(jsonResponse);
                    if (deserializeResponse == null)
                    {
                        Console.WriteLine("Error deserializing response.");
                        return default(T);
                    }
                    return deserializeResponse;
                }
                else
                {
                    // La requête a échoué, traitez l'erreur ici
                    Console.WriteLine("Erreur lors de l'appel à l'API.");
                }
            }
            return default(T) ;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        return default(T);
    }
}