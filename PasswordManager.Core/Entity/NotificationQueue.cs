using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Core.Entity
{
    public  class NotificationQueue
    {
        [Key]
        public int NotificationQueueID { get; set; }
        public bool IsSent { get; set; }
        public int UserID { get; set; }
        public int CompanyID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? SentDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
