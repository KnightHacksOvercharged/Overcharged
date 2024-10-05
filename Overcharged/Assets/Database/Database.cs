using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Assets.Database
{
    public static class Database
    {
        private static readonly string connectionString = Secrets.MongoUri;
        private static readonly MongoClient client = new(connectionString);
        private static readonly IMongoDatabase database = client.GetDatabase("Overcharged");

        private static readonly IMongoCollection<User> collection = database.GetCollection<User>("Users");

        public static async void SignUp(string displayName, string email, string password)
        {
            var user = new User
            {
                DisplayName = displayName,
                Password = password
            };

            var existingDisplayNameUser = await collection.Find(u => u.DisplayName == displayName).FirstOrDefaultAsync() ?? throw new InvalidOperationException("A user with the same display name already exists.");

            await collection.InsertOneAsync(user);
        }

        public static async Task<User> SignIn(string displayName, string password)
        {
            var user = await collection.Find(u => u.DisplayName == displayName && u.Password == password).FirstOrDefaultAsync();
            return user;
        }

        public static async void UpdateBestScore(string userId, int bestScore)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
            var user = await collection.Find(filter).FirstOrDefaultAsync() ?? throw new InvalidOperationException("User not found.");
            var update = Builders<User>.Update.Set(u => u.BestScore, user.BestScore == -1 ? bestScore : Math.Min(user.BestScore, bestScore));

            await collection.UpdateOneAsync(filter, update);
        }

        public static async Task<List<(string DisplayName, int BestScore)>> GetDisplayNamesAndScoresAsync()
        {
            var projection = Builders<User>.Projection.Expression(u => new { u.DisplayName, u.BestScore });
            var users = await collection.Find(new BsonDocument())
                                        .Project(projection)
                                        .SortBy(u => u.BestScore)
                                        .ToListAsync();

            var result = new List<(string DisplayName, int BestScore)>();
            foreach (var user in users)
                if (user.BestScore != -1)
                    result.Add((user.DisplayName, user.BestScore));

            return result;
        }
    }
}