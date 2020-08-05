using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Entities;
using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : ControllerBase
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepositoryAuthor _repositoryAuthor;
        private readonly IClientSessionHandle _clientSessionHandle;

        public BusinessController(IRepositoryUser repositoryUser, IRepositoryAuthor repositoryAuthor, IClientSessionHandle clientSessionHandle) =>
            (_repositoryUser, _repositoryAuthor, _clientSessionHandle) = (repositoryUser, repositoryAuthor, clientSessionHandle);

        [HttpPost]
        [Route("user")]
        public async Task<IActionResult> InsertUser([FromBody] CreateUserModel userModel)
        {
            _clientSessionHandle.StartTransaction();

            try
            {
                var user = new User(userModel.Name, userModel.Nin);
                await _repositoryUser.InsertAsync(user);
                await _clientSessionHandle.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await _clientSessionHandle.AbortTransactionAsync();

                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("authorAndUser")]
        public async Task<IActionResult> InsertAuthorAndUser([FromBody] CreateAuthorAndUserModel authorAndUserModel)
        {
            _clientSessionHandle.StartTransaction();

            try
            {
                var author = new Author(authorAndUserModel.AuthorModel.Name, new List<Book>(authorAndUserModel.AuthorModel.Books.Select(s => new Book(s.Name, s.Year))));
                var user = new User(authorAndUserModel.UserModel.Name, authorAndUserModel.UserModel.Nin);

                await _repositoryAuthor.InsertAsync(author);
                await _repositoryUser.InsertAsync(user);
                await _clientSessionHandle.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                await _clientSessionHandle.AbortTransactionAsync();

                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("author/{id}")]
        public async Task<IActionResult> DeleteAuthorAsync([FromRoute] string id)
        {
            _clientSessionHandle.StartTransaction();

            try
            {
                await _repositoryAuthor.DeleteAsync(id);
                await _clientSessionHandle.CommitTransactionAsync();
                
                return Ok();
            }
            catch (Exception ex)
            {
                await _clientSessionHandle.AbortTransactionAsync();

                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("author/{id}")]
        public async Task<IActionResult> DeleteAuthorAsync([FromRoute] string id, [FromBody] UpdateAuthorModel authorModel)
        {
            _clientSessionHandle.StartTransaction();

            try
            {
                var author = new Author(authorModel.Name, new List<Book>(authorModel.Books.Select(s => new Book(s.Name, s.Year))));
                author.SetId(id);

                await _repositoryAuthor.UpdateAsync(author);
                await _clientSessionHandle.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception ex)
            {

                await _clientSessionHandle.AbortTransactionAsync();

                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("author/books/{id}")]
        public async Task<IActionResult> GetAuthorsBookAsync([FromRoute] string id)
        {
            try
            {
                var books = await _repositoryAuthor.GetBooksAsync(id);
                var booksModel = new List<BookModel>(books.Select(s => new BookModel(s.Name, s.Year)));
                return Ok(booksModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}