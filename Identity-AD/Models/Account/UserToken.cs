using System;

namespace CustomIdentityServer4.Controllers
{
    public class UserToken
    {
        public string SubjectId { get; set; }
        public Guid Id { get; set; }
        public string OTP { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpireOn { get; set; }
        public string UserName { get; internal set; }
    }

    public class UserTokenModel
    {
        public Guid SubjectId { get; set; }
        public Guid Id { get; set; }
        public string OTP { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ExpireOn { get; set; }
        public string UserName { get; internal set; }
    }
}
