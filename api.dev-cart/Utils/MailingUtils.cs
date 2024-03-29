﻿using AegisImplicitMail;
using api.dev_cart.Entity;
using api.dev_cart.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Utils
{
    public class MailingUtils
    {
        public static async void SendOrderEmail(bool orderWasPaid, List<CartProducts> kartProducts, string orderNumber, string paymentReference, string To, string Subject)
        {
            Entities entity = new Entities();
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(orderWasPaid ? "~/Mailing/orderConfirmedEmail.html" : "~/Mailing/paymentOderEmail.html")))
            {
                string body = reader.ReadToEnd();

                string Products = String.Empty;
                foreach (var product in kartProducts)
                {
                    string ProductName = entity.cat_Products.Where(x => x.Product_Id == product.Product_Id).Select(x => x.Product_Name).FirstOrDefault();
                    string Price = String.Format("{0:C}", product.Price);

                    Products += $"<li style='text-align: left;'>{ProductName} {Price}</li>";
                }
                string Total = String.Format("{0:C}", kartProducts.Sum(x => x.Price));
                string OrderURL = $"https://sandbox-dashboard.openpay.mx/paynet-pdf/{ConfigurationUtils.GetConfiguration("MerchantID", "")}/{paymentReference}";

                body = body.Replace("{orderNumber}", orderNumber);
                body = body.Replace("{Products}", Products);
                body = body.Replace("{Total}", Total);
                body = body.Replace("{OrderURL}", OrderURL);

                SendEmail(body, To, Subject);

                await Task.CompletedTask;
            }
        }

        public static async void SendTrackingIdEmail(string To, string TrackingId, string ShippingService)
        {
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Mailing/trackingIdEmail.html")))
            {
                string body = reader.ReadToEnd();
                string TrackingURL = string.Empty;

                switch (ShippingService.ToLower())
                {
                    case "fedex":
                        TrackingURL = $"<a class=\"mcnButton\" title=\"Rastrear Envío\" href=\"https://www.fedex.com/apps/fedextrack/index.html?tracknumbers={TrackingId}&cntry_code=mx\" target=\"_blank\" style=\"font-weight: bold;letter-spacing: normal;line-height: 100%;text-align: center;text-decoration: none;color: #FFFFFF;\">Rastrear Envío</a>";
                        break;
                    case "estafeta":
                        TrackingURL = $"<a class=\"mcnButton\" title=\"Rastrear Envío\" href=\"https://www.estafeta.com/Herramientas/Rastreo\" target=\"_blank\" style=\"font-weight: bold;letter-spacing: normal;line-height: 100%;text-align: center;text-decoration: none;color: #FFFFFF;\">Rastrear Envío</a>";
                        break;
                    default:
                        TrackingURL = $"<a class=\"mcnButton\" title=\"Rastrear Envío\" href=\"#\" target=\"_blank\" style=\"font-weight: bold;letter-spacing: normal;line-height: 100%;text-align: center;text-decoration: none;color: #FFFFFF;\">Rastrear Envío</a>";
                        break;
                }

                body = body.Replace("{TrackingId}", TrackingId);
                body = body.Replace("{ShippingService}", ShippingService);
                body = body.Replace("{TrackingURL}", TrackingURL);

                SendEmail(body, To, "Numero de guia");
            }

            await Task.CompletedTask;
        }

        public static async void SendTestEmail(string To)
        {
            Entities entity = new Entities();
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Mailing/testEmail.html")))
            {
                string body = reader.ReadToEnd();
                SendEmail(body, To, "Test Email");

                await Task.CompletedTask;
            }
        }

        private static void SendEmail(string bodyEmail, string To, string Subject)
        {
            var mailer = new MimeMailer(ConfigurationUtils.GetConfiguration("MimeMailerHost", ""), ConfigurationUtils.GetConfiguration("MimeMailerPort", 0));
            mailer.User = ConfigurationUtils.GetConfiguration("MimeMailerUsername", "");
            mailer.Password = ConfigurationUtils.GetConfiguration("MimeMailerPassword", "");
            mailer.SslType = SslMode.Ssl;
            mailer.AuthenticationMode = AuthenticationType.Base64;

            var message = new MimeMailMessage();
            message.From = new MimeMailAddress(ConfigurationUtils.GetConfiguration("MimeMailerUsername", ""), ConfigurationUtils.GetConfiguration("FromLabel", "Dev-Cart"));
            message.To.Add(To);
            message.IsBodyHtml = true;
            addAttachments(ref message, bodyEmail);
            message.BodyEncoding = Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            message.Subject = Subject;
            message.Body = bodyEmail;
            mailer.SendCompleted += compEvent;
            mailer.SendMailAsync(message);
        }

        private static void compEvent(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
                Console.Out.WriteLine(e.UserState.ToString());

            Console.Out.WriteLine("is it canceled? " + e.Cancelled);

            if (e.Error != null)
                Console.Out.WriteLine("Error : " + e.Error.Message);
        }

        private static void addAttachments(ref MimeMailMessage message, string bodyEmail)
        {
            if (bodyEmail.Contains("cid:"))
            {
                var logo = new MimeAttachment(HttpContext.Current.Server.MapPath("~/Mailing/img/logo.png"));
                logo.Location = AttachmentLocation.Inline;
                logo.ContentId = "logo";
                logo.ContentDisposition.Inline = true;
                logo.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                var instagram = new MimeAttachment(HttpContext.Current.Server.MapPath("~/Mailing/img/instagram.png"));
                instagram.Location = AttachmentLocation.Inline;
                instagram.ContentId = "instagram.";
                instagram.ContentDisposition.Inline = true;
                instagram.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                var facebook = new MimeAttachment(HttpContext.Current.Server.MapPath("~/Mailing/img/facebook.png"));
                facebook.Location = AttachmentLocation.Inline;
                facebook.ContentId = "facebook";
                facebook.ContentDisposition.Inline = true;
                facebook.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                var website = new MimeAttachment(HttpContext.Current.Server.MapPath("~/Mailing/img/website.png"));
                website.Location = AttachmentLocation.Inline;
                website.ContentId = "website";
                website.ContentDisposition.Inline = true;
                website.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                var email = new MimeAttachment(HttpContext.Current.Server.MapPath("~/Mailing/img/email.png"));
                email.Location = AttachmentLocation.Inline;
                email.ContentId = "email";
                email.ContentDisposition.Inline = true;
                email.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                var youtube = new MimeAttachment(HttpContext.Current.Server.MapPath("~/Mailing/img/youtube.png"));
                youtube.Location = AttachmentLocation.Inline;
                youtube.ContentId = "youtube";
                youtube.ContentDisposition.Inline = true;
                youtube.ContentDisposition.DispositionType = DispositionTypeNames.Inline;

                message.Attachments.Add(logo);
                message.Attachments.Add(instagram);
                message.Attachments.Add(facebook);
                message.Attachments.Add(website);
                message.Attachments.Add(email);
                message.Attachments.Add(youtube);
            }
        }

        //private static AlternateView alternateView(string bodyEmail)
        //{
        //    AlternateView vw = AlternateView.CreateAlternateViewFromString(bodyEmail, null, MediaTypeNames.Text.Html);

        //    if (bodyEmail.Contains("cid:"))
        //    {
        //        LinkedResource logo = new LinkedResource(HttpContext.Current.Server.MapPath("~/Mailing/img/logo.png"), MediaTypeNames.Image.Jpeg);
        //        LinkedResource instagram = new LinkedResource(HttpContext.Current.Server.MapPath("~/Mailing/img/instagram.png"), MediaTypeNames.Image.Jpeg);
        //        LinkedResource facebook = new LinkedResource(HttpContext.Current.Server.MapPath("~/Mailing/img/facebook.png"), MediaTypeNames.Image.Jpeg);
        //        LinkedResource website = new LinkedResource(HttpContext.Current.Server.MapPath("~/Mailing/img/website.png"), MediaTypeNames.Image.Jpeg);
        //        LinkedResource email = new LinkedResource(HttpContext.Current.Server.MapPath("~/Mailing/img/email.png"), MediaTypeNames.Image.Jpeg);
        //        LinkedResource youtube = new LinkedResource(HttpContext.Current.Server.MapPath("~/Mailing/img/youtube.png"), MediaTypeNames.Image.Jpeg);

        //        logo.ContentId = "logo";
        //        instagram.ContentId = "instagram";
        //        facebook.ContentId = "facebook";
        //        website.ContentId = "website";
        //        email.ContentId = "email";
        //        youtube.ContentId = "youtube";

        //        logo.TransferEncoding = TransferEncoding.Base64;
        //        instagram.TransferEncoding = TransferEncoding.Base64;
        //        facebook.TransferEncoding = TransferEncoding.Base64;
        //        website.TransferEncoding = TransferEncoding.Base64;
        //        email.TransferEncoding = TransferEncoding.Base64;
        //        youtube.TransferEncoding = TransferEncoding.Base64;

        //        vw.LinkedResources.Add(logo);
        //        vw.LinkedResources.Add(instagram);
        //        vw.LinkedResources.Add(facebook);
        //        vw.LinkedResources.Add(website);
        //        vw.LinkedResources.Add(email);
        //        vw.LinkedResources.Add(youtube);
        //    }

        //    return vw;
        //}
    }
}