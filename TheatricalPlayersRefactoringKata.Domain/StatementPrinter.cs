using System;
using System.Collections.Generic;
using System.Globalization;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Entities.Play;

namespace TheatricalPlayersRefactoringKata.Domain;

public class StatementPrinter
{
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = string.Format("Statement for {0}\n", invoice.Customer);
        CultureInfo cultureInfo = new CultureInfo("en-US");

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            /* var lines = play.Lines;
               if (lines < 1000) lines = 1000;
               if (lines > 4000) lines = 4000;
            */

            var thisAmount = play.CalculateAmount(perf.Audience);
            volumeCredits += play.CalculateVolumeCredits(perf.Audience);

            /*switch (play.Type) 
            {
            case "tragedy":
            if (perf.Audience > 30) {
            thisAmount += 1000 * (perf.Audience - 30);
            }
            break;
            case "comedy":
            if (perf.Audience > 20) {
            thisAmount += 10000 + 500 * (perf.Audience - 20);
            }
            thisAmount += 300 * perf.Audience;
            break;
            default:
            throw new Exception("unknown type: " + play.Type);
              }
             add volume credits

             add extra credit for every ten comedy attendees
             if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

             print line for this order
             result += String.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(thisAmount / 100), perf.Audience);
             
             totalAmount += thisAmount;
             */


            result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n",
                play.Name, thisAmount, perf.Audience);

            
            totalAmount += (int)thisAmount;

        }

        /*
        result += string.Format(cultureInfo, "  {0}: {1:C} ({2} seats)\n",
        play.Name, thisAmount, perf.Audience);
        result += String.Format("You earned {0} credits\n", volumeCredits);
        */

        result += string.Format(cultureInfo, "Amount owed is {0:C}\n", totalAmount);
        result += string.Format("You earned {0} credits\n", volumeCredits);

        return result;
    }
}
