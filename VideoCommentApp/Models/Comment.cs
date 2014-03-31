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
        //we decided the simplest way to do this without a database would be to let comments have likes.
        public List<Like> Likes { get; set; }
        public string LStatus { get; set; }
        //Adds/Removes them likes.
        public void ChangeLikes(Like li)
        {
            foreach (var item in Likes)
            {
                if (item.Username == li.Username)
                {
                    Likes.Remove(item);
                    return;
                }
            }
            Likes.Add(li);
        }
        //An extremelly round-about way to make the like button change
        public string HasLiked(string user)
        {
            foreach (var item in Likes)
            {
                if (item.Username == user)
                {
                    return "Unlike";
                }
            }
            return "Like";
        }
    }
}