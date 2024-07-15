using Mapster;
using Microsoft.EntityFrameworkCore;
using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.Repositories.AuthorRepository;
using MVCFinalProje.Utilities.Concretes;
using MVCFinalProje.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.AuthorServices
{
    public class AuthorService : IAuthorService
    {

        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
          
        }

        public async Task<IResult> AddAsync(AuthorCreateDTO authorCreateDTO)
        {
            if (await _authorRepository.AnyAsync(x=>x.Name.ToLower() == authorCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("Yazar Sistemde kayıtlı");
            }

            try
            {
                var newAuthor = authorCreateDTO.Adapt<Author>();
                await _authorRepository.AddAsync(newAuthor);
                await _authorRepository.SaveChangeAsync();
                return new SuccessResult("Yazar Ekleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IResult> DeleteByAsync(Guid id)
        {
            var auther = await _authorRepository.GetByIdAsync(id);
            if (auther == null)
            {
                return new ErrorResult("Silenecek Yazar Bulunamadı");
            }

            try
            {
                await _authorRepository.DeleteAsync(auther);
                await _authorRepository.SaveChangeAsync();
                return new SuccessResult("Yazar başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Yazar silinirken bir hata oluştu.  " +  ex.Message);
            }
        }

        public async Task<IDataResult<List<AuthorListDTO>>> GetAllAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            var authorListDTOs = authors.Adapt<List<AuthorListDTO>>();
            if (authors.Count()<=0)
            {
                return new ErrorDataResult<List<AuthorListDTO>>(authorListDTOs, "Listelenecek Yazar Bulunamadı");
            }

            return new SuccessDataResult<List<AuthorListDTO>>(authorListDTOs, "Yazar Listeleme Başarılı");

        }

        public async Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            try
            {
                if (author is null)
                {
                    return new ErrorDataResult<AuthorDTO>(author.Adapt<AuthorDTO>(), "Yazar güncellenirken bir hata oluştu.  ");
                }
                var rr = new SuccessDataResult<AuthorDTO>(author.Adapt<AuthorDTO>(), "güncellenirken yazar getirildi");
                return rr;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<AuthorDTO>(author.Adapt<AuthorDTO>(), "Yazar güncellenirken bir hata oluştu.  " + ex.Message);
            }
            
        }

        public async Task<IResult> UpdateByAsync(AuthorUpdateDTOs authorUpdateDTOs)
        {
            var updateAuthor = await _authorRepository.GetByIdAsync(authorUpdateDTOs.Id);

            if (updateAuthor is null)
            {
                return new ErrorResult("Güncellenecek Yazar Bulunamadı");
            }

            try
            {
                var updated = authorUpdateDTOs.Adapt(updateAuthor);

                await _authorRepository.UpdateAsync(updated);
                await _authorRepository.SaveChangeAsync();
                return new SuccessResult("Yazar başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("Yazar güncellenirken bir hata oluştu.  " + ex.Message);
            }
            
        }
    }
}
