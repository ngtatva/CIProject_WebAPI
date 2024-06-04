using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Net.Mail;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Data_Access_Layer
{
    public class DALMission
    {
        private readonly AppDbContext _cIDbContext;

        public DALMission(AppDbContext cIDbContext)
        {
            _cIDbContext = cIDbContext;
        }
        public List<DropDown> GetMissionThemeList()
        {
            return _cIDbContext.MissionTheme
                .Select(mt => new DropDown { Value = mt.Id, Text = mt.ThemeName })
                .ToList();
        }

        public List<DropDown> GetMissionSkillList()
        {
            return _cIDbContext.MissionSkill
                .Select(ms => new DropDown { Value = ms.Id, Text = ms.SkillName })
                .ToList();
        }

        public List<Missions> MissionList()
        {
            return _cIDbContext.Missions.Where(m => m.IsDeleted == false)
                .ToList();
        }

        public string AddMission(Missions mission)
        {
            string result = "";

            mission.MissionOrganisationName = "";
            mission.CityName = "";
            mission.CountryName = "";
            mission.CreatedDate = DateTime.Now.ToUniversalTime();
            mission.MissionOrganisationDetail = "";
            mission.MissionApplyStatus = "";
            mission.MissionApproveStatus = "";
            mission.MissionAvilability = "";
            mission.MissionDateStatus = "";
            mission.MissionDeadLineStatus = "";
            mission.MissionDocuments = "";
            mission.MissionFavouriteStatus = "";
            mission.MissionSkillName = "";
            mission.MissionStatus = "";
            mission.MissionThemeName = "";
            mission.MissionType = "";
            mission.MissionVideoUrl = "";
            mission.ModifiedDate = DateTime.Now.ToUniversalTime();

            try
            {
                _cIDbContext.Missions.Add(mission);
                _cIDbContext.SaveChanges();
                result = "Mission added successfully.";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public Missions MissionDetailById(int id)
        {
            return _cIDbContext.Missions
                .FirstOrDefault(m => m.Id == id);
        }

        public string UpdateMission(Missions mission)
        {
            string result = "";
            try
            {
                // Check if the mission with the same title, city, start date, and end date already exists
                bool missionExists = _cIDbContext.Missions.Any(m => m.MissionTitle == mission.MissionTitle
                                                                    && m.CityId == mission.CityId
                                                                    && m.StartDate == mission.StartDate
                                                                    && m.EndDate == mission.EndDate
                                                                    && m.Id != mission.Id
                                                                    && !m.IsDeleted);

                if (!missionExists)
                {
                    // Find the mission in the database to update
                    var missionToUpdate = _cIDbContext.Missions.FirstOrDefault(m => m.Id == mission.Id && !m.IsDeleted);

                    if (missionToUpdate != null)
                    {
                        // Update the mission details
                        missionToUpdate.MissionTitle = mission.MissionTitle;
                        missionToUpdate.MissionDescription = mission.MissionDescription;
                        missionToUpdate.MissionOrganisationName = mission.MissionOrganisationName;
                        missionToUpdate.MissionOrganisationDetail = mission.MissionOrganisationDetail;
                        missionToUpdate.CountryId = mission.CountryId;
                        missionToUpdate.CityId = mission.CityId;
                        missionToUpdate.StartDate = mission.StartDate;
                        missionToUpdate.EndDate = mission.EndDate;
                        missionToUpdate.MissionType = mission.MissionType;
                        missionToUpdate.TotalSheets = mission.TotalSheets;
                        missionToUpdate.RegistrationDeadLine = mission.RegistrationDeadLine;
                        missionToUpdate.MissionThemeId = mission.MissionThemeId;
                        missionToUpdate.MissionSkillId = mission.MissionSkillId;
                        missionToUpdate.MissionImages = mission.MissionImages;
                        missionToUpdate.MissionDocuments = mission.MissionDocuments;
                        missionToUpdate.MissionAvilability = mission.MissionAvilability;
                        missionToUpdate.MissionVideoUrl = mission.MissionVideoUrl;
                        missionToUpdate.ModifiedDate = DateTime.Now;

                        _cIDbContext.SaveChanges();

                        result = "Update Mission Detail Successfully.";
                    }
                    else
                    {
                        throw new Exception("Mission not found.");
                    }
                }
                else
                {
                    throw new Exception("Mission with the same title, city, start date, and end date already exists.");
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public string DeleteMission(int id)
        {
            try
            {
                string result = "";
                var mission = _cIDbContext.Missions.FirstOrDefault(m => m.Id == id);
                if (mission != null)
                {
                    mission.IsDeleted = true;
                    _cIDbContext.SaveChanges();
                    result = "Delete Mission Detail Successfully.";
                }
                else
                {
                    result = "Mission not found."; // Or any other appropriate message
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Missions> ClientSideMissionList(int userid)
        {
            List<Missions> clientSideMissionList = new List<Missions>();

            try
            {
                clientSideMissionList = _cIDbContext.Missions
                    .Where(m => !m.IsDeleted)
                    .Select(m => new Missions
                    {
                        Id = m.Id,
                        MissionTitle = m.MissionTitle,
                        MissionDescription = m.MissionDescription,
                        MissionOrganisationDetail=m.MissionOrganisationDetail,
                        MissionOrganisationName=m.MissionOrganisationName,
                        CountryId= m.CountryId,
                        CountryName=m.CountryName,
                        CityId= m.CityId,
                        CityName= m.CityName,
                        StartDate= m.StartDate,
                        EndDate= m.EndDate,
                        MissionType= m.MissionType,
                        TotalSheets= m.TotalSheets,
                        RegistrationDeadLine= m.RegistrationDeadLine,
                        MissionThemeId= m.MissionThemeId,
                        MissionSkillId= m.MissionSkillId,
                        MissionImages= m.MissionImages,
                        MissionDocuments= m.MissionDocuments,
                        MissionAvilability= m.MissionAvilability,
                        MissionVideoUrl= m.MissionVideoUrl,
                        MissionThemeName= m.MissionThemeName,
                        MissionSkillName= string.Join(",",m.MissionSkillName),
                        MissionStatus= m.RegistrationDeadLine < DateTime.Now.AddDays(-1) ? "Closed" : "Available",
                        MissionApplyStatus= "Apply",
                        MissionDateStatus= m.EndDate <= DateTime.Now.AddDays(-1) ? "MissionEnd" : "MissionRunning",
                        MissionDeadLineStatus= m.RegistrationDeadLine <= DateTime.Now.AddDays(-1) ? "Closed" : "Running",
                        MissionFavouriteStatus= "0",
                        Rating= 0,
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return clientSideMissionList;
        }
    }
}
