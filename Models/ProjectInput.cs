using ProjectPracticeApi.Enums;

namespace ProjectPracticeApi.Models;

public class ProjectInput 
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ProjectManager { get; set; } = null!;
    public Status Status { get; set; }
}