using Mapster;
using MVCFinalProje.Business.DTOs.BookDTOs;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.Repositories.BookRepository;
using MVCFinalProje.Utilities.Concretes;
using MVCFinalProje.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IResult> AddAsync(BookCreateDTO bookCreateDTO)
        {
            if (await _bookRepository.AnyAsync(x => x.Name.ToLower() == bookCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("Kitap Sistemde kayıtlı");
            }

            try
            {
                var newBook = bookCreateDTO.Adapt<Book>();
                await _bookRepository.AddAsync(newBook);
                await _bookRepository.SaveChangeAsync();
                return new SuccessResult("Kitap Ekleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IResult> DeleteByAsync(Guid id)
        {
            var deleteBook = await _bookRepository.GetByIdAsync(id);
            if (deleteBook == null)
            {
                return new ErrorResult("Silenecek Kitap Bulunamadı");
            }

            try
            {
                await _bookRepository.DeleteAsync(deleteBook);
                await _bookRepository.SaveChangeAsync();
                return new SuccessResult("Kitap başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Kitap silinirken bir hata oluştu.  " + ex.Message);
            }
        }

        public async Task<IDataResult<List<BookListDTO>>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookListDTOs = books.Adapt<List<BookListDTO>>();
            if (books.Count() <= 0)
            {
                return new ErrorDataResult<List<BookListDTO>>(bookListDTOs, "Listelenecek Kitap Bulunamadı");
            }

            return new SuccessDataResult<List<BookListDTO>>(bookListDTOs, "Kitap Listeleme Başarılı");
        }

        public async Task<IDataResult<BookDTO>> GetByIdAsync(Guid id)
        {
            var publisher = await _bookRepository.GetByIdAsync(id);
            try
            {
                if (publisher is null)
                {
                    return new ErrorDataResult<BookDTO>(publisher.Adapt<BookDTO>(), "Kitap güncellenirken bir hata oluştu.  ");
                }
                var rr = new SuccessDataResult<BookDTO>(publisher.Adapt<BookDTO>(), "Güncellenirken Kitap getirildi");
                return rr;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<BookDTO>(publisher.Adapt<BookDTO>(), "Kitap güncellenirken bir hata oluştu.  " + ex.Message);
            }
        }

        public async Task<IResult> UpdateByAsync(BookUpdateDTO bookUpdateDTO)
        {
            var updateBook = await _bookRepository.GetByIdAsync(bookUpdateDTO.Id);

            if (updateBook is null)
            {
                return new ErrorResult("Güncellenecek Kitap Bulunamadı");
            }

            try
            {
                var updated = bookUpdateDTO.Adapt(updateBook);

                await _bookRepository.UpdateAsync(updated);
                await _bookRepository.SaveChangeAsync();
                return new SuccessResult("Kitap başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Kitap güncellenirken bir hata oluştu.  " + ex.Message);
            }
        }
    }
}
