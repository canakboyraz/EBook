using MVCFinalProje.Business.DTOs.BookDTOs;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.BookServices
{
    public interface IBookService
    {
        Task<IResult> AddAsync(BookCreateDTO bookCreateDTO);

        Task<IDataResult<List<BookListDTO>>> GetAllAsync();

        Task<IResult> DeleteByAsync(Guid id);

        Task<IDataResult<BookDTO>> GetByIdAsync(Guid id);

        Task<IResult> UpdateByAsync(BookUpdateDTO bookUpdateDTO);
    }
}
