using DatabaseLayer.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface INoteBL
    {
       Task AddNote(int UserId, NotePostModel notePostModel);

    }
}
