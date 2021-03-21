using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Helpers;
using Models;
using Microsoft.IdentityModel.Logging;
using Webapplication1.Data;

namespace Services
{
    public interface IUserService
    {
        Reservation Authenticate(string username, string SecretWord);
        IEnumerable<Reservation> GetAll();
        Reservation GetById(int id);
        Reservation Create(Reservation user, string SecretWord);
        void Update(Reservation user, string CurrentReserv_Name, string SecretWord, string ConfirmNewReserv_Name);
        void Delete(int id);
    }

    public class UserService : IUserService
    {
        private Context _context;

        public UserService(Context context)
        {
            _context = context;
        }

        public Reservation Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = _context.Reservation.FirstOrDefault(x => x.Username == username) ?? null;

            // check if username exists
            if (user == null)
            {
                return null;
            }

            // Granting access if the hashed password in the database matches with the password(hashed in computeHash method) entered by user.
            if(computeHash(password) != user.SecretWord)
            {
                return null;
            }
            return user;        
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _context.Reservation;
        }

        public Reservation GetById(int id)
        {
            return _context.Reservation.Find(id);
        }

        public Reservation Create(Reservation user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }

            if (_context.Reservation.Any(x => x.Username == user.Username))
            {
                throw new AppException("Username \"" + user.Username + "\" is already taken");
            }

            //Saving hashed password into Database table
            user.SecretWord = computeHash(password);  

            _context.Reservation.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(Reservation userParam, string currentPassword = null, string password = null, string confirmPassword = null)
        {
            //Find the user by Id
            var user = _context.Reservation.Find(userParam.Id);

            if (user == null) 
            {
                throw new AppException("User not found");
            }
            // update user properties if provided
            if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
            {
                // throw error if the new username is already taken
                if (_context.Reservation.Any(x => x.Username == userParam.Username))
                {
                    throw new AppException("Username " + userParam.Username + " is already taken");
                }
                else
                {
                    user.Username = userParam.Username;
                }
            }
            if (!string.IsNullOrWhiteSpace(userParam.FirstName))
            {
                user.FirstName = userParam.FirstName;
            }
            if (!string.IsNullOrWhiteSpace(userParam.LastName))
            {
                user.LastName = userParam.LastName;
            }
            if (!string.IsNullOrWhiteSpace(currentPassword))
            {   
                if(computeHash(currentPassword) != user.SecretWord)
                {
                    throw new AppException("Invalid Current SecretWord!");
                }

                if(currentPassword == password)
                {
                    throw new AppException("Please choose another SecretWord!");
                }

                if(password != confirmPassword)
                {
                    throw new AppException("SecretWord doesn't match!");
                }
    
                //Updating hashed password into Database table
                user.SecretWord = computeHash(password); 
            }
            
            _context.Reservation.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Reservation.Find(id);
            if (user != null)
            {
                _context.Reservation.Remove(user);
                _context.SaveChanges();
            }
        }

        private static string computeHash(string secretword)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var input = md5.ComputeHash(Encoding.UTF8.GetBytes(secretword));
            var hashstring = "";
            foreach(var hashbyte in input)
            {
                hashstring += hashbyte.ToString("x2"); 
            } 
            return hashstring;
        }
    }
}