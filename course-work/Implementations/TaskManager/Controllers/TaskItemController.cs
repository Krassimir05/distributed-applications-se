using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Views.ViewModels;

namespace TaskManager.Controllers
{
    public class TaskItemController : Controller
    {
            private readonly ApplicationDbContext _context;

            public TaskItemController(ApplicationDbContext context)
            {
                _context = context;
            }

            // GET: TaskItem
            public async Task<IActionResult> Index()
            {
            var tasks = await _context.TaskItems
                .Include(t => t.Project)
                .Select(t => new TaskItemViewModel
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    DueDate = t.DueDate,
                    ProjectId = t.ProjectId,
                    ProjectName = t.Project.Name
                })
                .ToListAsync();

            return View(tasks);
        }

            public async Task<IActionResult> Details(int? id)
            {
                if (id == null) return NotFound();

                var taskItem = await _context.TaskItems
                    .Include(t => t.Project)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (taskItem == null) return NotFound();

                return View(taskItem);
            }

            public IActionResult Create()
            {
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name");
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(TaskItem taskItem)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(taskItem);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", taskItem.ProjectId);
                return View(taskItem);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null) return NotFound();

                var taskItem = await _context.TaskItems.FindAsync(id);
                if (taskItem == null) return NotFound();

                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", taskItem.ProjectId);
                return View(taskItem);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, TaskItem taskItem)
            {
                if (id != taskItem.Id) return NotFound();

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(taskItem);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!TaskItemExists(taskItem.Id))
                            return NotFound();
                        else
                            throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Name", taskItem.ProjectId);
                return View(taskItem);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null) return NotFound();

                var taskItem = await _context.TaskItems
                    .Include(t => t.Project)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (taskItem == null) return NotFound();

                return View(taskItem);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var taskItem = await _context.TaskItems.FindAsync(id);
                if (taskItem != null)
                {
                    _context.TaskItems.Remove(taskItem);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }

            private bool TaskItemExists(int id)
            {
                return _context.TaskItems.Any(e => e.Id == id);
            }
        }
    }