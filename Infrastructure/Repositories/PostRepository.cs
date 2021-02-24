using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
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
        public Post Add(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

        public void Delete(Post post)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts;
        }
         
        public Post GetByID(int id)
        {
            return _context.Posts.SingleOrDefault(x => x.ID == id);
        }

        public IEnumerable<Post> GetByPhrase(string phrase)
        {
            var posts = _context.Posts.Where(x => x.Title.ToLower().Contains(phrase.Trim().ToLower()));
            return posts;
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
            _context.SaveChanges();
        }
    }
}
