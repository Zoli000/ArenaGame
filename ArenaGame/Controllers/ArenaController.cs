using ArenaGame.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArenaGame.Controllers
{
    [ApiController]
    [Route("/arena")]
    public class ArenaController : ControllerBase
    {
        private readonly IArenaService _arenaService;

        public ArenaController(IArenaService arenaService)
        {
            this._arenaService = arenaService;
        }

        [HttpPost]
        public IActionResult GenerateArena(int numberOfFighters)
        {
            if (numberOfFighters is <= 2)
            {
                return BadRequest("The number of fighters must be at least 2");
            }

            try
            {
                int arenaId = _arenaService.GenerateArena(numberOfFighters);
                return Ok(arenaId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while generating the arena: {ex.Message}");
            }
        }
    }
}
