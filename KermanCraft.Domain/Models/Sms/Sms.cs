using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace KermanCraft.Domain.Models.Sms
{
    public class Sms
    {
        [Key]
        public int SmsId { get; set; }
        public string PhoneNumber { get; set; }
        public int Code { get; set; }
    }
}
