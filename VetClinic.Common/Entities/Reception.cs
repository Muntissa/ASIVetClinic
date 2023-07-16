using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VetClinic.Common.Entities
{
    public class Reception
    {
        public enum State
        {
            Waiting,
            InHospital,
            TreatmentCompleted
        }

        public int Id { get; set; }

        [Display(Name = "Состояние")]
        public State TreatmentState { get; set; }

        [MaxLength(50)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }

        [Display(Name = "Владелец")]
        public int OwnerId { get; set; }
        public Owner? Owner { get; set; }

        [Display(Name = "Животное")]
        public int AnimalId { get; set; }
        public Animal? Animal { get; set; }

        [Display(Name = "Врач")]
        public string EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Дата и время")]
        public DateTime Date { get; set; }


        public List<Diagnosis> Diagnoses { get; set; } = new();
        public List<Service> Services { get; set; } = new();
        public List<Drug> Drugs { get; set; } = new();

        [DataType(DataType.Currency)]
        [Precision(10, 2)]
        [Display(Name = "Сумма к оплате")]
        public decimal Price { get; set; }
    }
}
