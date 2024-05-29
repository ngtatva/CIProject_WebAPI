using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class DALAdminUser
    {
        private readonly AppDbContext _appDbContext;
        public DALAdminUser(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<UserDetail> UserDetailList()
        {
            try
            {
                var userDetails = (from u in _appDbContext.User
                                   join ud in _appDbContext.UserDetail on u.Id equals ud.UserId into UserDetailGroup
                                   from userdetail in UserDetailGroup.DefaultIfEmpty()
                                   where !u.IsDeleted && !userdetail.IsDeleted && u.UserType == "user"
                                   select new UserDetail
                                   {
                                       Id = u.Id,
                                       FirstName = u.FirstName,
                                       LastName = u.LastName,
                                       PhoneNumber = u.PhoneNumber,
                                       EmailAddress = u.EmailAddress,
                                       UserType = u.UserType,
                                       UserId = userdetail.Id,
                                       Name = userdetail.Name,
                                       Surname = userdetail.Surname,
                                       EmployeeId = userdetail.EmployeeId,
                                       Department = userdetail.Department,
                                       Title = userdetail.Title,
                                       Manager = userdetail.Manager,
                                       WhyIVolunteer = userdetail.WhyIVolunteer,
                                       CountryId = userdetail.CountryId,
                                       CityId = userdetail.CityId,
                                       Avilability = userdetail.Avilability,
                                       LinkdInUrl = userdetail.LinkdInUrl,
                                       MySkills = userdetail.MySkills,
                                       UserImage = userdetail.UserImage,
                                       Status = userdetail.Status,
                                   }).ToList();

                return userDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteUserAndUserDetail(int userId)
        {
            try
            {
                string result = "";

                using (var transaction = _appDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userDetail = _appDbContext.UserDetail.FirstOrDefault(ud => ud.Id == userId);
                        if (userDetail != null)
                        {
                            userDetail.IsDeleted = true;

                            var user = _appDbContext.User.FirstOrDefault(u => u.Id == userDetail.UserId);
                            if (user != null)
                            {
                                user.IsDeleted = true;
                            }
                        }

                        _appDbContext.SaveChanges();

                        transaction.Commit();

                        result = "Delete User Successfully.";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
