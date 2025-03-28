using System;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.UseCase;

public class TragedyPlay : IPlayCalculator
{
    public decimal CalculateAmount(int audience, int lines)
    {
        decimal amount = lines * 10;
        if (audience > 30)
        {
            amount += 1000 * (audience - 30);
        }
        return amount / 100;
    }

    public int CalculateVolumeCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }
}