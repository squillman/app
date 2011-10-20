using System.Web;
using System.Web.Compilation;
using app.web.infrastructure.stubs;

namespace app.web.infrastructure.aspnet
{
    public class ASPXTemplateFactory : ICreateTemplateInstances
    {
        IFindPathsToTemplates template_path_registry;
        PageFactory page_factory;

        public ASPXTemplateFactory(IFindPathsToTemplates template_path_registry, PageFactory page_factory)
        {
            this.template_path_registry = template_path_registry;
            this.page_factory = page_factory;
        }

        public ASPXTemplateFactory():this(Stub.with<StubAspxPathRegistry>(),
            BuildManager.CreateInstanceFromVirtualPath)
        {
        }

        public IHttpHandler create_view_to_display<ReportModel>(ReportModel report_model)
        {
            var path  = template_path_registry.get_path_to_template_that_can_display<ReportModel>();
            var page = (IRenderA<ReportModel>)page_factory(path, typeof(IRenderA<ReportModel>));
            page.report_model = report_model;
            return page;
        }
    }
}