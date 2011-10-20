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
            yield return
                new RequestCommand(x => true,
                                   new ViewReport<IEnumerable<DepartmentItem>>(new GetTheDepartmentsInADepartment()));
            yield return
                new RequestCommand(x => true, new ViewReport<IEnumerable<DepartmentItem>>(new GetTheMainDepartments()));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class GetTheDepartmentsInADepartment : IFetchA<IEnumerable<DepartmentItem>>
    {
        public IEnumerable<DepartmentItem> query_using(IContainRequestDetails request)
        {
            return Stub.with<StubStoreDirectory>().get_all_the_departments_in(request.map<ViewDepartmentsRequest>());
        }
    }

    public class GetTheMainDepartments : IFetchA<IEnumerable<DepartmentItem>>
    {
        public IEnumerable<DepartmentItem> query_using(IContainRequestDetails request)
        {
            return Stub.with<StubStoreDirectory>().get_the_main_departments();
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