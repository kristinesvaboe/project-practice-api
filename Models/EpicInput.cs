namespace ProjectPracticeApi.Models;

public class EpicInput 
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public int ProjectID { get; set; }
}