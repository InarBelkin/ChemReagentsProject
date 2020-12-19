using ChemReagentsProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ChemReagentsProject.NavService
{
    class NavService
    {
        public IRecognizable recognizable { get; }
        public NavigationService  NavServ {get;}
        
        public NavService(Control Source, NavigationService Nav)
        {
            recognizable = Source.DataContext as IRecognizable;
            NavServ = Nav;
        }
    }

    internal static class Navigation
    {
        private static readonly List<NavService> Navs = new List<NavService>();

        public static void AddNav(Control Target, NavigationService NavService)
        {
            Navs.Add(new NavService(Target, NavService));
            Target.Unloaded += Target_Unloaded;
        }

        private static void Target_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Control Sender = sender as Control;
            IRecognizable This = Sender.DataContext as IRecognizable;
            foreach(NavService Item in Navs)
            {
                if(Item.recognizable.GetGuid()==This.GetGuid())
                {
                    Navs.Remove(Item);
                    break;
                }
            }
           
        }

        public static void Navigate(Guid Source, object Target)
        {
            foreach(NavService Item in Navs)
            {
                if(Item.recognizable.GetGuid() == Source)
                {
                    Item.NavServ.Navigate(Target);
                    break;
                }
            }
        }

        public static event EventHandler<IRecognizable> LoadDone;

        public static void LoadDoneInv(Control Source)
        {
            LoadDone?.Invoke(null,Source.DataContext as IRecognizable);
        }
    }
}
