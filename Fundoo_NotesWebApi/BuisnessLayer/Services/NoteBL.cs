using BuisnessLayer.Interface;
using DatabaseLayer.User;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;

        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public async Task AddNote(int UserId,NotePostModel notePostModel)
        {
            try
            {
               await this.noteRL.AddNote(UserId,notePostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
