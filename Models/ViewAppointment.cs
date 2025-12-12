namespace proje.Models
{
    public class ViewAppointment
    {
        public int Id { get; set; }  // db sorun çıkarmasın diye ekliyorum 
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public string CoachName { get; set; }

    }
}
