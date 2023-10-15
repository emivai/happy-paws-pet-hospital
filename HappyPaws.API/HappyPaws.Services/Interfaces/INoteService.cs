﻿using HappyPaws.Core.Entities;

namespace HappyPaws.Application.Interfaces
{
    public interface INoteService
    {
        public Task<Note> AddAsync(Note note);
        public Task<Note> GetAsync(Guid id);
        public Task<List<Note>> GetAllAsync();
        public Task<Note> UpdateAsync(Guid id, Note note);
        public Task DeleteAsync(Guid id);
    }
}
