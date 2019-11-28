using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB
{
   public class PersonModel
    {
        [BsonId]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AdressModel PrimaryAdress { get; set; }
        [BsonElement("dob")]
        public DateTime DateOfBirth { get; set; }
    }
}
