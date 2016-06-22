using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;
using ContextAwareAndRealTimeTimeTable.DAL;
using AutoMapper;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public class CommentService: ICommentService
    {
        private ICommentDataService _dataService;
        public CommentService( CommentDataService dataService)
        {
            this._dataService = dataService;
        }

        public IEnumerable<Comment> GetAllComments()
        {
            var results = this._dataService.GetAllComments();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Comment, ContextAwareAndRealTimeTimeTable.Models.Comment>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Comment>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Comment>>(results);
        }

        public IEnumerable<Comment> GetAllActivityComments(int activityId)
        {
            var comments = this._dataService.GetAllActivityComments(activityId);

            var commentList = new List<Comment>();
            foreach (EF.Models.Comment c in comments)
            {
                commentList.Add(new ContextAwareAndRealTimeTimeTable.Models.Comment()
                {
                    Description = c.Description,
                    CommentId = c.CommentId,
                    CreatedOn = c.CreatedOn,
                    ActivityId = c.ActivityId,
                    CreatedBy = c.CreatedBy
                });
            }
            return commentList;
        }

        public IEnumerable<Comment> GetAllCommentsAssociatedWithAparticularAccount(string accountId)
        {
            var results = _dataService.GetAllCommentsAssociatedWithAparticularAccount(accountId);
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Comment, ContextAwareAndRealTimeTimeTable.Models.Comment>();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Activity, ContextAwareAndRealTimeTimeTable.Models.Activity>();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Room, ContextAwareAndRealTimeTimeTable.Models.Room>();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Lecturer, ContextAwareAndRealTimeTimeTable.Models.Lecturer>();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Subject, ContextAwareAndRealTimeTimeTable.Models.Subject>();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Group, ContextAwareAndRealTimeTimeTable.Models.Group>();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Day, ContextAwareAndRealTimeTimeTable.Models.Day>();
            Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.AspNetUser, ContextAwareAndRealTimeTimeTable.Models.AspNetUser>();
            return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Comment>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Comment>>(results);
        }

        

        public Comment GetComment(int commentId, int activityId)
        {
            var result = this._dataService.GetComment(commentId, activityId);
            Comment comment = new Comment();
             AspNetUser x = new ContextAwareAndRealTimeTimeTable.Models.AspNetUser();
            if (result != null)
            {
                if(result.AspNetUser != null){
                   {
                        x.FirstName = result.AspNetUser.FirstName;
                        x.LastName = result.AspNetUser.LastName;
                        x.Id = result.AspNetUser.Id;
                    };
                }
                comment.CreatedOn = result.CreatedOn;
                comment.ActivityId = result.ActivityId;
                comment.Description = result.Description;
                comment.AspNetUser = x;
            }
            return comment;           
        }

        public int SaveComment(Comment comment)
        {
            return this._dataService.SaveComment(comment);
        }

        public void DeleteComment(int commentId, int MessageId)
        {
            this._dataService.DeleteComment(commentId, MessageId);
        }

      
    }
}
