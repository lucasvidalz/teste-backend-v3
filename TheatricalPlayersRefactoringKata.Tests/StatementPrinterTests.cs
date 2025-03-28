using System;
using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using TheatricalPlayersRefactoringKata.Domain;
using TheatricalPlayersRefactoringKata.Domain.Entities;
using TheatricalPlayersRefactoringKata.Domain.Entities.Play;
using TheatricalPlayersRefactoringKata.Domain.Interfaces;
using TheatricalPlayersRefactoringKata.Domain.UseCase;
using Xunit;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    private readonly Dictionary<string, IPlayCalculator> _playCalculators = new()
    {
        { "tragedy", new TragedyPlay() },
        { "comedy", new ComedyPlay() },
        { "historical", new HistoricalPlay() }
    };

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024) { Type = "tragedy" } },
            { "as-like", new Play("As You Like It", 2670) { Type = "comedy" } },
            { "othello", new Play("Othello", 3560) { Type = "tragedy" } }
        };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter(_playCalculators);
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024) { Type = "tragedy" } },
            { "as-like", new Play("As You Like It", 2670) { Type = "comedy" } },
            { "othello", new Play("Othello", 3560) { Type = "tragedy" } },
            { "henry-v", new Play("Henry V", 3227) { Type = "historical" } },
            { "john", new Play("King John", 2648) { Type = "historical" } },
            { "richard-iii", new Play("Richard III", 3718) { Type = "historical" } }
        };

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter(_playCalculators);
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var plays = new Dictionary<string, Play>
        {
            { "hamlet", new Play("Hamlet", 4024) { Type = "tragedy" } },
            { "as-like", new Play("As You Like It", 2670) { Type = "comedy" } },
            { "othello", new Play("Othello", 3560) { Type = "tragedy" } },
            { "henry-v", new Play("Henry V", 3227) { Type = "historical" } },
            { "john", new Play("King John", 2648) { Type = "historical" } },
            { "richard-iii", new Play("Richard III", 3718) { Type = "historical" } }
        };

        var invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
                new Performance("henry-v", 20),
                new Performance("john", 39),
                new Performance("henry-v", 20)
            }
        );

        var playCalculators = new Dictionary<string, IPlayCalculator>
        {
            { "tragedy", new TragedyPlay() },
            { "comedy", new ComedyPlay() },
            { "historical", new HistoricalPlay() }
        };

        var xmlPrinter = new XmlStatementPrinter(playCalculators);
        var result = xmlPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }
}