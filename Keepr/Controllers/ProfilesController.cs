using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly KeepsService _keepsService;
  private readonly VaultsService _vaultsService;

  public ProfilesController(KeepsService keepsService, VaultsService vaultsService, AccountService accountService)
  {
    _keepsService = keepsService;
    _vaultsService = vaultsService;
    _accountService = accountService;
  }

  [HttpGet("{profileId}")]
  public ActionResult<Profile> getAUsersProfile(string profileId)
  {
    try
    {
      Profile profile = _accountService.getAUsersProfile(profileId);
      return Ok(profile);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/keeps")]
  public ActionResult<List<Keep>> getAUsersKeeps(string profileId)
  {
    try
    {
      List<Keep> keeps = _keepsService.getAUsersKeeps(profileId);
      return Ok(keeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{profileId}/vaults")]
  public ActionResult<List<Vault>> getAUsersVaults(string profileId)
  {
    try
    {
      List<Vault> vaults = _vaultsService.getAUsersVaults(profileId);
      return Ok(vaults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
