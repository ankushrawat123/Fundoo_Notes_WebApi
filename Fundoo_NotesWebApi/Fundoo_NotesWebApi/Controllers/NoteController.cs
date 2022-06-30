﻿using BuisnessLayer.Interface;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Services;
using System;
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

        public NoteController(INoteBL noteBL , FundooContext fundooContext)
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
            catch(Exception e)
            {
                throw e;
            }
        }
       
    }
}
