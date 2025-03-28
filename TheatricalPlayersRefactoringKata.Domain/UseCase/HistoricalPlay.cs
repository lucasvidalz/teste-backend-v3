using System;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.UseCase;

public class HistoricalPlay : IPlayCalculator
{
    public decimal CalculateAmount(int audience, int lines)
    {
        int clampedLines = Math.Clamp(lines, 1000, 4000);
        decimal tragedyAmount = new TragedyPlay().CalculateAmount(audience, clampedLines);
        decimal comedyAmount = new ComedyPlay().CalculateAmount(audience, clampedLines);
        return tragedyAmount + comedyAmount;
    }

    public int CalculateVolumeCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }
}