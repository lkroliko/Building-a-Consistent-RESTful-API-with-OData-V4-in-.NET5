using AirVinyl.Core.ValueObjects;
using AirVinyl.Entities;
using AirVinyl.Infrastructure;
using System;
using System.Collections.Generic;

namespace AirVinyl.WebApi
{
    public static class SeedData
    {
        public static void Seed(AirVinylDbContext context)
        {
            PressingDetail[] pressingDetails = SeedData.GetPressingDetails();
            Person[] people = SeedData.GetPeople();
            SeedData.AddVinylRecords(people, pressingDetails);
            RecordStore[] recordStores = SeedData.GetRecordStores();
            SeedData.AddRatings(recordStores, people);

            context.PressingDetails.AddRange(pressingDetails);
            context.People.AddRange(people);
            context.RecordStores.AddRange(recordStores);

            context.SaveChanges();
        }

        private static PressingDetail[] GetPressingDetails()
        {
            return new PressingDetail[]
            {
                 new PressingDetail()
                {
                    Description = "Audiophile LP",
                    Grams = 180,
                    Inches = 12
                },
                new PressingDetail()
                {
                    Description = "Regular LP",
                    Grams = 140,
                    Inches = 12
                },
                new PressingDetail()
                {
                    Description = "Audiophile Single",
                    Grams = 50,
                    Inches = 7
                },
                new PressingDetail()
                {
                    Description = "Regular Single",
                    Grams = 40,
                    Inches = 7
                }
            };
        }

        private static Person[] GetPeople()
        {
            return new Person[]
            {
                new Person()
                {
                    DateOfBirth = new DateTimeOffset(new DateTime(1981, 5, 5)),
                    Email = "kevin@kevindockx.com",
                    FirstName = "Kevin",
                    LastName = "Dockx",
                    Gender = Gender.Male,
                    NumberOfRecordsOnWishList = 10,
                    AmountOfCashToSpend = 300
                },
                new Person()
                {
                    DateOfBirth = new DateTimeOffset(new DateTime(1986, 3, 6)),
                    Email = "sven@someemailprovider.com",
                    FirstName = "Sven",
                    LastName = "Vercauteren",
                    Gender = Gender.Male,
                    NumberOfRecordsOnWishList = 34,
                    AmountOfCashToSpend = 2000
                },
                new Person()
                {
                    DateOfBirth = new DateTimeOffset(new DateTime(1977, 12, 27)),
                    Email = "nele@someemailprovider.com",
                    FirstName = "Nele",
                    LastName = "Verheyen",
                    Gender = Gender.Female,
                    NumberOfRecordsOnWishList = 120,
                    AmountOfCashToSpend = 100
                },
                new Person()
                {
                    DateOfBirth = new DateTimeOffset(new DateTime(1983, 5, 18)),
                    Email = "nils@someemailprovider.com",
                    FirstName = "Nils",
                    LastName = "Missorten",
                    Gender = Gender.Male,
                    NumberOfRecordsOnWishList = 23,
                    AmountOfCashToSpend = 2500
                },
                new Person()
                {
                    DateOfBirth = new DateTimeOffset(new DateTime(1981, 10, 15)),
                    Email = "tim@someemailprovider.com",
                    FirstName = "Tim",
                    LastName = "Van den Broeck",
                    Gender = Gender.Male,
                    NumberOfRecordsOnWishList = 19,
                    AmountOfCashToSpend = 90
                },
                new Person()
                {
                    DateOfBirth = new DateTimeOffset(new DateTime(1981, 1, 16)),
                    Email = null,
                    FirstName = "Kenneth",
                    LastName = "Mills",
                    Gender = Gender.Male,
                    NumberOfRecordsOnWishList = 98,
                    AmountOfCashToSpend = 200
                }
            };
        }

        private static void AddVinylRecords(Person[] people, PressingDetail[] pressingDetails)
        {
            people[0].VinylRecords.Add(
                 new VinylRecord()
                 {
                     Artist = "Nirvana",
                     Title = "Nevermind",
                     CatalogNumber = "ABC/111",
                     PressingDetail = pressingDetails[0],
                     Person = people[0],
                     Year = 1991
                 });

            people[0].VinylRecords.Add(
            new VinylRecord()
            {

                Artist = "Arctic Monkeys",
                Title = "AM",
                CatalogNumber = "EUI/111",
                PressingDetail = pressingDetails[1],
                Person = people[0],
                Year = 2013
            });

            people[0].VinylRecords.Add(
            new VinylRecord()
            {
                Artist = "Beatles",
                Title = "The White Album",
                CatalogNumber = "DEI/113",
                PressingDetail = pressingDetails[1],
                Person = people[0],
                Year = 1968
            });

            people[0].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Beatles",
                    Title = "Sergeant Pepper's Lonely Hearts Club Band",
                    CatalogNumber = "DPI/123",
                    PressingDetail = pressingDetails[1],
                    Person = people[0],
                    Year = 1967
                });

            people[0].VinylRecords.Add(
            new VinylRecord()
            {
                Artist = "Nirvana",
                Title = "Bleach",
                CatalogNumber = "DPI/123",
                PressingDetail = pressingDetails[0],
                Person = people[0],
                Year = 1989
            });

            people[0].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Leonard Cohen",
                    Title = "Suzanne",
                    CatalogNumber = "PPP/783",
                    PressingDetail = pressingDetails[2],
                    Person = people[0],
                    Year = 1967
                });

            people[0].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Marvin Gaye",
                    Title = "What's Going On",
                    CatalogNumber = "MVG/445",
                    PressingDetail = pressingDetails[0],
                    Person = people[0],
                    Year = null
                });

            people[1].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Nirvana",
                    Title = "Nevermind",
                    CatalogNumber = "ABC/111",
                    PressingDetail = pressingDetails[0],
                    Person = people[1],
                    Year = 1991
                });

            people[1].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Cher",
                    Title = "Closer to the Truth",
                    CatalogNumber = "CHE/190",
                    PressingDetail = pressingDetails[1],
                    Person = people[1],
                    Year = 2013
                });

            people[2].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "The Dandy Warhols",
                    Title = "Thirteen Tales From Urban Bohemia",
                    CatalogNumber = "TDW/516",
                    PressingDetail = pressingDetails[1],
                    Person = people[2],
                });

            people[3].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Justin Bieber",
                    Title = "Baby",
                    CatalogNumber = "OOP/098",
                    PressingDetail = pressingDetails[2],
                    Person = people[3],
                });

            people[3].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "The Prodigy",
                    Title = "Music for the Jilted Generation",
                    CatalogNumber = "NBE/864",
                    PressingDetail = pressingDetails[1],
                    Person = people[3],
                });

            people[4].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Anne Clarke",
                    Title = "Our Darkness",
                    CatalogNumber = "TII/339",
                    PressingDetail = pressingDetails[2],
                    Person = people[4],
                });

            people[4].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Dead Kennedys",
                    Title = "Give Me Convenience or Give Me Death",
                    CatalogNumber = "DKE/864",
                    PressingDetail = pressingDetails[1],
                    Person = people[4],
                });

            people[4].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Sisters of Mercy",
                    Title = "Temple of Love",
                    CatalogNumber = "IIE/824",
                    PressingDetail = pressingDetails[3],
                    Person = people[4],
                });

            people[5].VinylRecords.Add(
                new VinylRecord()
                {
                    Artist = "Abba",
                    Title = "Gimme Gimme Gimme",
                    CatalogNumber = "TDW/516",
                    PressingDetail = pressingDetails[3],
                    Person = people[5],
                });
        }

        private static RecordStore[] GetRecordStores()
        {
            return new RecordStore[]
            {
                new RecordStore()
                {
                    Name = "All Your Music Needs",
                    //Tags = new List<string>() { "Rock", "Pop", "Indie", "Alternative" },
                    StoreAddress = new Address()
                    {
                        City = "Antwerp",
                        PostalCode = "2000",
                        Street = "25, Fluffy Road",
                        Country = "Belgium"
                    }
                }
            };
        }

        private static void AddRatings(RecordStore[] recordStores, Person[] people)
        {
            recordStores[0].Ratings.Add(
            new Rating()
            {
                RatedBy = people[0],
                Value = 4
            });

            recordStores[0].Ratings.Add(
                new Rating()
                {
                    RatedBy = people[1],
                    Value = 4
                });

            recordStores[0].Ratings.Add(
                new Rating()
                {
                    RatedBy = people[2],
                    Value = 4
                });

            //recordStores[0].Ratings.Add(
            //    new Rating()
            //    {
            //        RatingId = 4,
            //        RecordStoreId = 2,
            //        RatedByPersonId = 1,
            //        Value = 5
            //    });

            //recordStores[0].Ratings.Add(
            //    new Rating()
            //    {
            //        RatingId = 5,
            //        RecordStoreId = 2,
            //        RatedByPersonId = 2,
            //        Value = 4
            //    });

            //recordStores[0].Ratings.Add(
            //    new Rating()
            //    {
            //        RatingId = 6,
            //        RecordStoreId = 3,
            //        RatedByPersonId = 3,
            //        Value = 5
            //    });

            //recordStores[0].Ratings.Add(
            //    new Rating()
            //    {
            //        RatingId = 7,
            //        RecordStoreId = 3,
            //        RatedByPersonId = 2,
            //        Value = 4
            //    });
        }
    }
}
