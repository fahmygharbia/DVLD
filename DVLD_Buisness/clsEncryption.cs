using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public static class clsEncryption
    {
        public static string ComputeHashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] HashPassword = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

                return BitConverter.ToString(HashPassword).Replace("-", "");
            }
        }

    }
}
