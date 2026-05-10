using Ecom.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Service
{
    public interface IEmailService
    {
        Task SendEmail(EmailDto emailDto);

    }
}
