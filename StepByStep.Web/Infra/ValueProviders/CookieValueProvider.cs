using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace StepByStep.Web.Infra.ValueProviders
{
    public class CookieValueProvider: IValueProvider
    {
        public bool ContainsPrefix(string prefix)
        {
            return HttpContext.Current.Request.Cookies[prefix] != null;
        }

        public ValueProviderResult GetValue(string key)
        {
            return new ValueProviderResult(HttpContext.Current.Request.Cookies[key].Value,
                HttpContext.Current.Request.Cookies[key].ToString(), CultureInfo.CurrentCulture);
        }
    }

   public class CookieValueProviderFactory: ValueProviderFactory
   {
       public override IValueProvider GetValueProvider(ControllerContext controllerContext)
       {
           return new CookieValueProvider();
       }
   }
}