﻿using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V1
{
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

        [SwaggerOperation(Summary ="Retrieves all posts")]
        [HttpGet]
        public IActionResult Get()
        {
            var posts = _postService.GetAllPosts();
            return Ok(posts);
        }

        [SwaggerOperation(Summary = "Retrieves post")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var post = _postService.GetPostById(id);
            
            if(post==null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [SwaggerOperation(Summary = "Retrieve posts by phrase")]
        [HttpGet("Search/{phrase}")]
        public IActionResult Get(string phrase)
        {
            var posts = _postService.GetPostsByPhrase(phrase);

            return Ok(posts);
        }

        [SwaggerOperation(Summary = "Create new post")]
        [HttpPost]
        public IActionResult Create(CreatePostDto newPost)
        {
            var post = _postService.AddNewPost(newPost);
            return Created($"api/posts/{post.Id}", post);
        }

        [SwaggerOperation(Summary = "Update post")]
        [HttpPut]
        public IActionResult Update(UpdatePostDto updatePost)
        {
            _postService.UpdatePost(updatePost);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete post")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _postService.DeletePost(id);
            return NoContent();
        }
    }
}