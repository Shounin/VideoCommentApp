using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoCommentApp.Models;

namespace VideoBlogApplication.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public String Username { get; set; }
        public String CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public List<Like> Likes { get; set; }
        public void ChangeLikes(String Username)
        {
            Like temp = new Like(Username);
            if(Likes.Contains(temp))
            {
                Likes.Remove(temp);
            }
            else
            {
                Likes.Add(temp);
            }
        }
    }
}