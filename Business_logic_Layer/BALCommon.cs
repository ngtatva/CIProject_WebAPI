﻿using Data_Access_Layer;
using Data_Access_Layer.Repository.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business_logic_Layer
{
    public class BALCommon
    {
        private readonly DALCommon _dalCommon;

        public BALCommon(DALCommon dalCommon)
        {
            _dalCommon = dalCommon;
        }

        public async Task<List<DropDown>> GetCountryListAsync()
        {
            return await _dalCommon.CountryListAsync();
        }

        public async Task<List<DropDown>> GetCityListAsync(int countryId)
        {
            return await _dalCommon.CityListAsync(countryId);
        }

        public async Task<List<DropDown>> GetMissionCountryListAsync()
        {
            return await _dalCommon.MissionCountryListAsync();
        }

        public async Task<List<DropDown>> GetMissionCityListAsync()
        {
            return await _dalCommon.MissionCityListAsync();
        }

        public async Task<List<DropDown>> GetMissionThemeListAsync()
        {
            return await _dalCommon.MissionThemeListAsync();
        }

        public async Task<List<DropDown>> GetMissionSkillListAsync()
        {
            return await _dalCommon.MissionSkillListAsync();
        }

        public async Task<List<DropDown>> GetMissionTitleListAsync()
        {
            return await _dalCommon.MissionTitleListAsync();
        }

        public List<DropDown> GetUserSkill(int userId)
        {
            return _dalCommon.GetUserSkill(userId);
        }
    }
}
