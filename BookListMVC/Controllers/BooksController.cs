﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookListMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookListMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        //API CALLS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookFromDb = await _db.Books.FirstOrDefaultAsync(book => book.Id == id);

            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "error while deleting" });

            }

            _db.Books.Remove(bookFromDb);

            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Delete sucess!" });
        }
    }
}