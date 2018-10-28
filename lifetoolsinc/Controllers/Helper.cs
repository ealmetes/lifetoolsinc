using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace lifetoolsinc.Controllers
{
    public class Helper : Controller
    {
        public static string PassCode()
        {
            Random rand = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 8)
                .Select(x => pool[rand.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }
        public static void InviteEmail(string to, string username, string business, string key)
        {
            string message = "";
            message = "Thank you for your interest in the position at " + business + ". We are moving you forward in the review process. The next step involves a short online profile. <br /><p>Visit the following link: http://lifetoolsinc.com/Account/Access " + "<br />"
                + "<p>After the page opens enter the following: <br /> </p><p>Username: <strong>" + username + "</strong> <br />  </p><p>Access code: <strong>" + key + "</strong></hr><br />" +
                " <p>Your unique access code expires 72 hours from the timestamp of this email. Please contact the HR department at " + business + " with any questions.</p>";
            using (MailMessage mail = new MailMessage())
            {

                mail.From = new MailAddress("contact@requestshuttle.com", "ReQRequest");
                mail.To.Add(new MailAddress(to));
                mail.Subject = business;
                mail.Body = string.Format(message);
                mail.IsBodyHtml = true;
                //mail.Attachments.Add(new Attachment("C:\\file.zip"));
                using (SmtpClient smtp = new SmtpClient("relay-hosting.secureserver.net"))
                {
                    try
                    {
                        smtp.Send(mail);

                    }
                    catch
                    {
                        //TempData["error"] = "An error as occurred while sending the code please try again.";

                    }

                }
            }
        }
    }
}