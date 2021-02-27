﻿using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BloggerContext _context;

        public PostRepository(BloggerContext context)
        {
            _context = context;
        }
        public async Task<Post> AddAsync(Post post)
        {
            var createdPost = await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return createdPost.Entity;
        }

        public async Task DeleteAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _context.Posts.ToListAsync();
        }
         
        public async Task<Post> GetByIDAsync(int id)
        {
            return await _context.Posts.SingleOrDefaultAsync(x => x.ID == id);
        }

        public async Task<IEnumerable<Post>> GetByPhraseAsync(string phrase)
        {
            var posts =  _context.Posts.Where(x => x.Title.ToLower().Contains(phrase.Trim().ToLower())).ToListAsync();
            return await posts;
        }

        public async Task UpdateAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
