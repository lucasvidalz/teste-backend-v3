using System;

namespace TheatricalPlayersRefactoringKata.Domain.Entities.Play;

public class ComedyPlay : Play
{
    public ComedyPlay(string name, int lines) : base(name, lines) { }

    public override decimal CalculateAmount(int audience)
    {
        decimal amount = Lines * 10;
        if (audience > 20)
        {
            amount += 10000 + 500 * (audience - 20);
        }
        amount += 300 * audience;
        return amount / 100;
    }

    public override int CalculateVolumeCredits(int audience)
    {
        return Math.Max(audience - 30, 0) + (int)Math.Floor((decimal)audience / 5);
    }
}