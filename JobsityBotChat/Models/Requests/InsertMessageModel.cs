using System;
using System.Collections.Generic;
namespace WebApi.Models.Requests
{
    public class InsertMessageModel
    {
        public string Text { get; set; }
        public string OwnerName { get; set; }
        public string ChatId { get; set; } = null;
    }
}
