using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Application.DTO;
using API.Application.Services.IServices;
using API.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/profil")]
    public class ProfilController : ControllerBase
    {

        private readonly IProfilService _profilService;
        private readonly ILogger<ProfilController> _logger;

        public ProfilController(IProfilService profilService, ILogger<ProfilController> logger)
        {
            _profilService = profilService;
            _logger = logger;
        }
        
        [Authorize]
        [HttpPost("save")] 
        public async Task<IActionResult> SaveProfil([FromBody] Profil profil)
        { 
            if (ModelState.IsValid)
            { 
                var userProfil = await _profilService.CreateProfilAsync(profil); 
                if (userProfil != null)
                { 
                    return Ok(ApiResponse<Profil>.SuccessResponse(userProfil, "Profil enregisté."));
                }

                return Ok(ApiResponse<Profil>.Fail("Le profil existe déjà pour cet utilisateur."));
            }
            return Ok(ApiResponse<Profil>.Fail("Tout les champs du formulaire sont requis"));
        }
    }
}