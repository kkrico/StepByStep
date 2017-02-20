using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace StepByStep.Web.Infra.Factory
{
    public class ControllerFactory : IControllerFactory
    {
        private readonly IControllerFactory _innerFactory;

        public ControllerFactory(IControllerFactory innerFactory)
        {
            this._innerFactory = innerFactory;
        }
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            var controller = _innerFactory.CreateController(requestContext, controllerName) as Controller;
            if (controller != null)
            {
                controller.TempDataProvider = new RepositorioTempDataProvider(controller.TempData);
            }

            return controller;
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            throw new NotImplementedException();
        }

        public void ReleaseController(IController controller)
        {
            throw new NotImplementedException();
        }
    }

    public class RepositorioTempDataProvider : ITempDataProvider
    {
        public RepositorioTempDataProvider()
        {

        }
        public IDictionary<string, object> LoadTempData(ControllerContext controllerContext)
        {
            return new RepositorioTempData();
        }

        public void SaveTempData(ControllerContext controllerContext, IDictionary<string, object> values)
        {
            throw new NotImplementedException();
        }
    }

    public class RepositorioTempData : TempDataDictionary
    {
        public new object this[string key]
        {
            get
            {
                object value;
                if (TryGetValue(key, out value))
                {
                    Keep(key);
                    return value;
                }
                return null;
            }
            set
            {
                base[key] = value;
                Keys.Add(key);
            }
        }
    }
}