using System.Web;
using app.models;

namespace app.web.infrastructure.stubs
{
    public class StubRequestFactory : ICreateRequests
    {
        public IContainRequestDetails create_request_from(HttpContext original_http_context)
        {
            return new StubRequest();
        }

        class StubRequest : IContainRequestDetails
        {
            public InputModel map<InputModel>()
            {
                object result = new object();
                if (typeof(InputModel).Equals(typeof(ViewDepartmentsRequest)))
                {
                    result = new ViewDepartmentsRequest();
                }
                else
                {
                    result = new ViewProductsRequest();
                }
                return (InputModel) result;
            }
        }
    }
}