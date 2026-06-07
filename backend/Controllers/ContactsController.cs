using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactsController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetContactList()
    {
        var contacts = await _contactService.GetContactsAsync();
        var dtoList = contacts.Select(c => new ContactListDto
        {
            Id = c.Id,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email ?? string.Empty,
            Phone = c.Phone
        }).ToList();
        
        return Ok(dtoList);
    }
    
    [HttpGet]
    [Route("{id}")]
    public IActionResult GetContact(string id)
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
    public IActionResult UpdateContact(string id)
    {
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult DeleteContact(string id)
    {
        return Ok();
    }

}