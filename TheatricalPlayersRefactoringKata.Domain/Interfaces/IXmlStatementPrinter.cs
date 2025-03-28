using System.Collections.Generic;
using TheatricalPlayersRefactoringKata.Domain.Entities;

namespace TheatricalPlayersRefactoringKata.Domain.Interfaces;

public interface IXmlStatementPrinter
{
    string Print(Invoice invoice, Dictionary<string, Play> plays);
}