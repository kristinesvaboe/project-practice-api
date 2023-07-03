namespace ProjectPracticeApi.Entities;

public class Epic
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public required int ProjectID { get; set; }
    public Project? Project { get; set; }

    public List<ProjectTask>? ProjectTasks { get; set; }
}