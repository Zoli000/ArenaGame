using ArenaGame.DTOs;
using ArenaGame.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArenaGame.Controllers
{
    [ApiController]
    [Route("/battle")]
    public class BattleController : ControllerBase
    {

        private readonly IBattleService _battleService;

        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        [HttpPost]
        public IActionResult Get(int arenaId)
        {
            try
            {
                List<BattleRoundResultDto> battleResults = _battleService.DoBattle(arenaId).ToList();
                return battleResults.Count == 0 ? NotFound("There is no battle found") : Ok(battleResults);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while generating battle log: {ex.Message}");
            }
        }
    }
}
