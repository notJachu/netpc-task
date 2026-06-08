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
    [Route("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var categories = await _contactService.GetCategoriesAsync();
        return Ok(categories);
    }
    
    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetContact(string id)
    {
        var contact = await _contactService.GetContactByIdAsync(id);
        if (contact == null)
            return NotFound();

        var dto = new ContactDetailsDto
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email ?? string.Empty,
            Phone = contact.Phone,
            BirthDate = contact.BirthDate.ToString("yyyy-MM-dd"),
            Category = contact.Category?.Name ?? string.Empty,
            Subcategory = contact.Subcategory?.Name ?? contact.CustomSubcategory,
            CategoryId = contact.CategoryId,
            SubcategoryId = contact.SubcategoryId,
            CustomSubcategory = contact.CustomSubcategory
        };

        return Ok(dto);
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

    [Authorize]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateContact(string id, [FromBody] ContactUpdateCreateDto dto)
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
            await _contactService.UpdateContactAsync(id, contact, dto.Password);
            return NoContent();
        }
        catch (Exception ex)
        {
            if (ex.Message == "Contact not found")
                return NotFound();
                
            return BadRequest(ex.Message);
        }
    }

    [Authorize]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteContact(string id)
    {
        var success = await _contactService.DeleteContactAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }

}