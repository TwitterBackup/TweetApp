using System;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Models.Contracts;

namespace TwitterBackup.Models.Abstracts
{
    public abstract class DataModel : IAuditable, IDeletable
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? SavedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? ModifiedOn { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DeletedOn { get; set; }
    }
}
