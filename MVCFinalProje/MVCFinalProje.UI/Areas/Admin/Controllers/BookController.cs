using Mapster;
using Microsoft.AspNetCore.Mvc;
using MVCFinalProje.Business.DTOs.BookDTOs;
using MVCFinalProje.Business.DTOs.PublisherDTOs;
using MVCFinalProje.Business.Services.BookServices;
using MVCFinalProje.UI.Areas.Admin.Models.BookVM;
using MVCFinalProje.UI.Models.PublisherVM;

namespace MVCFinalProje.UI.Areas.Admin.Controllers
{
    public class BookController : AdminBaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
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

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminBookCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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
            var result = await _bookService.GetByIdAsync(id);
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return RedirectToAction("Index");
            }
            SuccesNotyf(result.Message);
            return View(result.Data.Adapt<AdminBookUpdateVM>());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(AdminBookUpdateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _bookService.UpdateByAsync(model.Adapt<BookUpdateDTO>());
            if (!result.IsSuccess)
            {
                ErrorNotyf(result.Message);
                return View(model);
            }
            SuccesNotyf(result.Message);
            return RedirectToAction("Index");
        }
    }
}
