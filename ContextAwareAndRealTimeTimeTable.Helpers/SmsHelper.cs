using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Configuration;
using System.IO;

namespace ContextAwareAndRealTimeTimeTable.Helpers
{
    public class SmsHelper
    {

        public static void SendSmsToOneContact(string apiBaseUrl, string phoneNumber, string messageBody, string senderId)
        {
            string apiUrl = apiBaseUrl + "&message=" + messageBody + "&from=" + senderId + "&to=" + phoneNumber;
            ConnectToSmsProvider(apiUrl);
        }

        public static void SendSmsToMultipleContacts(string apiBaseUrl, List<string> phoneNumbers, string messageBody, string senderId)
        {
            StringBuilder phoneContacts = new StringBuilder();
            foreach (string phoneNumber in phoneNumbers)
            {
                phoneContacts.Append(phoneNumber).Append(",");
            }
            string number = phoneContacts.ToString();
            string apiUrl = apiBaseUrl + "&message=" + messageBody + "&from=" + senderId + "&to=" + phoneContacts.ToString();
            ConnectToSmsProvider(apiUrl);
        }

        public static void ConnectToSmsProvider(string providerUrl)
        {
            Uri url = new Uri(providerUrl);
            WebRequest request = WebRequest.Create(providerUrl);
            //WebResponse response = request.GetResponse();
            //Stream responseStream = response.GetResponseStream();
            //TODO: Incase i need response...can work it out from here
            string responseJSONInfo = String.Empty;

            //using (var sr = new StreamReader(responseStream))
            //{
            //    responseJSONInfo = sr.ReadToEnd();
            //    using (StreamWriter x = new StreamWriter(@"C:\Work\Current Projects\ContextAwareAndRealTimeTimeTable\ContextAwareAndRealTimeTimeTable.Web\log.txt"))
            //    {

            //        x.WriteLine(responseJSONInfo);
            //    }
            //}
        }

    }
}
