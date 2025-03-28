namespace TheatricalPlayersRefactoringKata.Domain.Interfaces;

public interface IPlayCalculator
{
    decimal CalculateAmount(int audience, int lines);
    int CalculateVolumeCredits(int audience);
}