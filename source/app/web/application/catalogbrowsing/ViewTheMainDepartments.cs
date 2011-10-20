﻿using app.tasks;
using app.tasks.stubs;
using app.web.infrastructure;
using app.web.infrastructure.stubs;

namespace app.web.application.catalogbrowsing
{
    public class ViewTheMainDepartments : IEncapsulateUserFunctionality
    {
        IProvideInformationAboutTheStore department_repository;
        IDisplayInformation report_engine;

        public ViewTheMainDepartments():this(Stub.with<StubStoreDirectory>(),
            Stub.with<StubReportEngine>())
        {
        }

        public ViewTheMainDepartments(IProvideInformationAboutTheStore department_repository, IDisplayInformation report_engine)
        {
            this.department_repository = department_repository;
            this.report_engine = report_engine;
        }

        public void process(IContainRequestDetails request)
        {
            report_engine.display(department_repository.get_the_main_departments());
        }
    }
}