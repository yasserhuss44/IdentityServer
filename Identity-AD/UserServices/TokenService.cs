using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using CustomIdentityServer4.Controllers;
using IdentityServer4Admin.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace CustomIdentityServer4.UserServices
{
    public class TokenService : ITokenService
    {
        private readonly IUserRepository _userRepository;
        private readonly ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment environment;
        private readonly ICryptoService cryptoService;

        public TokenService(ICryptoService cryptoService, IUserRepository userRepository, ApplicationDbContext dbContext,
            IWebHostEnvironment environment
            )
        {
            this.cryptoService = cryptoService;
            _userRepository = userRepository;
            this.dbContext = dbContext;
            this.environment = environment;
        }

        public async Task<UserToken> GetTokenDetails(Guid tokenId)
        {
            var token = await this.dbContext.Tokens.FirstOrDefaultAsync(t => t.Id == tokenId);

            return token;
        }

        public async Task RemoveToken(Guid tokenId)
        {
            var token = await this.dbContext.Tokens.FirstOrDefaultAsync(t => t.Id == tokenId);

            this.dbContext.Tokens.Remove(token);

            await this.dbContext.SaveChangesAsync();

        }

        public async Task<UserToken> CreateToken(string userName)
        {
            var customer = this._userRepository.FindByUsername(userName);

            var vm = new UserToken
            {
                Id = Guid.NewGuid(),
                ExpireOn = DateTime.Now.AddMinutes(5),
                CreatedOn = DateTime.Now,
                SubjectId = this.cryptoService.Encrypt(customer.SubjectId),
                OTP = this.cryptoService.Encrypt(this.GenerateRandomCode()),
                UserName = this.cryptoService.Encrypt(userName),

            };

            await this.dbContext.Tokens.AddAsync(vm);

            await this.dbContext.SaveChangesAsync();

            return await Task.FromResult(vm);
        }

        private string GenerateRandomCode()
        {
            if (environment.EnvironmentName== "Development")
                return "123456";
            else
            {
                Random rd = new Random();

                int rand_num = rd.Next(199999, 999999);

                return rand_num.ToString();
            }
        }

    }
}
