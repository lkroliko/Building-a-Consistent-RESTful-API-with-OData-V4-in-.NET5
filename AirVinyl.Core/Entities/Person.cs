using AirVinyl.Core.ValueObjects;
using AirVinyl.SharedKernel;
using System;
using System.Collections.Generic;

namespace AirVinyl.Entities
{
    public class Person : BaseEntity
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public int NumberOfRecordsOnWishList { get; set; }
        public decimal AmountOfCashToSpend { get; set; }
        public virtual ICollection<VinylRecord> VinylRecords { get; set; } = new List<VinylRecord>();
        public virtual ICollection<Person> Friends { get; set; } = new List<Person>();
    }
}
