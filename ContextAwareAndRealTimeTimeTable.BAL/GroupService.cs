using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContextAwareAndRealTimeTimeTable.Models;
using ContextAwareAndRealTimeTimeTable.DAL;
using AutoMapper;
using ContextAwareAndRealTimeTimeTable.Helpers;

namespace ContextAwareAndRealTimeTimeTable.BAL
{
    public class GroupService: IGroupService
    {
        private IGroupDataService _dataService;
        public GroupService( GroupDataService dataService)
        {
            this._dataService = dataService;
        }

        public List<Group> GetAllStudentSelectedGroups(List<StudentGroup> studentGroups)
        {
            List<Group> selectedGroups = new List<Group>();
            var allGroups = GetAllGroups();
            foreach (var x in studentGroups)
            {
                var Group = allGroups.AsQueryable().Where(c => c.GroupId == x.GroupId).FirstOrDefault();
                if (Group != null)
                {
                    selectedGroups.Add(Group);
                }
            }
            return selectedGroups;
        }


        public IEnumerable<Group> GetAllGroups()
        {
            var results = this._dataService.GetAllGroups();
            var groupList = MapEfGroupObjectsToModelsGroupObject(results.ToList());
            return groupList;
            //Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.Group, ContextAwareAndRealTimeTimeTable.Models.Group>();
            //return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.Group>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.Group>>(results);
        }

        public IEnumerable<Group> GetGroupsForSpecifiedYearsAndCourses(YearCourseGroup input)
        {
            var results = this._dataService.GetGroupsForSpecifiedYearsAndCourses(input);
            var groupList = MapEfGroupObjectsToModelsGroupObject(results.ToList());
            return groupList;
        }

        public Group GetGroup(int groupId)
        {
            var result = this._dataService.GetGroup(groupId);
            Course groupCourse = MappingHelper.MapEFCourseToModelCourse(result.Course);
            Year groupYear = MappingHelper.MapEFYearToModelYear(result.Year);
            Group resultGroup = new Group();
            resultGroup.Name = result.Name;
            resultGroup.GroupId = result.GroupId;        
            resultGroup.Course = groupCourse;
            resultGroup.Year = groupYear;            
            return resultGroup;
        }

        public IEnumerable<Group> MapEfGroupObjectsToModelsGroupObject(List<EF.Models.Group> GroupList)
        {
            var modelsGroupList = new List<Group>();
            foreach (EF.Models.Group group in GroupList)
            {
               
                var groupCourse = MappingHelper.MapEFCourseToModelCourse(group.Course);
                var groupYear = MappingHelper.MapEFYearToModelYear(group.Year);                

                modelsGroupList.Add(new ContextAwareAndRealTimeTimeTable.Models.Group()
                {
                    GroupId = group.GroupId,
                    Name =group.Name,
                    CourseId =group.CourseId,
                    Course = groupCourse,
                    Year = groupYear                  

                });
            }

            return modelsGroupList;
        }
    }
}
