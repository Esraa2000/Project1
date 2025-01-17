namespace COC.Models
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;
    
    public class SubMenuVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> Order { get; set; }
        public string Content { get; set; }
        public string URL { get; set; }
        public string Title { get; set; }
        public Nullable<int> MainMenuID { get; set; }

       public SelectList MainMenuList { get; set; }
    }
}
