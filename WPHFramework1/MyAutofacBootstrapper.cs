using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Windows;
using System.Reflection;
using System.ComponentModel;
using IContainer = Autofac.IContainer;
using System.Globalization;
using Caliburn.Micro.Autofac;
using MahApps.Metro.Controls.Dialogs;

namespace WPHFramework1
{
    public class MyAutofacBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public MyAutofacBootstrapper()
        {
            Initialize();
        }

        #region Properties

        private IContainer _localContainer;
        protected IContainer LocalContainer
        {
            get { return _localContainer; }
        }

        #endregion

        protected override void Configure()
        {
            //NOTE: If viewmodels don't implement INotifyPropertyChanged (via Screen or PropertyChangedBase or other means)
            // then they don't get registered here.  Results in runtime crash.

            //  Configure container
            var builder = new ContainerBuilder();

            //  Register view models
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                //  must be a type that ends with ViewModel
              .Where(type => type.Name.EndsWith("ViewModel"))
                //  must be in a namespace ending with ViewModels - NOT
                //  .Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("ViewModels"))             

                //  must implement INotifyPropertyChanged (deriving from PropertyChangedBase will statisfy this)
              .Where(type => type.GetInterface(typeof(INotifyPropertyChanged).Name) != null)
                //  registered as self
              .AsSelf()
                //  always create a new one
              .InstancePerDependency();

            // TODO: would be nice to have more granular control, ie., to register some view models with .InstancePerLifetimeScope
            // and some with InstancePerDependency, etc. How to leverage that?  Hmmmm....

            //  Register views
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                //  must be a type that ends with View
              .Where(type => type.Name.EndsWith("View"))
                //  must be in a namespace that ends in Views - NOT
              //.Where(type => !(string.IsNullOrWhiteSpace(type.Namespace)) && type.Namespace.EndsWith("Views"))
                //  registered as self
              .AsSelf()
                //  always create a new one
              .InstancePerDependency();
         
            //  register the single window manager for this container
            builder.Register<IWindowManager>(c => new WindowManager()).InstancePerLifetimeScope();

            //  register the single event aggregator for this container
            builder.Register<IEventAggregator>(c => new EventAggregator()).InstancePerLifetimeScope();

           // Register dialog coordinator static instance
           builder.Register<MahApps.Metro.Controls.Dialogs.DialogCoordinator>(c => MahApps.Metro.Controls.Dialogs.DialogCoordinator.Instance).SingleInstance().As<IDialogCoordinator>();
            
           ConfigureContainer(builder);

           _localContainer = builder.Build();
        }

        protected override object GetInstance(Type service, string key)
        {
             if (String.IsNullOrWhiteSpace(key)) { 
                 object obj;
                 if (LocalContainer.TryResolve(service, out obj)) 
                     return obj; 
             } else { 
                 object obj;
                 if (LocalContainer.TryResolveNamed(key, service, out obj)) 
                     return obj; 
             } 
             throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? service.Name)); 
         } 

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return LocalContainer.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override void BuildUp(object instance)
        {
            LocalContainer.InjectProperties(instance);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            base.OnStartup(sender, e);

            DisplayRootViewFor<ShellViewModel>();
        }

    }
}
