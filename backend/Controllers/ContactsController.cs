using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddContact([FromBody] ContactUpdateCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        DateTime parsedDate;
        if (!DateTime.TryParse(dto.BirthDate, out parsedDate))
            return BadRequest("Invalid birth date format.");

        var contact = new Contact
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Phone = dto.Phone,
            BirthDate = parsedDate.ToUniversalTime(),
            CategoryId = dto.CategoryId,
            SubcategoryId = dto.SubcategoryId,
            CustomSubcategory = dto.CustomSubcategory
        };

        try
        {
            var createdContact = await _contactService.CreateContactAsync(contact, dto.Password);
            return Ok(new { Id = createdContact.Id });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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