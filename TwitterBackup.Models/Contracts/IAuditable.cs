using System;

namespace TwitterBackup.Models.Contracts
{
    public interface IAuditable
    {
        DateTime? SavedOn { get; set; }

        DateTime? ModifiedOn { get; set; }

    }
}
