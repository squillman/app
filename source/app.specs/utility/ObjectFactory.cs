using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using developwithpassion.specifications.extensions;

namespace app.specs.utility
{
    public class ObjectFactory
    {
        public class web
        {
            public static HttpContext create_http_context()
            {
                return new HttpContext(create_request(), create_response());
            }

            static HttpRequest create_request()
            {
                return new HttpRequest("sdfsf.aspx", "http://localhost/blah.aspx", String.Empty);
            }

            static HttpResponse create_response()
            {
                return new HttpResponse(new StringWriter());
            }
        }

        public static class expressions
        {
            public static ExpressionBuilder<T> to_target<T>()
            {
                return new ExpressionBuilder<T>();
            }
        }

        public class ExpressionBuilder<Target>
        {
            public ConstructorInfo get_constructor(Expression<Func<Target>> ctor)
            {
                return ctor.Body.downcast_to<NewExpression>().Constructor;
            }
        }
    }
}