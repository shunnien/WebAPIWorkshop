﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AttributeRouting.Models;

namespace AttributeRouting.Controllers
{
    [RoutePrefix("books")]
    public class BooksController : ApiController
    {
        private BookAPIContext db = new BookAPIContext();

        [Route("")]
        public IQueryable<Book> GetBooks()
        {
            return db.Books;
        }

        [Route("{id:int}")]
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        [Route("{genre}")]
        public IHttpActionResult GetBookByGenre(string genre)
        {
            var books = db.Books.Include(b => b.Author)
                .Where(b => b.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase));

            return Ok(books);
        }

        [Route("~/authors/{authorId:int}/books")]
        public IHttpActionResult GetBooksByAuthor(int authorId)
        {
            var author = db.Books.Include(b => b.Author)
                .Where(b => b.AuthorId == authorId);

            return Ok(author);
        }

        [Route("date/{pubdate:datetime}")]
        [Route("date/{pubdate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [Route("date/{*pubdate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        public IHttpActionResult Get(DateTime pubdate)
        {
            var books = db.Books.Include(b => b.Author)
                .Where(b => DbFunctions.TruncateTime(b.PublishDate)
                            == DbFunctions.TruncateTime(pubdate)
                );

            return Ok(books);
        }

        [Route("multiRuleDate/{pubdate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        [Route("multiRuleDate/{*pubdate:datetime:regex(\\d{4}/\\d{2}/\\d{2})}")]
        public IHttpActionResult GetMultiRuleDate(DateTime pubdate)
        {
            var books = db.Books.Include(b => b.Author)
                .Where(b => DbFunctions.TruncateTime(b.PublishDate)
                            == DbFunctions.TruncateTime(pubdate)
                );

            return Ok(books);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.BookId)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(Book))]
        public IHttpActionResult PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public IHttpActionResult DeleteBook(int id)
        {
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.BookId == id) > 0;
        }
    }
}