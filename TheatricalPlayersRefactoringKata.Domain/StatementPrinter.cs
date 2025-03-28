using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain;

public class StatementPrinter
{
    private readonly Dictionary<string, IPlayCalculator> _playCalculators;

    public StatementPrinter(Dictionary<string, IPlayCalculator> playCalculators)
    {
        _playCalculators = playCalculators;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var playCalculator = _playCalculators[play.Type];

            var thisAmount = playCalculator.CalculateAmount(perf.Audience, play.Lines);
            volumeCredits += playCalculator.CalculateVolumeCredits(perf.Audience);

            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n",
                play.Name, thisAmount, perf.Audience);

            totalAmount += thisAmount;
        }

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += $"You earned {volumeCredits} credits\n";

        return result;
    }
}
