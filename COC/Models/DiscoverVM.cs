using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COC.Models
{
    
    public class DiscoverVM
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public IFormFile fileupload { get; set; }
    }
}