﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Models
{
    public class StatusAgreement
    {
        [Key]
        public int ID { get; set; }

        public string Status { get; set; } = "";

        // Связь один-ко-многим с таблицей Agreement
        public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();
    }
}
