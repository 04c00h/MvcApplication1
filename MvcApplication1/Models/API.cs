using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MvcApplication1.Models
{
    public static class API
    {

        public class SheetUser
        {
            public string fio { get; set; }
            public string value { get; set; }
        }

        public class ObjAuthRequest
        {
            public string auth { get; set; }
            public string login { get; set; }
            public string rnd { get; set; }
        }

        public class ObjSheetRequest
        {
            public string key { get; set; }
            public string value { get; set; }
            public string token { get; set; }
        }

        public class ObjResponse
        {
            public string view;
            public string title;
            public string token;
            public object data;
            public ObjResponse(string title, string view, string token, object data)
            {
                this.title = title;
                this.view = view;
                this.token = token;
                this.data = data;
            }
        }

        public static string userAuth(ObjAuthRequest obj)
        {
            if (obj != null)
            {
                string now = DateTime.Now.ToShortDateString();
                string pass = API.getPass(obj.login);
                string auth = API.md5(obj.login + pass + obj.rnd + now);
                if (auth == obj.auth)
                {
                    string token = md5(auth + new Random(DateTime.Now.Millisecond).Next().ToString() + "Cookie");
                    int userId = API.getUserId(obj.login);
                    API.addToken(userId, token);
                    return token;
                }
            }
            return "";
        }

        private static void addToken(int userId, string token)
        {
            // insert into token (usesr_id, token) value (userId, token)
        }

        private static int getUserId(string p)
        {
            int resutl = 1;
            // select id from user where login = p
            return resutl;
        }

        public static List<API.SheetUser> getAllUsers()
        {
            List<API.SheetUser> result = new List<SheetUser>();
            // select fio, value from user
            return result;
        }

        internal static string updValue(ObjSheetRequest obj)
        {
            int userId = API.getUserIdFromToken(obj.token);
            if (userId > 0)
            {
                //update user set value = f(value, key) where id = userId
                return "";
            }
            return "пользователь не авторизован";
            throw new NotImplementedException();
        }

        public static int getUserIdFromToken(string p)
        {
            //select user_id from Token where token = p
            return 1;
        }

        public static int getUserIdFromUser(string p)
        {
            //select id from User where login = p
            return 1;
        }

        static string getPass(string p)
        {
            //select pass from User where login = p
            return API.md5("a1");
        }

        static string md5(string value)
        {
            byte[] hash = Encoding.ASCII.GetBytes(value);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hashenc = md5.ComputeHash(hash);
            string result = "";
            foreach (var b in hashenc)
            {
                result += b.ToString("x2");
            }
            return result;
        }
    }
}