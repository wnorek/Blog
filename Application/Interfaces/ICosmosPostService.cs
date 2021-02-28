﻿using Application.DTO;
using Application.DTO.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICosmosPostService
    {
        Task<IEnumerable<CosmosPostDto>> GetAllPostsAsync();
        Task<CosmosPostDto> GetPostByIdAsync(string id);
        Task<IEnumerable<CosmosPostDto>> GetPostsByPhraseAsync(string phrase);

        Task<CosmosPostDto> AddNewPostAsync(CreateCosmosPostDto newPost);
        Task UpdatePostAsync(UpdateCosmosPostDto updatePost);
        Task DeletePostAsync(string id);
    }
}
