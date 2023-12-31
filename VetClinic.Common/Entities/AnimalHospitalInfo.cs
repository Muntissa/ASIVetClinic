﻿using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class AnimalHospitalInfo
    {
        public int Id { get; set; }

        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }

        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата приема")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата выписки")]
        public DateTime? EndDate { get; set; }
    }
}
