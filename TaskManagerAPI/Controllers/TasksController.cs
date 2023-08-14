using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Data;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        private readonly int _testValue;
        private readonly TaskManagerDBContext _taskManagerDBContext;
        public TasksController(TaskManagerDBContext taskManagerDBContext)
        {
            this._taskManagerDBContext = taskManagerDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskManagerDBContext.Tasks.ToListAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] Models.Task taskRequest)
        {
            taskRequest.id = new Guid();
            await _taskManagerDBContext.Tasks.AddAsync(taskRequest);
            await _taskManagerDBContext.SaveChangesAsync();
            return Ok(taskRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult> GetTask([FromRoute] Guid id)
        {
            var task = 
                await _taskManagerDBContext.Tasks.FirstOrDefaultAsync(t => t.id == id);
            
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);    
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ActionResult> UpdateTask([FromRoute] Guid id, [FromBody] Models.Task updateTaskRequest)
        {
            var task = await _taskManagerDBContext.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            task.text = updateTaskRequest.text;
            task.day = updateTaskRequest.day;
            task.reminder = updateTaskRequest.reminder;

            await _taskManagerDBContext.SaveChangesAsync();

            return Ok(task);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult> DeleteTask([FromRoute] Guid id)
        {
            var task = await _taskManagerDBContext.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _taskManagerDBContext.Tasks.Remove(task);
            await _taskManagerDBContext.SaveChangesAsync();

            return Ok(task);
        }

    }
}
