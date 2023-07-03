using ProjectPracticeApi.Enums;

namespace ProjectPracticeApi.Entities;

public class ProjectTask
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Responsible { get; set; }
    public required Priority Priority { get; set; }

    public required int EpicID { get; set; }
    public Epic? Epic { get; set; }
}