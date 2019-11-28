using System;

namespace MongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoCruid db = new MongoCruid("PhoneBook");

            //var person = new PersonModel
            //{
            //    FirstName = "Olga",
            //    LastName = "Petrova",
            //    PrimaryAdress = new AdressModel
            //    {
            //        StreetAdress="Pobeditelei pr, 37-10",
            //        City = "Minsk",
            //        ZipCode = "218232"
            //    }
            //};

            //db.InsertRecord("Users", new PersonModel { FirstName = "Alexey", LastName = "Dev"});
            //db.InsertRecord("Users", person);

            var users = db.GetRecords<PersonModel>("Users");

            users.ForEach(user =>
            {
                Console.WriteLine($"{user.Id}: {user.FirstName} {user.LastName} " +
                    $"{(user.PrimaryAdress != null ? $"from {user.PrimaryAdress.City}" : "")}");
            });
            Console.WriteLine("------------------------------");

            var userById = db.GetRecordById<PersonModel>("Users", new Guid("fc62abc6-de18-496a-b06c-e60dba881eeb"));
            Console.WriteLine($"fc62abc6-de18-496a-b06c-e60dba881eeb = { userById.Id} | {userById} | {userById.DateOfBirth}");
            userById.DateOfBirth = new DateTime(2010, 10, 12, 0, 0, 0, DateTimeKind.Utc);
            db.UpsetRecord("Users", userById.Id, userById);
            var updatedUserById = db.GetRecordById<PersonModel>("Users", new Guid("fc62abc6-de18-496a-b06c-e60dba881eeb"));
            Console.WriteLine($"fc62abc6-de18-496a-b06c-e60dba881eeb = { updatedUserById.Id} | {updatedUserById} | {updatedUserById.DateOfBirth}");

            Console.ReadKey();
        }
    }
}
