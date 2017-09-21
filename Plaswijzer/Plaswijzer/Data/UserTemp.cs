using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plaswijzer.Data
{
    public class UserTemp: IUserTemp
    {
        private Dictionary<long, UserData> UserInformation;
        private Dictionary<DateTime, long> LastConnected;
        private Dictionary<long, DateTime> InverseLastConnected;

        private static readonly int MAX_USERS = 500;
        private static readonly int KEEP_ALIVE_MINUTES = 10;
        public UserTemp()
        {
            UserInformation = new Dictionary<long, UserData>();
            LastConnected = new Dictionary<DateTime, long>();
            InverseLastConnected = new Dictionary<long, DateTime>();

        }

        public class UserData
        {
            public string Lang { get; set; }
            public string Type { get; set; }
        }

        /// <summary>
        /// Removes users that didn't do any action last 'treshold' minutes
        /// </summary>
        /// <param name="minutes"></param>
        public void CleanMaps(int minutes)
        {
            DateTime threshhold = DateTime.Now.AddMinutes(minutes * 2);
            foreach (DateTime time in LastConnected.Keys)
            {
                if (DateTime.Compare(time, threshhold) < 0) //date is before threshhold
                {
                    Remove(LastConnected[time]);
                }
            }

        }

        public string GetLanguage(long id)
        {
            if (UserInformation.ContainsKey(id))
            {
                string lang = UserInformation[id].Lang;
                return lang;
            }
            else
            {
                return null;
            }
        }

        public string GetType(long id)
        {
            if (UserInformation.ContainsKey(id))
            {
                string type = UserInformation[id].Type;
                return type;
            }
            else
            {
                return null;
            }
        }

        public void Remove(long id)
        {
            if (UserInformation.ContainsKey(id))//Data could already be deleted
            {
                LastConnected.Remove(InverseLastConnected[id]);
                InverseLastConnected.Remove(id);
                UserInformation.Remove(id);
            }
        }

        public void Add(long id, string lang, string type)
        {
            try
            {
                Remove(id);
                DateTime now = DateTime.Now;
                UserInformation.Add(id, new UserData { Lang = lang, Type = type });
                LastConnected.Add(now, id);
                InverseLastConnected.Add(id, now);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            if (UserInformation.Count > MAX_USERS)
                CleanMaps(KEEP_ALIVE_MINUTES); //remove users that did not connect in more than x minutes
        }


    }
}
