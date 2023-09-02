using Experimental.System.Messaging;
using System.Net;
using System.Net.Mail;

namespace BookStore.User.EmailService
{
    public class MSMQ
    {
        MessageQueue userQ = new MessageQueue();
        public void sendData2Queue(string token)
        {
            userQ.Path = @".\private$\UserToken";
            if (!MessageQueue.Exists(userQ.Path))
            {
                MessageQueue.Create(userQ.Path);
            }
            userQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            userQ.ReceiveCompleted += UserQ_ReceiveComplete;
            userQ.Send(token);
            userQ.BeginReceive();
            userQ.Close();


        }

        private void UserQ_ReceiveComplete(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = userQ.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string body = $"<a style = \"color:#00802b; text-decoration: none; font-size:20px;\" href='https://localhost:44384/api/User/resetpassword/{token}'>Click me</a>";
                string subject = "Token For Reset Password";
                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("amit40fakeemail@gmail.com", "dyezqtoboczpzppj"),
                    EnableSsl = true,
                };
                using (var message = new MailMessage("amit40fakeemail@gmail.com", "amit40fakeemail@gmail.com", subject, body))
                {
                    message.IsBodyHtml = true;
                    smtp.Send(message);
                }
                userQ.BeginReceive();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
