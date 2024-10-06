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

        public static User CurrentUser { get; private set; }

        public static async Task<User> SignUp(string displayName, string password)
        {
            var user = new User
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DisplayName = displayName,
                Password = password,
                BestScore = -1,
            };

            var existingDisplayNameUser = await collection.Find(u => u.DisplayName == displayName).FirstOrDefaultAsync();

            if(existingDisplayNameUser != null)
                throw new InvalidOperationException("Display name already taken.");

            await collection.InsertOneAsync(user);

            CurrentUser = user;

            return user;
        }

        public static async Task<User> SignIn(string displayName, string password)
        {
            var user = await collection.Find(u => u.DisplayName == displayName).FirstOrDefaultAsync();

            if (user == null || user.Password != password)
                throw new InvalidOperationException("Invalid username or password.");

            CurrentUser = user;

            return user; 
        }

        public static async void UpdateBestScore(int bestScore)
        {
            if(CurrentUser.BestScore == -1 || bestScore < CurrentUser.BestScore)
                CurrentUser.BestScore = bestScore;

            await collection.ReplaceOneAsync(u => u.Id == CurrentUser.Id, CurrentUser);
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