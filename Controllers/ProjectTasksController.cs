using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPracticeApi.Entities;
using ProjectPracticeApi.Models;

[ApiController]
[Route("[controller]")]
public class ProjectTasksController : ControllerBase
{
    private readonly ProjectDbContext _context;
    private readonly ILogger<ProjectTasksController> _logger;

    public ProjectTasksController(ProjectDbContext context, ILogger<ProjectTasksController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectTask>>> GetProjectTasks()
    {
        return await _context.ProjectTasks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTask>> GetProjectTask(int id)
    {
        var projectTask = await _context.ProjectTasks.FindAsync(id);

        if (projectTask == null)
        {
            _logger.LogError("Could not find project task with ID {0}", id);
            return NotFound();
        }

        return projectTask;
    }

    [HttpPost]
    public async Task <ActionResult<ProjectTask>> AddProjectTask(ProjectTaskInput model)
    {
        // TODO: Use Automapper
        ProjectTask projectTask = new ProjectTask
        {
            Name = model.Name,
            Description = model.Description,
            Responsible = model.Responsible,
            Priority = model.Priority,
            EpicID = model.EpicID
        };

        _context.ProjectTasks.Add(projectTask);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Project task with ID {0} added", 0);

        return CreatedAtAction(nameof(GetProjectTask), new { id = projectTask.ID}, projectTask);
    }

    [HttpPut("{id}")]
    public async Task <IActionResult> UpdateProjectTask(int id, ProjectTaskInput model)
    {
        var existing = await _context.ProjectTasks.SingleAsync(x => x.ID == id);

        // TODO: Use Automapper
        existing.Name = model.Name;
        existing.Description = model.Description;
        existing.Responsible = model.Responsible;
        existing.Priority = model.Priority;
        existing.EpicID = model.EpicID;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException e)
        {
            if (!_context.ProjectTasks.Any(pt => pt.ID == id))
            {
                _logger.LogError("Could not find project task with ID {0}", id);
                return NotFound();
            }
            else
            {
                _logger.LogError("Concurrency violation exception thrown: ", e);
                throw;
            }
        }

        _logger.LogInformation("Project task with ID {0} was updated", id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProjectTask(int id)
    {
        var projectTask = await _context.ProjectTasks.FindAsync(id);
        if (projectTask == null)
        {
            _logger.LogError("Could not find project task with ID {0}", id);
            return NotFound();
        }

        _context.ProjectTasks.Remove(projectTask);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Project task with ID {0} was deleted.", id);
        return NoContent();
    }
}