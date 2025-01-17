namespace COC.Models
{
    public class newstest
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile FileUpload { get; set; }
        public Nullable<bool> Highlight { get; set; }
    }
}
