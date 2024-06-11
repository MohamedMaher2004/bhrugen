using bhrugen_webapi.Data;

namespace bhrugen_webapi.Helpers
{
    public class Helpers
    {
        private static  ApplicationDBContext _DB;
        public Helpers(ApplicationDBContext db)
        {
            _DB = db;
        }
        public static bool IsUniqueUser(string username)
        {
            var user =  _DB.LocalUsers.FirstOrDefault(x => x.UserName == username);
            if (user == null) return true;
            return false;
        }
    }
}
