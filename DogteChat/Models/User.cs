using System;
using System.Collections.Generic;
using System.Text;

namespace DogteChat.Models
{
    public class User
    {
        public int Id { get; set; }
        public int TelegramId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public int? Steam32Id { get; set; }
        public DateTime SignupDate { get; set; }
    }
}
