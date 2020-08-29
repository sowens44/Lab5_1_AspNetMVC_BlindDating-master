using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab4_3_AspNetMVC_BlindDating.Models;
using Lab4_3_AspNetMVC_BlindDating.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab4_3_AspNetMVC_BlindDating.Controllers
{
    public class MessagesController : Controller
    {

        private BlindDatingContext _context;
        private UserManager<IdentityUser> _userManager;

        public MessagesController(BlindDatingContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IActionResult Inbox()
        {
            DatingProfile profile = _context.DatingProfile.FirstOrDefault(id =>
            id.UserAccountId == _userManager.GetUserId(User));

            // Declare ViewModel of Messages and Profiles.
            InboxViewModel inbox = new InboxViewModel();

            // Retrieve all messages sent to active user. (Inbox)
            inbox.mailMessages = _context.MailMessage.Where(to => to.ToProfileId == profile.Id).ToList();

            // Declare a Generic List meant to hold all messages sent where user replied to sender. (Sentbox)
            List<DatingProfile> fromList = new List<DatingProfile>();
            foreach (var msg in inbox.mailMessages)
            {
                fromList.Add(_context.DatingProfile.FirstOrDefault(from => from.Id == msg.FromProfileId));
            }

            inbox.fromProfiles = fromList;
            return View(inbox);

        }

        // New Message Method
        public IActionResult NewMessage(int id) //int toProfileId
        {

            ViewBag.ToProfileId = id;
            // ViewBag.ToProfileId = toProfileId;
            return View();

        }

        // Read message using Message ID
        public IActionResult Read(int id)
        {

            MailMessage mail = _context.MailMessage.FirstOrDefault(m => m.Id == id);
            mail.IsRead = true;
            _context.Update(mail);
            _context.SaveChanges();
            return View(mail);

        }

        // Sent Method
        [HttpPost]
        [Authorize]
        public IActionResult Send([Bind("MessageTitle, MessageText")] MailMessage mail, int toProfileId)
        {

            // Retrieve Active User Profile 
            DatingProfile fromUser = _context.DatingProfile.FirstOrDefault(p => p.UserAccountId == _userManager.GetUserId(User));

            mail.FromProfileId = fromUser.Id;
            mail.IsRead = false;
            mail.FromProfile = fromUser;
            mail.ToProfileId = toProfileId;

            _context.Add(mail);
            _context.SaveChanges();

            return RedirectToAction("Browse", "DatingProfiles");

        }

    }
}
