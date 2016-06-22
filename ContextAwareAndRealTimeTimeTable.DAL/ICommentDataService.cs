using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using MD = ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public interface ICommentDataService
    {
        IEnumerable<Comment> GetAllComments();
        IEnumerable<Comment> GetAllActivityComments(int activityId);
        Comment GetComment(int commentId, int activityId);
        int SaveComment(MD.Comment comment);
        void DeleteComment(int commentId, int activityId);  
        IEnumerable<Comment> GetAllCommentsAssociatedWithAparticularAccount(string accountId);
    }
}
