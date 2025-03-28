using System;

namespace TheatricalPlayersRefactoringKata.Domain.Entities.Play;

public abstract class Play
{
    public string Name { get; set; }
    public int Lines { get; set; }
    public string Type { get; set; }

    protected Play(string name, int lines)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Lines = Math.Clamp(lines, 1000, 4000);
    }
    public abstract decimal CalculateAmount(int audience);
    public abstract int CalculateVolumeCredits(int audience);
}