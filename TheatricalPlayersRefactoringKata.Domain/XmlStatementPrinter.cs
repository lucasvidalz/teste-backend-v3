using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;

namespace TheatricalPlayersRefactoringKata.Domain;

public class XmlStatementPrinter : IXmlStatementPrinter
{
    private readonly Dictionary<string, IPlayCalculator> _playCalculators;

    public XmlStatementPrinter(Dictionary<string, IPlayCalculator> playCalculators)
    {
        _playCalculators = playCalculators;
    }

    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        decimal totalAmount = 0;
        int totalCredits = 0;
        var items = new List<XElement>();

        foreach (var perf in invoice.Performances)
        {
            var play = plays[perf.PlayId];
            var playCalculator = _playCalculators[play.Type];

            var thisAmount = playCalculator.CalculateAmount(perf.Audience, play.Lines);
            var thisCredits = playCalculator.CalculateVolumeCredits(perf.Audience);

            items.Add(new XElement("Item",
                new XElement("AmountOwed", thisAmount.ToString("0.##", CultureInfo.InvariantCulture)),
                new XElement("EarnedCredits", thisCredits),
                new XElement("Seats", perf.Audience)
            ));

            totalAmount += thisAmount;
            totalCredits += thisCredits;
        }

        var xmlDocument = new XDocument(
            new XDeclaration("1.0", "utf-8", null),
            new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", invoice.Customer),
                new XElement("Items", items),
                new XElement("AmountOwed", totalAmount.ToString("0.0##", CultureInfo.InvariantCulture)),
                new XElement("EarnedCredits", totalCredits)
            )
        );

        // Solução definitiva para o encoding UTF-8
        using var memoryStream = new MemoryStream();
        using (var writer = XmlWriter.Create(memoryStream, new XmlWriterSettings
               {
                   Encoding = Encoding.UTF8,
                   Indent = true,
                   OmitXmlDeclaration = false
               }))
        {
            xmlDocument.Save(writer);
        }

        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
}