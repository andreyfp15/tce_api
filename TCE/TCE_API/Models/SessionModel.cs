using TCE_API.Entities;

namespace TCE_API.Models
{
    public class SessionModel : SessionEntity
    {
        private UserModel _User;
        public UserModel User
        {
            get
            {
                if (_User == null)
                    _User = new UserModel();
                return _User;
            }
            set { _User = value; }
        }
    }
}
