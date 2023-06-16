public interface IKnowBe4ApiWrapper
{
    Task<AccountInfo> GetAccountInfo();
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int userId);
    Task<IEnumerable<Campaign>> GetAllTrainingCampaigns();
    Task<IEnumerable<User>> GetGroupMembers(int groupId);
    Task<IEnumerable<RiskScoreHistory>> GetUserRiskScoreHistory(int userId, bool includeFullHistory = false);
    Task<IEnumerable<Group>> GetGroupsByStatus(GroupStatus status);
    Task<Group> GetGroupById(int groupId);
    Task<IEnumerable<GroupRiskScoreHistory>> GetGroupRiskScoreHistory(int groupId, bool includeFullHistory = false);
    Task<IEnumerable<PhishingCampaign>> GetPhishingCampaignById(int campaignId);
    Task<IEnumerable<SecurityTest>> GetSecurityTests();
    Task<IEnumerable<User>> FindTrainingUsersEnrollments(SearchEnrollmentSpecification spec);
}