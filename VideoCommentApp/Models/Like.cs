using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VideoCommentApp.Models
{
    public class Like
    {
        //The ID of the comment its attached to
        public int ID { get; set; }
        //The user that gave it
        public String Username { get; set; }
    }
}