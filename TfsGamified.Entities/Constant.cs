using System;

namespace TfsGamified.Entities
{

    // Constants
    
    public class Constant
    {
        public int PartitionId { get; set; } // PartitionId (Primary key via unique index PK_Constants)
        public int ConstId { get; set; } // ConstID
        public string DomainPart { get; set; } // DomainPart (length: 256)
        public bool FInTrustedDomain { get; set; } // fInTrustedDomain
        public string NamePart { get; set; } // NamePart (length: 256)
        public string DisplayPart { get; set; } // DisplayPart (length: 256)
        public string String { get; private set; } // String (length: 513)
        public int ChangerId { get; set; } // ChangerID
        public DateTime AddedDate { get; set; } // AddedDate
        public DateTime RemovedDate { get; set; } // RemovedDate

        public string IdentityDisplayName { get; private set; } // IdentityDisplayName 

        public byte[] ImageData { get; set; }

        public Constant()
        {
            DomainPart = "";
            FInTrustedDomain = false;
            NamePart = "";
            ChangerId = 0;
        }
    }

}
// </auto-generated>
