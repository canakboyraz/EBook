using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCFinalProje.Business.DTOs.BookDTOs;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Business.Services.AuthorServices;
using MVCFinalProje.Business.Services.BookServices;
using MVCFinalProje.Business.Services.PublisherServices;
using MVCFinalProje.UI.Areas.Admin.Models.BookVM;
using MVCFinalProje.UI.Models.PublisherVM;

namespace MVCFinalProje.UI.Areas.Admin.Controllers
{
    public class BookController : AdminBaseController
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IPublisherService _publisherService;

        public BookController(IBookService bookService, IAuthorService authorService, IPublisherService publisherService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _publisherService = publisherService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _bookService.GetAllAsync();
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                //await Console.Out.WriteLineAsync(result.Message);
                return View(result.Data.Adapt<List<AdminBookListVM>>());
            }
            SuccesNotyf(result.Message);
            return View(result.Data.Adapt<List<AdminBookListVM>>());
        }

        // SelectList Create



        private async Task<SelectList> GetAuthors(Guid? authorId = null)
        {
            var authors =( await _authorService.GetAllAsync()).Data;
            return new SelectList(authors.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (authorId != null ? authorId.Value : authorId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }

        private async Task<SelectList> GetPublishers(Guid? publisherId = null)
        {
            var publishers = (await _publisherService.GetAllAsync()).Data;
            return new SelectList(publishers.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = x.Id == (publisherId != null ? publisherId.Value : publisherId)
            }).OrderBy(x => x.Text), "Value", "Text");
        }

        public async Task<IActionResult> Create()
        {
            AdminBookCreateVM vm = new AdminBookCreateVM()
            {
                Authors = await GetAuthors(),
                Publishers = await GetPublishers()
            };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminBookCreateVM model)
        {
            var bookCreateDTO = model.Adapt<BookCreateDTO>();
            var result = await _bookService.AddAsync(bookCreateDTO);  

            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return View(model);
            }

            SuccesNotyf(result.Message);
            return RedirectToAction("Index");
        }

        // edit ve delete

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _bookService.DeleteByAsync(id);

            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccesNotyf(result.Message);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (!book.IsSuccess)
            {
                ErrorNotyf(book.Message);
                return RedirectToAction("Index");
            }

            var vm = book.Data.Adapt<AdminBookUpdateVM>();
            vm.Authors = await GetAuthors(vm.AuthorId);
            vm.Publishers = await GetPublishers(vm.PublisherId);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AdminBookUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await GetAuthors(model.AuthorId);
                model.Publishers = await GetPublishers(model.PublisherId);
                return View(model);
            }

            var bookUpdateDTO = model.Adapt<BookUpdateDTO>();
            var result = await _bookService.UpdateByAsync(bookUpdateDTO);

            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                model.Authors = await GetAuthors(model.AuthorId);
                model.Publishers = await GetPublishers(model.PublisherId);
                return View(model);
            }

            SuccesNotyf(result.Message);
            return RedirectToAction("Index");
        }
    }
}
