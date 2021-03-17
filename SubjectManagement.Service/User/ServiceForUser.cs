using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SubjectManagement.Service.User
{
    public class ServiceForUser
    {
        public static string PasswordHash(string password)
        {
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
            var hashBytes = MD5.Create().ComputeHash(inputBytes);

            // Encode the byte array using Base64 encoding
            return Convert.ToBase64String(hashBytes);
        }

    }

}
