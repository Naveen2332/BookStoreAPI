using BookStore.Model;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookRepo bookRepo;

        public BookController(IBookRepo _bookRepo)
        {
            bookRepo = _bookRepo;
        }

        // GET: Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBooks()
        {
            return await bookRepo.GetAllBooksAsync();
        }

        [HttpPost]
        public async Task<ActionResult<BookModel>> AddBook([FromBody] BookModel bookModel)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    return Ok("Book " + await bookRepo.AddBookAsync(bookModel) + " Added Sucessfully ");
                }
                catch (Exception ex)
                {
                    return BadRequest($"Failed to Book Added {ex}");
                }
            }
            else { return Ok("Book Data Not valid"); }

        }

        // GET: Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetBook([FromRoute] int id)
        {
            var data = await bookRepo.GetBookByIdAsync(id);
            if (data == null)
            {
                return Ok($"Book not Exist {id}");
            }
            return Ok(data);
        }

        // PUT: Book/5
        [HttpPut /*("{id}")*/ ]
        public async Task<IActionResult> PutBook([FromBody] BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                return Ok(await bookRepo.UpdateBookAsync(bookModel));
            }
            else
            {
                return BadRequest($"{bookModel.Name} book data is Invalid");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchBook([FromRoute] int id,[FromBody] JsonPatchDocument bookModel)
        {
            if (ModelState.IsValid)
            {
                return Ok(await bookRepo.UpdateBookPatchAsync(id, bookModel));                
            }
            return BadRequest("Patch-Up value Not Matched to the Model");
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteBook([FromRoute] int id)
        {
            bool sucess = await bookRepo.DeleteAsync(id);
            return sucess == true ? $"Book has Deleted Successfully"
                 : $"Book {id} Not Found";
        }

        private async Task<BookModel> BookExists(int id)
        {
            return await bookRepo.GetBookByIdAsync(id);
        }
    }
}
