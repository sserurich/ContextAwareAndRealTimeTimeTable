using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;
using ContextAwareAndRealTimeTimeTable.EF.Models;
using MD = ContextAwareAndRealTimeTimeTable.Models;

namespace ContextAwareAndRealTimeTimeTable.DAL
{
    public class CommentDataService:DataServiceBase, ICommentDataService
    {
         public CommentDataService(IUnitOfWork<ContextAwareAndRealTimeTimeTableEntities> unitOfWork)
            : base(unitOfWork)
        {

        }

         public IEnumerable<Comment> GetAllComments()
         {
             return this.UnitOfWork.Get<Comment>().AsQueryable()
                    .Where(y => y.DeletedOn == null);
         }

         public IEnumerable<Comment> GetAllActivityComments(int activityId)
         {
             return this.UnitOfWork.Get<Comment>().AsQueryable()
                 .Where(c => c.ActivityId == activityId);
         }

         public IEnumerable<Comment> GetAllCommentsAssociatedWithAparticularAccount(string accountId)
         {
             return this.UnitOfWork.Get<Comment>().AsQueryable()
                .Where(c => c.CreatedBy == accountId);
         }

         public Comment GetComment(int commentId, int activityId)
         {
             return this.UnitOfWork.Get<Comment>().AsQueryable()
                 .FirstOrDefault(c => c.CommentId == commentId && c.ActivityId == activityId);
         }

         public int SaveComment(MD.Comment comment)
         {
             if (comment.CommentId == 0)
             {
                 var c = new Comment()
                 {
                     Description = comment.Description,
                     ActivityId = comment.ActivityId,
                     CreatedBy = comment.CreatedBy,
                     CreatedOn = DateTime.Now
                 };
                 this.UnitOfWork.Get<Comment>().AddNew(c);
                 this.UnitOfWork.SaveChanges();
                 return c.CommentId;
             }
             else
             {
                 var c = this.UnitOfWork.Get<Comment>().AsQueryable()
                         .FirstOrDefault(d => d.CommentId == comment.CommentId && d.ActivityId== comment.ActivityId);
                 if (c != null)
                 {
                     c.Description = comment.Description;
                     c.CreatedBy = comment.CreatedBy;
                     c.ActivityId = comment.ActivityId;
                     c.UpdatedOn = DateTime.Now;
                     this.UnitOfWork.Get<Comment>().Update(c);
                     this.UnitOfWork.SaveChanges();
                 }
                 return comment.CommentId;
             }
         }

         public void DeleteComment(int commentId, int activityId)
         {
             var c = this.UnitOfWork.Get<Comment>().AsQueryable()
                     .FirstOrDefault(d => d.CommentId == commentId && d.ActivityId == activityId);
             if (c != null)
             {
                 this.UnitOfWork.Get<Comment>().Delete(c);
                 this.UnitOfWork.SaveChanges();
             }
         }

    }
}
