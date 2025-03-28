using System;

namespace TheatricalPlayersRefactoringKata.Domain.Entities;

public class Play
{
    public string Name { get; set; }
    public int Lines { get; set; }
    public string Type { get; set; }

    public Play(string name, int lines)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Lines = Math.Clamp(lines, 1000, 4000);
    }
}