using System.ComponentModel.DataAnnotations;

namespace travel.Models
{
    public class User
    {
        [Key]
        public int user_id
        {
            get; set;
        }
        [Required(ErrorMessage ="Name field is required")]
        public string user_name
        {
        get; set; }
        [Required(ErrorMessage ="Email field is required")]
        public string user_email
        {
            get; set;
        }
        [Required(ErrorMessage ="Password field is required")]
        public string password
        {
            get; set;
        }
        public string profile_pic
        {
            get; set;
        }
        public int role_type
        {
            get; set;
        }
    }
}
