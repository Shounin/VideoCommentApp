using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoCommentApp.Models
{
    public class Like
    {
        public String Username { get; set; }
        public Like( String Username)
        {
            this.Username = Username;
        }
    }
}