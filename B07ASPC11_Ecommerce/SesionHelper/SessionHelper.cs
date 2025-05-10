using Newtonsoft.Json;

namespace B07ASPC11_Ecommerce.SesionHelper
{
    public static class SessionHelper
    {
        public static void SetObjInsession(this ISession session,string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));

        }
        public static List<T> GetSessionObj<T>(this ISession session,string key)
        {
            string sessiondata= session.GetString(key);
            return sessiondata == null ? default(List<T>) : JsonConvert.DeserializeObject <List<T>>(sessiondata);
        }

        public static  T GetObj<T>(this ISession session, string key)
        {
            string sessiondata = session.GetString(key);
            return sessiondata == null ? default(T) : JsonConvert.DeserializeObject<T>(sessiondata);
        }
    }
}
