using System;

namespace Bibliotek.Pages
{
    public static class SessionExtensions
    {
        public static void Boolean(this ISession session, string key, bool value)
        {
            session.Set(key, BitConverter.GetBytes(value));
        }
        public static bool GetBoolean(this ISession session, string key)
        {
            var data = session.Get(key);

            if (data == null) { return false; }

            return BitConverter.ToBoolean(data, 0);
        }
    }
}
