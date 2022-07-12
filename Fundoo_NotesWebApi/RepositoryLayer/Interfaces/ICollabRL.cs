using DatabaseLayer.Collaborator;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ICollabRL
    {
        Task AddCollab(int UserId,int Noteid, string collabEmail);

        Task<List<Collaborator>> GetAllCollab(int UserId);
        Task DeleteCollab(int UserId, int NoteId);
    }
}
