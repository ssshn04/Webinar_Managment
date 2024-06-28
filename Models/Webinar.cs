using System.ComponentModel.DataAnnotations;

namespace WebinarManagement.Models
{
    public class Webinar
    {
        public int WebinarID { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Short Description is required")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ImageUrl { get; set; }

        [Required(ErrorMessage = "Start Date and Time are required")]
        public DateTime StartDateTime { get; set; }

        [Required(ErrorMessage = "End Date and Time are required")]
        public DateTime EndDateTime { get; set; }

        [Required(ErrorMessage = "Speaker ID is required")]
        public int SpeakerID { get; set; }
        public virtual User Speaker { get; set; }
        public virtual ICollection<Participant> Participants { get; set; }
    }
}
