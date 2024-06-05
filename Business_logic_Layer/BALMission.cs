using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;

namespace Business_logic_Layer
{
    public class BALMission
    {
        private readonly DALMission _dalMission;     
        public BALMission(DALMission dalMission)
        {
            _dalMission = dalMission;
        }
        public List<DropDown> GetMissionThemeList()
        {
            return _dalMission.GetMissionThemeList();
        }
        public List<DropDown> GetMissionSkillList()
        {
            return _dalMission.GetMissionSkillList();
        }
        public List<Missions> MissionList()
        {
            return _dalMission.MissionList();
        }
        public string AddMission(Missions  mission)
        {
            return _dalMission.AddMission(mission);
        }       
        public Missions MissionDetailById(int id)
        {
            return _dalMission.MissionDetailById(id);
        }
        public string UpdateMission(Missions mission)
        {
            return _dalMission.UpdateMission(mission);
        }
        public string DeleteMission(int id)
        {
            return _dalMission.DeleteMission(id);
        }

        public string ApplyMission(MissionApplication missionApplication)
        {
            return _dalMission.ApplyMission(missionApplication);
        }

        public List<Missions> ClientSideMissionList(int userid)
        {
            return _dalMission.ClientSideMissionList(userid);
        }

        public List<MissionApplication> MissionApplicationList()
        {
            return _dalMission.MissionApplicationList();
        }

        public string MissionApplicationDelete(int id)
        {
            return _dalMission.MissionApplicationDelete(id);
        }
        public string MissionApplicationApprove(int id)
        {
            return _dalMission.MissionApplicationApprove(id);
        }
    }
}
