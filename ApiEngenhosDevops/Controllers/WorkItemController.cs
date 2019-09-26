using ApiEngenhosDevops.Services;
using ApiEngenhosDevops.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiEngenhosDevops.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WorkItemController : Controller
    {
        private readonly WorkItemService _workItemService;

        public WorkItemController(WorkItemService workItemService)
        {
            _workItemService = workItemService;
        }

        [HttpGet]
        public ActionResult<List<WorkItem>> Get() =>
            _workItemService.Get();

        [HttpGet("{id:length(24)}", Name = "GetWorkItem")]
        public ActionResult<WorkItem> Get(string id)
        {
            var book = _workItemService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpPost]
        public ActionResult<WorkItem> Create(WorkItem book)
        {
            _workItemService.Create(book);

            return CreatedAtRoute("GetWorkItem", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, WorkItem bookIn)
        {
            var book = _workItemService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _workItemService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _workItemService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _workItemService.Remove(book.Id);

            return NoContent();
        }
    }
}