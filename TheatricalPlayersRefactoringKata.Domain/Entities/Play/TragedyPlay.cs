using System;

namespace TheatricalPlayersRefactoringKata.Domain.Entities.Play;

public class TragedyPlay : Play
{
    public TragedyPlay(string name, int lines) : base(name, lines) { }

    public override decimal CalculateAmount(int audience)
    {
        decimal amount = Lines * 10;
        if (audience > 30)
        {
            amount += 1000 * (audience - 30);
        }
        return amount / 100;
    }

    public override int CalculateVolumeCredits(int audience)
    {
        return Math.Max(audience - 30, 0);
    }
}