using Mapster;
using MVCFinalProje.Business.DTOs.AuthorDTOs;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Domain.Entities;
using MVCFinalProje.Infrastructure.Repositories.AuthorRepository;
using MVCFinalProje.Infrastructure.Repositories.PublisherRepository;
using MVCFinalProje.Utilities.Concretes;
using MVCFinalProje.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFinalProje.Business.Services.PublisherServices
{
    public class PublisherService : IPublisherService
    {

        private readonly IPublisherRepository _publisherRepository;

        public PublisherService(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository;

        }
        public async Task<IResult> AddAsync(PublisherCreateDTO publisherCreateDTO)
        {
            if (await _publisherRepository.AnyAsync(x => x.Name.ToLower() == publisherCreateDTO.Name.ToLower()))
            {
                return new ErrorResult("YayınEvi Sistemde kayıtlı");
            }

            try
            {
                var newPublisher= publisherCreateDTO.Adapt<Publisher>();
                await _publisherRepository.AddAsync(newPublisher);
                await _publisherRepository.SaveChangeAsync();
                return new SuccessResult("YayınEvi Ekleme Başarılı");
            }
            catch (Exception ex)
            {

                return new ErrorResult(ex.Message);
            }
        }

        public async Task<IResult> DeleteByAsync(Guid id)
        {
            var deletePublisher = await _publisherRepository.GetByIdAsync(id);
            if (deletePublisher == null)
            {
                return new ErrorResult("Silenecek YayınEvi Bulunamadı");
            }

            try
            {
                await _publisherRepository.DeleteAsync(deletePublisher);
                await _publisherRepository.SaveChangeAsync();
                return new SuccessResult("YayınEvi başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("YayınEvi silinirken bir hata oluştu.  " + ex.Message);
            }
        }

        public async Task<IDataResult<List<PublisherListDTO>>> GetAllAsync()
        {
            var publishers = await _publisherRepository.GetAllAsync();
            var publisherListDTOs = publishers.Adapt<List<PublisherListDTO>>();
            if (publishers.Count() <= 0)
            {
                return new ErrorDataResult<List<PublisherListDTO>>(publisherListDTOs, "Listelenecek YayınEvi Bulunamadı");
            }

            return new SuccessDataResult<List<PublisherListDTO>>(publisherListDTOs, "YayınEvi Listeleme Başarılı");
        }

        public async Task<IDataResult<PublisherDTO>> GetByIdAsync(Guid id)
        {
            var publisher = await _publisherRepository.GetByIdAsync(id);
            try
            {
                if (publisher is null)
                {
                    return new ErrorDataResult<PublisherDTO>(publisher.Adapt<PublisherDTO>(), "YayınEvi güncellenirken bir hata oluştu.  ");
                }
                var rr = new SuccessDataResult<PublisherDTO>(publisher.Adapt<PublisherDTO>(), "Güncellenirken YayınEvi getirildi");
                return rr;
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<PublisherDTO>(publisher.Adapt<PublisherDTO>(), "YayınEvi güncellenirken bir hata oluştu.  " + ex.Message);
            }
        }

        public async Task<IResult> UpdateByAsync(PublisherUpdateDTO publisherUpdateDTO)
        {
            var updatePublisher = await _publisherRepository.GetByIdAsync(publisherUpdateDTO.Id);

            if (updatePublisher is null)
            {
                return new ErrorResult("Güncellenecek YayınEvi Bulunamadı");
            }

            try
            {
                var updated = publisherUpdateDTO.Adapt(updatePublisher);

                await _publisherRepository.UpdateAsync(updated);
                await _publisherRepository.SaveChangeAsync();
                return new SuccessResult("YayınEvi başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult("YayınEvi güncellenirken bir hata oluştu.  " + ex.Message);
            }
        }
    }
}
