using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using UnityEngine;

namespace Assets.Database
{
    public static class Database
    {
        private static readonly string connectionString = Secrets.MongoUri;
        private static readonly MongoClient client = new(connectionString);
        private static readonly IMongoDatabase database = client.GetDatabase("Overcharged");

        public static async Task<List<User>> GetUsersAsync()
        {
            var collection = database.GetCollection<User>("Users");
            var users = await collection.Find(new BsonDocument()).ToListAsync();
            return users;
        }

        public static async Task CreateUserAsync(string displayName, string email, string password)
        {
            var user = new User
            {
                DisplayName = displayName,
                Email = email,
                Password = password
            };

            var collection = database.GetCollection<User>("Users");

            var existingDisplayNameUser = await collection.Find(u => u.DisplayName == displayName).FirstOrDefaultAsync();
            if (existingDisplayNameUser != null)
                throw new InvalidOperationException("A user with the same display name already exists.");
                
            var existingEmailUser = await collection.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (existingEmailUser != null)
                throw new InvalidOperationException("A user with the same email already exists.");

            await collection.InsertOneAsync(user);
        }
    }
}