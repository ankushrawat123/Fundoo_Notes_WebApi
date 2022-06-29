using BuisnessLayer.Interface;
using DatabaseLayer.User;
using Experimental.System.Messaging;
using RepositoryLayer.Interfaces;

using RepositoryLayer.Services;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public void AddUser(UserPostModel userPostModel)
        {
            try
            {
                userRL.AddUser(userPostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ForgetPassword(string Email)
        {

            try
            {
                return this.userRL.ForgetPassword(Email);
            }

            catch (Exception e)
            {
                throw e;
            }
        }

        public string LogInUser(string Email, string Password)
        {
            try
            {
                return this.userRL.LogInUser(Email, Password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool ResetPassword(string email, UserPasswordModel userPasswordModel)
        {
            try
            {
                return this.userRL.ResetPassword(email, userPasswordModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}


