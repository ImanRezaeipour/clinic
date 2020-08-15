using System.Threading.Tasks;
using Microsoft.Owin;

namespace Clinic.FrameWork.Middlewares
{
    public class ApplicationMiddleware: OwinMiddleware
    {
        public ApplicationMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            context.Request.Path = new PathString("/iman");

            await Next.Invoke(context);
        }
    }
}
