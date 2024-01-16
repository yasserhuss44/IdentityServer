using System.Linq;
using System;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace CustomIdentityServer4.UserServices
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;

        public UserRepository(IConfiguration configuration, IWebHostEnvironment environment)
        {
            this.configuration = configuration;
            this.environment = environment;
        }

        public bool ValidateCredentials(string userName, string password)
        {
            try
            {
                using PrincipalContext oPrincipalContext = GetPrincipalContext();

                bool isValid = oPrincipalContext.ValidateCredentials(userName, password);

                if (isValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<CustomUser> FindBySubjectId(string subjectId)
        {
            try
            {
                using PrincipalContext oPrincipalContext = GetPrincipalContext();
                
                UserPrincipal oUserPrincipal = new UserPrincipal(oPrincipalContext);
                
                var users = new PrincipalSearcher(oUserPrincipal).FindAll().Cast<UserPrincipal>();

                var user = users.FirstOrDefault(x => x.Sid.Value.Equals(subjectId, StringComparison.OrdinalIgnoreCase));
                               
                return await Task.FromResult(new CustomUser
                {
                    UserName =  user.Name,
                    SubjectId = user.Sid.Value,
                    Email =user.Name,
                    VoiceTelephoneNumber = user.VoiceTelephoneNumber
                });

            }
            catch (Exception ex)
            {
                return await Task.FromResult(new CustomUser());
            }
        }

        public CustomUser FindByUsername(string userName)
        {
            try
            {
                using PrincipalContext oPrincipalContext = GetPrincipalContext();

                UserPrincipal oUserPrincipal = new UserPrincipal(oPrincipalContext);

                var user = new PrincipalSearcher(oUserPrincipal).FindAll().Cast<UserPrincipal>()
                .FirstOrDefault(x => x.Name.Equals(userName, StringComparison.OrdinalIgnoreCase));

                

                return new CustomUser
                {
                    UserName =  userName,
                    SubjectId = user.Sid.Value,
                    Email =userName,
                    VoiceTelephoneNumber = user.VoiceTelephoneNumber

                };
                
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private PrincipalContext GetPrincipalContext()
        {
            var name = this.configuration.GetValue<string>("DomainSettings:DomainName");

            var type1 = this.configuration.GetValue<string>("DomainSettings:ContextType");

           // var domainSettings = section.Get<DomainSetting>();

            var type = (ContextType)Enum.Parse(typeof(ContextType), type1);

            return new PrincipalContext(type, name);

        }

        public class DomainSetting
        {
            public string ContextType { get; set; }
            public string DomainName { get; set; }
        }

    }
}


