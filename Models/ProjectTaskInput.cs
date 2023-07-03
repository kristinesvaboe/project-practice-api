using ProjectPracticeApi.Enums;

namespace ProjectPracticeApi.Models;

public class ProjectTaskInput 
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Responsible { get; set; } = null!;
    public Priority Priority { get; set; }

    public int EpicID { get; set; }
}