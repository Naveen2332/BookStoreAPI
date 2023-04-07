using AutoMapper;
using BookStore.Data;
using BookStore.Model;

namespace BookStore.Helppers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    } 
}
