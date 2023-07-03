using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPracticeApi.Entities;
using ProjectPracticeApi.Models;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ProjectDbContext _context;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(ProjectDbContext context, ILogger<ProjectsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
    {
        return await _context.Projects.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);

        if (project == null)
        {
            _logger.LogError("Could not find project with ID {0}", id);
            return NotFound();
        }

        return project;
    }

    [HttpPost]
    public async Task <ActionResult<Project>> AddProject(ProjectInput model)
    {
        // TODO: Use Automapper
        Project project = new Project
        {
            Name = model.Name,
            Description = model.Description,
            ProjectManager = model.ProjectManager,
            Status = model.Status
        };
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Project with ID {0} added", 0);

        return CreatedAtAction(nameof(GetProject), new { id = project.ID}, project);
    }

    [HttpPut("{id}")]
    public async Task <IActionResult> UpdateProject(int id, ProjectInput model)
    {
        var existing = await _context.Projects.SingleAsync(x => x.ID == id);

        // TODO: Use Automapper
        existing.Name = model.Name;
        existing.Description = model.Description;
        existing.ProjectManager = model.ProjectManager;
        existing.Status = model.Status;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException e)
        {
            if (!_context.Projects.Any(p => p.ID == id))
            {
                _logger.LogError("Could not find project with ID {0}", id);
                return NotFound();
            }
            else
            {
                _logger.LogError("Concurrency violation exception thrown: ", e);
                throw;
            }
        }

        _logger.LogInformation("Project with ID {0} was updated", id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            _logger.LogError("Could not find project with ID {0}", id);
            return NotFound();
        }

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Project with ID {0} was deleted.", id);
        return NoContent();
    }

    [HttpGet("{id}/epics")]
    public async Task<ActionResult<IEnumerable<Epic>>> GetEpics(int id)
    {
        return await _context.Epics.Where(e => e.ProjectID == id).ToListAsync();
    }
}