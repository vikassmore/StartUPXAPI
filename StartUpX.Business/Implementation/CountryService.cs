using StartUpX.Business.Interface;
using StartUpX.Entity.DataModels;
using StartUpX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartUpX.Business.Implementation
{
    public class CountryService : ICountryService
    {
        StartUpDBContext _startupContext;

        public CountryService(StartUpDBContext startUpDBContext)
        {
            _startupContext = startUpDBContext;
        }
        /// <summary>
        /// Get All Countries
        /// </summary>
        /// <returns></returns>
        public List<CountryModel> GetAllCountries()
        {
            var countryEntity = _startupContext.Countries.Where(x => x.IsActive == true).ToList();
            var CountryList = countryEntity.Select(x => new CountryModel
            {
                CountryId=x.CountryId,
                CountryName=x.CountryName,
                CountryCode=x.CountryCode,
                IsActive = x.IsActive,

            }).ToList();
            return CountryList;
        }
    }
}
