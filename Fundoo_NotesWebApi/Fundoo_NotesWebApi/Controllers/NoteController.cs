using BuisnessLayer.Interface;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundoo_NotesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {

        INoteBL noteBL;
        FundooContext fundooContext;

        public NoteController(INoteBL noteBL, FundooContext fundooContext)
        {
            this.noteBL = noteBL;
            this.fundooContext = fundooContext;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                await this.noteBL.AddNote(userId, notePostModel);
                return this.Ok(new { success = true, message = "Note Added Successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        [Authorize]
        [HttpGet]
   
        public async Task<ActionResult> GetAllNote()
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId);
                if (note == null)
                {
                 
                    return this.BadRequest(new { success = true, message = "Note Doesn't Exits" });

                }
                List<Note> noteList = new List<Note>();

                noteList =  await this.noteBL.GetAllNote(userId);

                return Ok(new { success = true, message = "GetAllNote Successfully", data= noteList });

            }
            catch (Exception e)
            {
                throw e;
            }

        }




        [Authorize]
        [HttpDelete("{NoteId}")]
       
        public async Task<ActionResult> DeleteNote(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.noteId == NoteId);
                if (note == null)
                {

                    return this.BadRequest(new { success = true, message = "Sorry! Note Doesn't Exist Please Create a Notes" });

                }
                await this.noteBL.DeleteNote(userId, NoteId);
            
                return Ok(new { success = true, message = $"Note Deleted Successfully for the note, {note.Title} "});

            }
            catch (Exception e)
            {
                throw e;
            }
            //$"cartList fetched Fail {e.Message}"
        }

    } 
}

//{
//    try
//    {
//        var currentUser = HttpContext.User;
//        int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
//        await this.noteBL.GetAllNote(userId);
//        return this.Ok(new { success = true, message = "Note Got Successfully" });
//    }
//    catch (Exception e)
//    {
//        throw e;
//    }
//}




//[HttpPost]
//[Route("GetNotes")]
//public async Task<IActionResult> GetNotes([FromBody] string userId)
//{
//    try
//    {
//        var result = this.manager.GetNotes(userId);
//        if (result != null)
//        {
//            return this.Ok(new { Status = true, Message = "Note Received", Data = result });
//        }
//        else
//        {
//            return this.BadRequest(new { Status = true, Message = "Note Received UnSuccessfully", Data = result });
//        }
//    }
//    catch (Exception ex)
//    {
//        throw new Exception(ex.Message);
//    }
//}