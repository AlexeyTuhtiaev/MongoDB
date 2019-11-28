using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDB
{
    public class MongoCruid
    {
        private IMongoDatabase db;
        public MongoCruid(string databaseName)
        {
            var client = new MongoClient();
            db = client.GetDatabase(databaseName);
        }

        public void InsertRecord<T>(string table, T record)
        {
            var collection = db.GetCollection<T>(table);
            collection.InsertOne(record);
        }

        public List<T> GetRecords<T>(string tableName)
        {
            return db.GetCollection<T>(tableName).Find(new BsonDocument()).ToList();
        }

        public T GetRecordById<T>(string tableName, Guid id)
        {
            var collection = db.GetCollection<T>(tableName);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public void UpsetRecord<T>(string tableName, Guid id, T record)
        {
            var collection = db.GetCollection<T>(tableName);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id), 
                record,
                new UpdateOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string tableName, Guid id)
        {
            var collection = db.GetCollection<T>(tableName);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}
