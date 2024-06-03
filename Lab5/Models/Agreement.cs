using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public class Agreement
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Person")]
        public int PersonID { get; set; }
        public Person Person { get; set; }

        [ForeignKey("TypeAgreement")]
        public int TypeID { get; set; }
        public TypeAgreement TypeAgreement { get; set; }

        [ForeignKey("StatusAgreement")]
        public int StatusID { get; set; }
        public StatusAgreement StatusAgreement { get; set; }

        public string Number { get; set; } = "";
        public DateTime DataOpen { get; set; }
        public DateTime? DataClouse { get; set; }
        public string Note { get; set; } = "";
    }
}
