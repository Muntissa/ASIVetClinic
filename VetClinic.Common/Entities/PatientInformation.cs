namespace VetClinic.Common.Entities
{
    public class PatientInformation
    {
        public int RecordAnimalId { get; set; }
        public RecordAnimal RecordAnimal { get; set; }
        public int HospitalId { get; set; }
        public Hospital Hospital { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
