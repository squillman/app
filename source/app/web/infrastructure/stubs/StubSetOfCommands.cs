using System.Collections;
using System.Collections.Generic;
using app.models;
using app.tasks.stubs;
using app.web.application.catalogbrowsing;

namespace app.web.infrastructure.stubs
{
    public class StubSetOfCommands : IEnumerable<IProcessOneSpecificTypeOfRequest>
    {
        public IEnumerator<IProcessOneSpecificTypeOfRequest> GetEnumerator()
        {
            yield return
                new RequestCommand(x => true, new ViewReport<IEnumerable<ProductItem>>(new GetDepartmentProducts()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class GetDepartmentProducts : IFetchA<IEnumerable<ProductItem>>
    {
        public IEnumerable<ProductItem> query_using(IContainRequestDetails request)
        {
            return Stub.with<StubStoreDirectory>().get_all_the_products_in(request.map<ViewProductsRequest>());
        }
    }
}