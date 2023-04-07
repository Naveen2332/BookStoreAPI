using AutoMapper;
using BookStore.Data;
using BookStore.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepo : IBookRepo
    {
        public readonly BookStoreContext Context;
        private readonly IMapper mapper;
        public BookRepo(BookStoreContext context, IMapper _mapper)
        {
            Context = context;
            mapper = _mapper;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var allbook = await Context.Books.ToListAsync();
            return mapper.Map<List<BookModel>>(allbook);
            //var Books = await Context.Books.Select(books => new BookModel()
            //{
            //    Id = books.Id,
            //    Name = books.Name,
            //    Authore = books.Authore,
            //    Price = books.Price
            //}).ToListAsync();
            //return Books;
        }
        public async Task<BookModel> GetBookByIdAsync(int id)
        {
            var books = await Context.Books.FindAsync(id);
            return mapper.Map<BookModel>(books);
            //var records = await Context.Books.Where(x => x.Id == id)
            //.Select(book => new BookModel()
            //{
            //    Id = book.Id,
            //    Name = book.Name,
            //    Authore = book.Authore,
            //    Price = book.Price
            //}).FirstOrDefaultAsync();
            //return records;
        }
        public async Task<string> AddBookAsync(BookModel bookModel)
        {
            //var NewBook = new Book()
            //{
            //    Name = bookModel.Name,
            //    Authore = bookModel.Authore,
            //    Price = bookModel.Price
            //};
            var NewBook = mapper.Map<Book>(bookModel);
            await Context.Books.AddAsync(NewBook);
            await Context.SaveChangesAsync();
            return NewBook.Name;
        }
        public async Task<string> UpdateBookAsync(BookModel bookModel)
        {
            try
            {
                var entity = await Context.Books.FindAsync(bookModel.Id);
                if (entity == null) return ($"{bookModel.Name} Not Found");
                //////////Old method////////////
                //entity.Name = bookModel.Name;
                //entity.Authore = bookModel.Authore;
                //entity.Price = bookModel.Price;
                //Context.Books.Update(entity);
                //await Context.SaveChangesAsync();
                ////////////////////////////////////
                var UpdateBook = mapper.Map(bookModel, entity);
                Context.Books.Update(UpdateBook);
                await Context.SaveChangesAsync();
            }
            catch
            {
                return ($"Book {bookModel.Name} Not Found");
            }
            return ($"Book {bookModel.Name} Updated Sucessfully");
        }
        public async Task<string> UpdateBookPatchAsync(int id, JsonPatchDocument bookmodel)
        {
            try
            {
                var book = await Context.Books.FindAsync(id);
                if (book != null) 
                {
                    bookmodel.ApplyTo(book); 
                }
                await Context.SaveChangesAsync();
                return ("Patch value Updated Sucessfully");
            }
            catch
            {
                return ("Unable to Patch Up the Value");
            }

        }
        public async Task<bool> DeleteAsync(int id)
        {
            bool sucess;
            try
            {
                var entity = Context.Books.Find(id);
                if (entity != null)
                {
                    Context.Books.Remove(entity);
                    await Context.SaveChangesAsync();
                    sucess = true;
                }
                else { sucess = false; }
            }
            catch { sucess = false; }
            return sucess;

        }
    }
}
