using DatabaseLayer.Collaborator;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface ICollabBL
    {
        Task AddCollab(int userId, int NoteId, string CollabEmail);
        Task<List<Collaborator>> GetAllCollab(int userId);
        Task DeleteCollab(int userid,int noteid);
    }
}
