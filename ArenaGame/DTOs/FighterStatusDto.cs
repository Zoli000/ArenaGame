namespace ArenaGame.DTOs
{
    public readonly record struct FighterStatusDto(
        int Id,
        string Type,
        int HealthBefore,
        int HealthAfter,
        bool Survived
    );
}
