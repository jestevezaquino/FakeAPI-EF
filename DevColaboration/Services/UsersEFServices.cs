using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DevColaboration.Models.EF;
using DevColaboration.Models.FakeAPI;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevColaboration.Services
{
    public class UsersEFServices
    {

        private readonly ApplicationDbContext _context;

        public UsersEFServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<DevColaboration.Models.EF.User> GetUsersFromRepository() 
        {
            var usersRepository = _context.Users.Include(x => x.Company).Include(x => x.Address).ThenInclude(x => x.Geo).ToList();
            return usersRepository;
        }

        public bool AddUserDB(DevColaboration.Models.FakeAPI.User userAPI)
        {
            try
            {
                // Adding company to DB

                DevColaboration.Models.EF.Company companyDB = new DevColaboration.Models.EF.Company();
                companyDB.Name = userAPI.Company.Name;
                companyDB.CatchPhrase = userAPI.Company.CatchPhrase;
                companyDB.Bs = userAPI.Company.Bs;
                _context.Companies.Add(companyDB);
                _context.SaveChanges(true);

                // Adding User to DB

                DevColaboration.Models.EF.User userDB = new DevColaboration.Models.EF.User();
                userDB.Name = userAPI.Name;
                userDB.Username = userAPI.Username;
                userDB.Phone = userAPI.Phone;
                userDB.Website = userAPI.Website;
                userDB.CompanyId = _context.Companies.ToList().Last().Id;
                userDB.Email = userAPI.Email;
                _context.Users.Add(userDB);
                _context.SaveChanges(true);

                // Adding Geo to DB

                DevColaboration.Models.EF.Geo geoDB = new DevColaboration.Models.EF.Geo();
                geoDB.Lat = userAPI.Address.Geo.Lat;
                geoDB.Lng = userAPI.Address.Geo.Lng;
                _context.Geos.Add(geoDB);
                _context.SaveChanges(true);

                // Adding Address to DB

                DevColaboration.Models.EF.Address addressDB = new DevColaboration.Models.EF.Address();
                addressDB.Street = userAPI.Address.Street;
                addressDB.Suite = userAPI.Address.Suite;
                addressDB.Zipcode = userAPI.Address.Zipcode;
                addressDB.City = userAPI.Address.City;
                addressDB.UserId = _context.Users.ToList().Last().Id;
                addressDB.GeoId = _context.Geos.ToList().Last().Id;
                _context.Addresses.Add(addressDB);
                _context.SaveChanges(true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
