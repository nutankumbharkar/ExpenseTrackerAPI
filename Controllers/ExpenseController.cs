using Microsoft.AspNetCore.Mvc;
using ExpenseTrackerAPI.Services;
using ExpenseTrackerAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _service;

        public ExpenseController(IExpenseService service)
        {
            _service = service;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst("id").Value);
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var expenses = await _service.GetExpenses(GetUserId());
            return Ok(expenses);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Add(ExpenseDto dto)
        {
            await _service.AddExpense(GetUserId(), dto);
            return Ok("Added");
        }

        // PUT (UPDATE)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ExpenseDto dto)
        {
            await _service.UpdateExpense(id, GetUserId(), dto);
            return Ok("Updated");
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteExpense(id, GetUserId());
            return Ok("Deleted");
        }
    }
}