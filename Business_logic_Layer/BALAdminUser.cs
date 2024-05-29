using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_logic_Layer
{
    public class BALAdminUser
    {
        private readonly DALAdminUser _dALAdminUser;
        public BALAdminUser(DALAdminUser dALAdminUser)
        {
            _dALAdminUser = dALAdminUser;
        }

        public List<UserDetail> UserDetailList()
        {
            return _dALAdminUser.UserDetailList();
        }

        public string DeleteUserAndUserDetail(int userId)
        {
            return _dALAdminUser.DeleteUserAndUserDetail(userId);
        }
    }
}
