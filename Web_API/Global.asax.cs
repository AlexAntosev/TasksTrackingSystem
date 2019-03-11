using BLL.Mapper;
using System.Web.Http;
using Web_API.App_Start;

namespace Web_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            NinjectWebCommon.CreateKernel();
            AutoMapperConfig.Configure();
        }
    }
}
