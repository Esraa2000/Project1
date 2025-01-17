namespace COC.Models
{
    public class PhotosVideoVM
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string ImageURl { get; set; }
        public string VideoURL { get; set; }
        public IFormFile fileupload { get; set; }

    }
}
