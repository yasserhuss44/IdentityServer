// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.ComponentModel.DataAnnotations;

namespace CustomIdentityServer4.Controllers
{
    public class TwoFactorAuthModel
    {
        [Required]
        public Guid TokenId { get; set; }
        [Required]
        public string OTP { get; set; }
        public bool RememberLogin { get; set; }
        public bool AllowRememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}