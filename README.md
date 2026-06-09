## Task specification

### Used libraries

The task was written using .NET 8 with AspNet core. All the libraries have matching versions.

#### 1. Entity Framework Core - 8.0.27
ORM - object relational mapping.
I useed this framework as a layer connectiong the PostgreSQL database to the application

#### 2. AspNetCore Identity Framework - 8.0.27
Microsoft-provided framework for handling user authentication and authorization. 
It handles auth logic and adds endpoints related to this functionality.

#### 4. ReactJS 19
Library used to create the single page app displaying the working of the api.
With React I also used:
* Bootstrap - ui prefabs,
* React-dom-router - in-browser routing
* Vite - build tool

### Main classes

#### 1. AppDbContext
Main class that gives the application a way to communicate with the database via a connection string.

#### 2. IContactService and ContactServive
Interface and its implementation for handling business logic related to Contact entity.
Main methods are:
* `public async Task<Contact?> GetContactByIdAsync(string id)`
    Gets a contact by ID, including their category and subcategory information.
* `        public async Task<Contact> CreateContactAsync(Contact contact, string password)`
    Creates a new contact with the specified information and password.
* `        public async Task<List<CategoryDto>> GetCategoriesAsync()`
    Gets all categories with their subcategories. used for UI.

#### 3. ContactController
Main http REST controller for the contact entity.
Endpoints:
* GET
```dotnet
    // Returns contact list
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetContactList()
```

```dotnet
    // Retruns category list for UI.
    [HttpGet]
    [Route("categories")]
    public async Task<IActionResult> GetCategories()
```

```dotnet
    // Returns contact details. Requires authentication.
    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetContact(string id)
```
* POST
```dotnet
    // Creates a new contact. Requires authentication.
    [Authorize]
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> AddContact([FromBody] ContactUpdateCreateDto dto)
```
* PUT
```dotnet
    // Updates contact. Requires authentication.
    [Authorize]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateContact(string id, [FromBody] ContactUpdateCreateDto dto)
```
* DELETE
```dotnet
    // Delete contact, Requires authentication.
    [Authorize]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> DeleteContact(string id)
```

#### 4. Contact entity and categories
In this application contact is both a resource used by authenticated users as well as a user account itself.
It is a strange way to implement a contact list, however it's compliant with the task specification provided.

Categories and subcategories exist as entities in the code, however they don't have their respective controllers. Their purpose is only to enhance the functionality of the cotnact entity
thus they are handled by the contatct service and controller.

### Deployment

#### Development

#### Production