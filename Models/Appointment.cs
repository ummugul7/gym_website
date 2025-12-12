namespace proje.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string? MemberId { get; set; }
        public int CoachId { get; set; }
        public bool IsBooked { get; set; } = false;
    }

    public class AppointmentGroupVM
    {
        public DateTime Date { get; set; }
        public List<AppointmentTimeVM> Times { get; set; }
    }

    public class AppointmentTimeVM
    {
        public int AppointmentId { get; set; }
        public TimeSpan Time { get; set; }
    }

}
