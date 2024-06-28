using System;

namespace WebinarManagement.Models
{
    public class Participant
    {
        public int ParticipantID { get; set; }
        public int WebinarID { get; set; }
        public int UserID { get; set; }

        public virtual Webinar Webinar { get; set; }
        public virtual User User { get; set; }
    }
}
