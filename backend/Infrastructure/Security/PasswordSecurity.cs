using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using BCrypt;

// hashing accomplished using Bcrypt for dotnet core
// https://github.com/neoKushan/BCrypt.Net-Core
namespace backend.Infrastructure.PasswordSecurity
{
    public static class PasswordSecurity
    {
        // private const int SaltByteSize = 128 /;
        public static string HashPassword(string plaintextPassword) =>
            BCrypt.Net.BCrypt.HashPassword(plaintextPassword);
    
        public static bool CompareHashedPasswords(string suppliedPlaintextPassword, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(suppliedPlaintextPassword, hashedPassword);

    }
}