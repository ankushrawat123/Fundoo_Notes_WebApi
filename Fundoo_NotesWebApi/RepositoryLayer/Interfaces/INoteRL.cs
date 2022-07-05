using DatabaseLayer.Note;
using DatabaseLayer.User;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        Task AddNote(int UserId, NotePostModel notePostModel);


        Task<List<Note>> GetAllNote(int noteid);

        Task DeleteNote(int UserId, int noteId);

        Task<Note> GetNote(int UserId,int noteId);

        Task UpdateNote(int UserId, int noteId, NoteUpdateModel noteUpdateModel );

        Task ReminderNote(int UserId, int noteId, DateTime dateTime);
        
        Task PinNote(int UserId, int noteId);

        Task ArchiveNote(int UserId, int noteId);

        Task TrashNote(int UserId, int noteId);

        Task ColourNote(int UserId, int noteId, string colour);

    }
}
