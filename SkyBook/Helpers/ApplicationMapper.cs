using AutoMapper;
using SkyBook.Data;
using SkyBook.Models;

namespace SkyBook.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        { 
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
