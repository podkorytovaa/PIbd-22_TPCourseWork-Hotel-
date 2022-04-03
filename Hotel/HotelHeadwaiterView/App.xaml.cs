using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HotelBusinessLogic.BusinessLogics;
using HotelContracts.BusinessLogicsContracts;
using HotelContracts.StoragesContracts;
using HotelContracts.ViewModels;
using HotelDatebaseImplement.Implements;
using HotelBusinessLogic.OfficePackage;
using HotelBusinessLogic.OfficePackage.Implements;
using Unity;
using Unity.Lifetime;

namespace HotelHeadwaiterView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IUnityContainer container = null;
        public static HeadwaiterViewModel Headwaiter { get; set; }
        public static IUnityContainer Container
        {
            get
            {
                if (container == null)
                {
                    container = BuildUnityContainer();
                }
                return container;
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var authorizationWindow = Container.Resolve<SignInWindow>();
            authorizationWindow.ShowDialog();
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IHeadwaiterStorage, HeadwaiterStorage>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<IConferenceStorage, ConferenceStorage>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<ISeminarStorage, SeminarStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILunchStorage, LunchStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRoomStorage, RoomStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRoomerStorage, RoomerStorage>(new HierarchicalLifetimeManager());
            
            currentContainer.RegisterType<IHeadwaiterLogic, HeadwaiterLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IConferenceLogic, ConferenceLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISeminarLogic, SeminarLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ILunchLogic, LunchLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRoomLogic, RoomLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRoomerLogic, RoomerLogic>(new HierarchicalLifetimeManager());
            //currentContainer.RegisterType<IReportLogic, ReportLogic>(new HierarchicalLifetimeManager());


            currentContainer.RegisterType<HeadwaiterAbstractSaveToPdf, HeadwaiterSaveToPdf>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<HeadwaiterAbstractSaveToExcel, HeadwaiterSaveToExcel>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<HeadwaiterAbstractSaveToWord, HeadwaiterSaveToWord>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
