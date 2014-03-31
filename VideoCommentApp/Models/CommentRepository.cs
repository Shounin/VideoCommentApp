using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoCommentApp.Models;

namespace VideoBlogApplication.Models
{
    public class CommentRepository
    {
        private static CommentRepository _instance;

        public static CommentRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CommentRepository();
                return _instance;
            }
        }

        private List<Comment> m_comments = null;

        private CommentRepository()
        {
            this.m_comments = new List<Comment>();
            Comment commment1 = new Comment { ID = 1, CommentText = "Great Video!", CommentDate = new DateTime(2014, 3, 1, 12, 30, 00), Username = "Patrekur", Likes = new List<Like>() };
            Comment commment2 = new Comment { ID = 2, CommentText = "Amazing content!", CommentDate = new DateTime(2014, 3, 5, 12, 30, 00), Username = "Siggi", Likes = new List<Like>() };
            Like temp = new Like { Username = "Rassapi", ID = 1 };
            commment1.ChangeLikes(temp);
            this.m_comments.Add(commment1);
            this.m_comments.Add(commment2);
            
        }

        public IEnumerable<Comment> GetComments()
        {
            var result = from c in m_comments
                         orderby c.CommentDate ascending
                         select c;
            return result;
        }

        public void AddComment(Comment c)
        {
            int newID = 1;
            if (m_comments.Count() > 0)
            {
                newID = m_comments.Max(x => x.ID) + 1;
            }
            c.ID = newID;
            c.CommentDate = DateTime.Now;
            c.Likes = new List<Like>();
            m_comments.Add(c);
        }
        public void LikeManip(Like li)
        {
            var result = m_comments.Find(p => p.ID == li.ID);
            result.ChangeLikes(li);
        }
    }
}