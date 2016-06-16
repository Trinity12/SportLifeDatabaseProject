using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace SportLife.Models.IdentityModels {
    internal class EmailService : IIdentityMessageService {
        public Task SendAsync ( IdentityMessage message ) {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }
}