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
    public class StudentGroupService: IStudentGroupService
    {
        private IStudentGroupDataService _dataService;
        public StudentGroupService( StudentGroupDataService dataService)
        {
            this._dataService = dataService;
        }

        public List<int> GetAllStudentIdsAttachedToAGroup(int groupId)
        {
            return this._dataService.GetAllStudentIdsAttachedToAGroup(groupId);
        }

        public IEnumerable<StudentGroup> GetAllStudentGroups(int studentId)
        {
            var results = this._dataService.GetAllStudentGroups(studentId);
            //Mapper.CreateMap<ContextAwareAndRealTimeTimeTable.EF.Models.StudentGroup, ContextAwareAndRealTimeTimeTable.Models.StudentGroup>();
            //return Mapper.Map<IEnumerable<ContextAwareAndRealTimeTimeTable.EF.Models.StudentGroup>, IEnumerable<ContextAwareAndRealTimeTimeTable.Models.StudentGroup>>(results);
            var studentGroupList = MapEfStudentGroupObjectsToModelsStudentGroupObject(results.ToList());
            return studentGroupList;
        }

        public int SaveStudentGroup(StudentGroup studentGroup)
        {
            return this._dataService.SaveStudentGroup(studentGroup);
        }


        public IEnumerable<StudentGroup> MapEfStudentGroupObjectsToModelsStudentGroupObject(List<EF.Models.StudentGroup> studentGroupList)
        {
            var modelsStudentGroupList = new List<StudentGroup>();
            foreach (EF.Models.StudentGroup studentGroup in studentGroupList)
            {

                modelsStudentGroupList.Add(new ContextAwareAndRealTimeTimeTable.Models.StudentGroup()
                {
                    StudentGroupId = studentGroup.StudentGroupId,
                    StudentId = studentGroup.StudentId,
                    GroupId = studentGroup.GroupId

                });
            }

            return modelsStudentGroupList;
        }


    }
}
