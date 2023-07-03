using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectPracticeApi.Entities;
using ProjectPracticeApi.Models;

[ApiController]
[Route("[controller]")]
public class EpicsController : ControllerBase
{
    private readonly ProjectDbContext _context;
    private readonly ILogger<EpicsController> _logger;

    public EpicsController(ProjectDbContext context, ILogger<EpicsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Epic>>> GetEpics()
    {
        return await _context.Epics.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Epic>> GetEpic(int id)
    {
        var epic = await _context.Epics.FindAsync(id);

        if (epic == null)
        {
            _logger.LogError("Could not find epic with ID {0}", id);
            return NotFound();
        }

        return epic;
    }

    [HttpPost]
    public async Task <ActionResult<Epic>> AddEpic(EpicInput model)
    {
        // TODO: Use Automapper
        Epic epic = new Epic
        {
            Name = model.Name,
            Description = model.Description,
            ProjectID = model.ProjectID
        };

        _context.Epics.Add(epic);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Epic with ID {0} added", 0);

        return CreatedAtAction(nameof(GetEpic), new { id = epic.ID}, epic);
    }

    [HttpPut("{id}")]
    public async Task <IActionResult> UpdateEpic(int id, EpicInput model)
    {
        var existing = await _context.Epics.SingleAsync(x => x.ID == id);

        // TODO: Use Automapper
        existing.Name = model.Name;
        existing.Description = model.Description;
        existing.ProjectID = model.ProjectID;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException e)
        {
            if (!_context.Epics.Any(e => e.ID == id))
            {
                _logger.LogError("Could not find epic with ID {0}", id);
                return NotFound();
            }
            else
            {
                _logger.LogError("Concurrency violation exception thrown: ", e);
                throw;
            }
        }

        _logger.LogInformation("Epic with ID {0} was updated", id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEpic(int id)
    {
        var epic = await _context.Epics.FindAsync(id);
        if (epic == null)
        {
            _logger.LogError("Could not find epic with ID {0}", id);
            return NotFound();
        }

        _context.Epics.Remove(epic);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Epic with ID {0} was deleted.", id);
        return NoContent();
    }

    [HttpGet("{id}/projecttasks")]
    public async Task<ActionResult<IEnumerable<ProjectTask>>> GetProjectTasks(int id)
    {
        return await _context.ProjectTasks.Where(t => t.EpicID == id).ToListAsync();
    }
}