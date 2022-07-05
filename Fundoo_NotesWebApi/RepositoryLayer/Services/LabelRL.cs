﻿using Microsoft.EntityFrameworkCore;
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
    public class LabelRL : ILabelRL
    {
        FundooContext fundooContext;
        IConfiguration configuration;

        public LabelRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;

        }

        public async Task AddLabel(int userid, int noteid, string labelName)
        {
            try
            {
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserId == userid && u.noteId == noteid);
                if (note != null)
                {
                    Label label = new Label();
                    label.UserId = userid;
                    label.NoteId = noteid;
                    label.LabelName = labelName;
                 
                     fundooContext.Labels.Add(label);
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteLabel(int userid, int noteid)
        {
            try
            {
                var label = fundooContext.Labels.Where(u=>u.UserId== userid && u.NoteId==noteid).FirstOrDefault();
                if(label != null)
                {
                    fundooContext.Labels.Remove(label);
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<Label> GetLabel(int userid, int noteId)
        {
            try
            {
               return await fundooContext.Labels.FirstOrDefaultAsync(u => u.UserId == userid && u.NoteId == noteId);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateLabel(int userid, int noteId, string labelName)
        {
           try
            {
                var label = fundooContext.Labels.Where(u => u.NoteId == noteId).FirstOrDefault();
                if(label!= null)
                {
                 
                    label.LabelName = labelName;
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
