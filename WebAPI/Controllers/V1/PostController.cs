using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [SwaggerOperation(Summary = "Retrieves sort fields")]
        [HttpGet("[action]")]
        public IActionResult GetSortFields()
        {
            return Ok(SortingHelper.GetSortField().Select(x => x.Key));
        }

        [SwaggerOperation(Summary ="Retrieves paged posts")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter, [FromQuery] SortingFilter sortingFilter,
                                              [FromQuery] string filteredBy="")
        {
            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);

            var posts = await _postService.GetAllPostsAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize,
                                                            validSortingFilter.SortField, validSortingFilter.Ascending,
                                                            filteredBy);

            var totalRecords = await _postService.GetAllCountAsync(filteredBy);

            return Ok(PaginationHelper.CreatePagedResponse(posts,validPaginationFilter, totalRecords));
        }

        [SwaggerOperation(Summary ="Retrieves all posts")]
        [EnableQuery]
        [HttpGet("[action]")]
        public IQueryable<PostDto> GetAll()
        {
            return _postService.GetAllPosts();
        }

        [SwaggerOperation(Summary = "Retrieves post")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            
            if(post==null)
            {
                return NotFound();
            }

            return Ok(new Response<PostDto>(post));
        }

        [SwaggerOperation(Summary = "Retrieve posts by phrase")]
        [HttpGet("Search/{phrase}")]
        public async Task<IActionResult> Get(string phrase)
        {
            var posts = await _postService.GetPostsByPhraseAsync(phrase);

            return Ok(posts);
        }

        [SwaggerOperation(Summary = "Create new post")]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto newPost)
        {
            var post = await _postService.AddNewPostAsync(newPost);
            return Created($"api/posts/{post.Id}", new Response<PostDto>(post));
        }

        [SwaggerOperation(Summary = "Update post")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostDto updatePost)
        {
            await _postService.UpdatePostAsync(updatePost);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete post")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}
