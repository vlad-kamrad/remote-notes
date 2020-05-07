using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RN.Application.UseCases.Notes.Commands.CreateNote;
using RN.Application.UseCases.User.Commands.CreateUser;

namespace RN.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        //public async Task<ActionResult> Get()
        public ActionResult<IEnumerable<string>> Get()
        {
         /*   var a = new CreateNoteCommand
            {
                Text="txt",
                Title="Title",
                UserId="1",
            };

            await Mediator.Send(a);
            return NoContent();*/
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
