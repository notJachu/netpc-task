using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{

    [HttpGet]
    [Route("list")]
    public IActionResult GetContactList()
    {
        
        return Ok();
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetContact(int id)
    {
        return Ok();
    }

    [HttpPost]
    [Route("add")]
    public IActionResult AddContact()
    {
        
        return Ok();
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult UpdateContact(int id)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteContact(int id)
    {
        return Ok();
    }

}