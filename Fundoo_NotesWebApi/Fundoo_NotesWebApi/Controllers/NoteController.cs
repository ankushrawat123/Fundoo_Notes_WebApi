using BuisnessLayer.Interface;
using DatabaseLayer.Note;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fundoo_NotesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
      
        INoteBL noteBL;
        FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;
        //private readonly IMemoryCache memoryCache;

        public NoteController(INoteBL noteBL, FundooContext fundooContext, IDistributedCache distributedCache)
        {
            this.noteBL = noteBL;
            this.fundooContext = fundooContext;
            this.distributedCache=distributedCache;
            //this.memoryCache = memoryCache;
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
                    return this.BadRequest(new { success = false, message = "Note Doesn't Exits" });
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
        [HttpGet("{noteid}")]
        public async Task<ActionResult> GetNote(int noteid)
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId" ).Value);

                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == UserId && u.noteId== noteid);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Note Doesn't Exists" });
                }
                
                var noteElement = await this.noteBL.GetNote(UserId,noteid);
                
                return Ok(new { success = true, message = "Get Note Successfully", data = noteElement });
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [Authorize]
        [HttpPut("UpdateNote/{noteid}/{noteUpdateModel}")]

        public async Task<ActionResult> UpdateNote(int noteid, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var noteCheck = fundooContext.Notes.FirstOrDefault(u => u.UserId == UserId && u.noteId == noteid);
                if (noteCheck == null)
                {
                    return this.BadRequest(new { success = false, message = "Note Doesn't Exists" });
                }

                await this.noteBL.UpdateNote(UserId,noteid,noteUpdateModel);

                return Ok(new { success = true, message = "Update Note Successfully" });
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
                    return this.BadRequest(new { success = false, message = "Sorry! Note Doesn't Exist Please Create a Notes" });
                }
                await this.noteBL.DeleteNote(userId, NoteId);
            
                return Ok(new { success = true, message = $"Note Deleted Successfully for the note, {note.Title} "});

            }
            catch (Exception e)
            {
                throw e;
            }
          
        }

        [Authorize]
        [HttpPut("ReminderNote/{NoteId}")]

        public async Task<ActionResult> ReminderNote(int NoteId,NoteReminderModel noteReminderModel)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.noteId == NoteId);
                if (note == null)
                {

                    return this.BadRequest(new { success = false, message = "Sorry! Note Doesn't Exist Please Create a Notes" });

                }
                await this.noteBL.ReminderNote(userId, NoteId, Convert.ToDateTime(noteReminderModel.Reminder));

                return Ok(new { success = true, message = $"Note Reminder Successfully for the note, {note.Title} " });

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [Authorize]
        [HttpPut("ArchiveNote/{NoteId}")]

        public async Task<ActionResult> ArchiveNote(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.noteId == NoteId);
                if (note == null)
                {

                    return this.BadRequest(new { success = false, message = "Sorry! Note Doesn't Exist Please Create a Notes" });

                }
                await this.noteBL.ArchiveNote(userId, NoteId);

                return Ok(new { success = true, message = $"Note Archive Successfully for the note, {note.Title} " });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut("PinNote/{NoteId}")]

        public async Task<ActionResult> PinNote(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.noteId == NoteId);
                if (note == null)
                {

                    return this.BadRequest(new { success =false, message = "Sorry! Note Doesn't Exist Please Create a Notes" });

                }
                await this.noteBL.PinNote(userId, NoteId);

                return Ok(new { success = true, message = $"Note Pinned Successfully for the note, {note.Title} " });

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [Authorize]
        [HttpPut("TrashNote/{NoteId}")]

        public async Task<ActionResult> TrashNote(int NoteId)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.noteId == NoteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Note Doesn't Exist Please Create a Notes" });

                }
                await this.noteBL.TrashNote(userId, NoteId);

                return Ok(new { success = true, message = $"Note Trashed Successfully for the note, {note.Title} " });

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize]
        [HttpPut("ChangeColour/{NoteId}/{colour}")]
        public async Task<ActionResult> ChangeColour(int NoteId,string colour)
        {
            try
            {
                var currentUser = HttpContext.User;
                int userId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userId && u.noteId == NoteId);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! Note Doesn't Exist Please Create a Notes" });

                }
                await this.noteBL.ColourNote(userId, NoteId, colour);
                return Ok(new { success = true, message = $"Note Coloured Successfully for the note, {note.Title} " });
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        [Authorize]
        [HttpGet("AllNoteByRedis")]

        public async Task<ActionResult> GetAllNoteByRedis()
        {
            try
            {

               var CacheKey = "NoteList";
     
                string SerializeNoteList;
                var notelist = new List<Note>();
                var redisnotelist = await distributedCache.GetAsync(CacheKey);
                if (redisnotelist != null)
                {
                    SerializeNoteList = Encoding.UTF8.GetString(redisnotelist);
                    notelist = JsonConvert.DeserializeObject<List<Note>>(SerializeNoteList);
                }

                else
                {
                    var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                    int userId = Int32.Parse(userid.Value);
                    notelist = await this.noteBL.GetAllNote(userId);
                    SerializeNoteList = JsonConvert.SerializeObject(notelist);
                    redisnotelist = Encoding.UTF8.GetBytes(SerializeNoteList);
                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    await distributedCache.SetAsync(CacheKey, redisnotelist, option);
                }
                return this.Ok(new { success = true, message = $"Get Note Successful", data = notelist });

               
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    } 
}
