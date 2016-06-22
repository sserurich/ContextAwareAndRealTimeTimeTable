﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using ContextAwareAndRealTimeTimeTable.EF.UnitOfWork;

namespace ContextAwareAndRealTimeTimeTable.DependencyResolver
{
    public class ModelDependencyResolver: NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IUnitOfWork<>)).To(typeof(UnitOfWork<>));
        }
    }
}
