using Lab4_3_AspNetMVC_BlindDating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_3_AspNetMVC_BlindDating.ViewModels
{
    public class InboxViewModel
    {

        public IEnumerable<MailMessage> mailMessages;
        public IEnumerable<DatingProfile> fromProfiles;

    }
}
