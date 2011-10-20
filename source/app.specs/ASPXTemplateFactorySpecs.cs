 using System.Web;
 using Machine.Specifications;
 using app.web.infrastructure.aspnet;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
    [Subject(typeof(ASPXTemplateFactory))]  
    public class ASPXTemplateFactorySpecs
    {
        public abstract class concern : Observes<ICreateTemplateInstances,
                                            ASPXTemplateFactory>
        {
        
        }

   
        public class when_creating_an_instance_of_a_template_that_can_view_a_report : concern
        {
            Establish c = () =>
            {
                model = new AModel();
                the_page = fake.an<IRenderA<AModel>>();
                the_path = "sdfdsfsdf.aspx";

                template_path_registry = depends.on<IFindPathsToTemplates>();

                template_path_registry.setup(x => x.get_path_to_template_that_can_display<AModel>()).Return(the_path);

                depends.on<PageFactory>((path, page_type) =>
                {
                    page_type.ShouldEqual(typeof(IRenderA<AModel>));
                    path.ShouldEqual(the_path);
                    return the_page;
                });
            };

            Because b = () =>
                result = sut.create_view_to_display(model);


            It should_get_the_path_to_the_template = () =>
                template_path_registry.received(x => x.get_path_to_template_that_can_display<AModel>());

            It should_return_page_created_using_the_path = () =>
                result.ShouldEqual(the_page);

            It should_have_populated_the_report_model_for_the_template = () =>
                the_page.report_model.ShouldEqual(model);
                


            static IFindPathsToTemplates template_path_registry;
            static AModel model;
            static IHttpHandler result;
            static IRenderA<AModel> the_page;
            static string the_path;
        }

        public class AModel
    {
    }
    }

}
