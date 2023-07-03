using ProjectPracticeApi.Enums;

namespace ProjectPracticeApi.Entities;

public class Project
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ProjectManager { get; set; }
    public required Status Status { get; set; }

    public List<Epic>? Epics { get; set; }
}