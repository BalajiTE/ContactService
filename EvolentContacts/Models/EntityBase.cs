using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvolentContacts.Models
{
    public abstract class EntityBase
    {
        public int ID { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [DefaultValue(true)]
        public bool Status { get; set; }
    }
}