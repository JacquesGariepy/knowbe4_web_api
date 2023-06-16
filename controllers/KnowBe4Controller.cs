using Microsoft.AspNetCore.Mvc;
using Serilog;


[ApiController]
[Route("[controller]")]
public class KnowBe4Controller : ControllerBase
{
    private readonly IKnowBe4ApiWrapper _apiWrapper;

    public KnowBe4Controller(IKnowBe4ApiWrapper apiWrapper)
    {
        _apiWrapper = apiWrapper;
    }

    [HttpGet("account-info")]
    public async Task<IActionResult> GetAccountInfo()
    {
        try
        {
            var accountInfo = await _apiWrapper.GetAccountInfo();
            return Ok(accountInfo);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error getting account info");
            return StatusCode(500);
        }
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var allUsers = await _apiWrapper.GetAllUsers();
            return Ok(allUsers);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Error getting all users");
            return StatusCode(500);
        }
    }

    [HttpGet("users/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        try
        {
            var userById = await _apiWrapper.GetUserById(id);
            if (userById == null)
            {
                return NotFound();
            }
            return Ok(userById);
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Error getting user by ID: {id}");
            return StatusCode(500);
        }
    }

    [HttpGet("groups/{id}/members")]
    public async Task<IActionResult> GetGroupMembers(int id)
    {
        try
        {
            var groupMembers = await _apiWrapper.GetGroupMembers(id);
            if (groupMembers == null)
            {
                return NotFound();
            }
            return Ok(groupMembers);
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Error getting group members for group ID: {id}");
            return StatusCode(500);
        }
    }

    [HttpGet("training-campaigns")]
    public async Task<IActionResult> GetAllTrainingCampaigns()
    {
        try
        {
            var allTrainingCampaigns = await _apiWrapper.GetAllTrainingCampaigns();
            if (allTrainingCampaigns != null)
            {
                return Ok(allTrainingCampaigns);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("users/{id}/risk-score-history")]
    public async Task<IActionResult> GetUserRiskScoreHistory(int id)
    {
        try
        {
            var userRiskScoreHistory = await _apiWrapper.GetUserRiskScoreHistory(id);
            if (userRiskScoreHistory != null)
            {
                return Ok(userRiskScoreHistory);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("groups")]
    public async Task<IActionResult> GetGroupsByStatus([FromQuery] GroupStatus status)
    {
        try
        {
            var groupsByStatus = await _apiWrapper.GetGroupsByStatus(status);
            if (groupsByStatus != null)
            {
                return Ok(groupsByStatus);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("groups/{id}")]
    public async Task<IActionResult> GetGroupById(int id)
    {
        try
        {
            var groupById = await _apiWrapper.GetGroupById(id);
            if (groupById != null)
            {
                return Ok(groupById);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("groups/{id}/risk-score-history")]
    public async Task<IActionResult> GetGroupRiskScoreHistory(int id)
    {
        try
        {
            var groupRiskScoreHistory = await _apiWrapper.GetGroupRiskScoreHistory(id);
            if (groupRiskScoreHistory != null)
            {
                return Ok(groupRiskScoreHistory);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("phishing-campaigns/{id}")]
    public async Task<IActionResult> GetPhishingCampaignById(int id)
    {
        try
        {
            var phishingCampaignById = await _apiWrapper.GetPhishingCampaignById(id);
            if (phishingCampaignById != null)
            {
                return Ok(phishingCampaignById);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("security-tests")]
    public async Task<IActionResult> GetSecurityTests()
    {
        try
        {
            var securityTests = await _apiWrapper.GetSecurityTests();
            if (securityTests != null)
            {
                return Ok(securityTests);
            }
            else
            {
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
