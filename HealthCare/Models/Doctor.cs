namespace HealthCare.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string DoctorName { get; set; }
        public string Specialization { get; set; }
        public User user { get; set; }
        public int UserId { get; set; }
    }
}
