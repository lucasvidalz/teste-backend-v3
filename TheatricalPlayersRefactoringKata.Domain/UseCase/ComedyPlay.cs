using System;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain.UseCase;

public class ComedyPlay : IPlayCalculator
{
    public decimal CalculateAmount(int audience, int lines)
    {
        decimal amount = lines * 10;
        if (audience > 20)
        {
            amount += 10000 + 500 * (audience - 20);
        }

        amount += 300 * audience;
        return amount / 100;
    }

    public int CalculateVolumeCredits(int audience)
    {
        return Math.Max(audience - 30, 0) + (int)Math.Floor((decimal)audience / 5);
    }
}