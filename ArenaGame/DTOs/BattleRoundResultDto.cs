using ArenaGame.Models;
using System.Text.Json.Serialization;

namespace ArenaGame.DTOs
{
    public readonly record struct BattleRoundResultDto(
        int RoundIndex,
        FighterStatusDto Fighter,
        FighterStatusDto Opponent,
        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        FightResult FightResult
    );
}
