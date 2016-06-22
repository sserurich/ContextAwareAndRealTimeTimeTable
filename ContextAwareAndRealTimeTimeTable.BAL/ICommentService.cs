using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public interface ICommentService
    {
        IEnumerable<Comment> GetAllActivityComments(int activityId);
        Comment GetComment(int commentId, int activityId);
        int SaveComment(Comment comment);
        void DeleteComment(int commentId, int activityId);
        IEnumerable<Comment> GetAllCommentsAssociatedWithAparticularAccount(string accountId);
    }
}
