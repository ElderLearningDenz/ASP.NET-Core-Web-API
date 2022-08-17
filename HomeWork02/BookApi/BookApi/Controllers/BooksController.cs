using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookApi.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            try
            {
                return Ok(StaticDb.Books);
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }

        [HttpGet("queryString")]
        public ActionResult<Book> GetBookByQueryString(int? index)
        {
            try
            {
                if (index == null)
                {
                    return BadRequest("Index is a required parameter!");
                }

                if (index < 0)
                {
                    return BadRequest("Index can not be a negative value!");
                }

                if (index >= StaticDb.Books.Count)
                {
                    return NotFound($"There is no resource on index:{index}");
                }

                return Ok(StaticDb.Books[index.Value]);
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }

        [HttpGet("multipleQueryStrings")]
        public ActionResult<List<Book>> FilterBookByAuthorOrTitle(string? author, string? title)
        {
            try
            {
                if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(title))
                {
                    return BadRequest("You must enter at least one filter parameter!");
                }

                if (string.IsNullOrEmpty(author))
                {
                    List<Book> filteredBook = StaticDb.Books.Where(x => x.Title == title).ToList();
                    return Ok(filteredBook);
                }

                if (string.IsNullOrEmpty(title))
                {
                    List<Book> filteredBook = StaticDb.Books.Where(x => x.Author == author).ToList();
                    return Ok(filteredBook);
                }

                List<Book> booksDb = StaticDb.Books.Where(x => x.Author.ToLower().Contains(author.ToLower()) && x.Title.ToLower().Contains(title.ToLower())).ToList();
                return Ok(booksDb);
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }

        [HttpPost]
        public IActionResult PostBook([FromBody] Book book)
        {
            try
            {
                if (string.IsNullOrEmpty(book.Author) && string.IsNullOrEmpty(book.Title))
                {
                    return BadRequest("Please fill the author and title fields to add new book!");
                }

                StaticDb.Books.Add(book);
                return StatusCode(StatusCodes.Status201Created, "Book added!");
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }

        [HttpPost("getAllBookTitles")]
        public ActionResult<List<string>> AllBooks([FromBody] List<Book> books)
        {
            try
            {
                if (books== null)
                {
                    return BadRequest("Error! Empty Book List...");
                }
                return Ok(books.Select(x => x.Title).ToList());
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, please contact the admin!");
            }
        }
    }
}

