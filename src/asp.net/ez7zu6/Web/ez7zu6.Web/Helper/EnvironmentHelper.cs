namespace ez7zu6.Web.Helper
{
    public class EnvironmentHelper
    {
        private bool? _isAppHarbor;
        public bool IsAppHarbor
        {
            get
            {
                if (!_isAppHarbor.HasValue)
                    _isAppHarbor = bool.TryParse(System.Environment.GetEnvironmentVariable("IS_APPHARBOR"), out bool isAppHarbor) && isAppHarbor;

                return _isAppHarbor.Value;
            }
        }
    }
}
