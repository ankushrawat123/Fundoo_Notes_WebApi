using DatabaseLayer.Collaborator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    
    public class CollabRL : ICollabRL
    {
        FundooContext fundooContext;
        IConfiguration configuration;

        public CollabRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
        }
        public async Task AddCollab(int UserId, int Noteid, string collabEmail)
        {
            try
            {
                
                Collaborator collaborator = new Collaborator();
                collaborator.UserId = UserId;
                collaborator.NoteId = Noteid;
                collaborator.CollabEmail = collabEmail;
                fundooContext.collaborators.Add(collaborator);
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteCollab(int UserId, int NoteId)
        {
            try
            {
                var collab = fundooContext.collaborators.FirstOrDefault(c => c.UserId == UserId && c.NoteId == NoteId);
               if(collab!=null)
                {
                    fundooContext.collaborators.Remove(collab);
                    await fundooContext.SaveChangesAsync();
                }
               
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Collaborator>> GetAllCollab(int UserId)
        {
            try
            {
                var collab = fundooContext.collaborators.FirstOrDefault(c => c.UserId == UserId);
                if(collab==null)
                {
                    return null;
                }
                return await fundooContext.collaborators.ToListAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
