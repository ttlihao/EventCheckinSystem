using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(IEnumerable<string> toList, string subject, string body);
    }
}
