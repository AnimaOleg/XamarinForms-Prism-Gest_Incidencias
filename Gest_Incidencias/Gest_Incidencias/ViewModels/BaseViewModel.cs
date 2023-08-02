using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;


namespace Gest_Incidencias.ViewModels
{
    public abstract class BaseViewModel : BindableBase, INavigationAware, IDestructible, IInitialize
    {
        protected INavigationService NavigationService { get; set; }

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }


        // Called when we inflate the SampleViewModel object
        // This is then added as the BindingContext of the View
        // NOTE: You CAN NOT access Navigation Parameters from here!!!!
        public BaseViewModel(INavigationService navigationService /*, string tipo*/)
        {
        }

        // Called before the View (Xamarin.Forms Page) is pushed onto the Navigation Stack
        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        // Called when the View is Navigated away from
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        // Called any time the View is is Navigated to, or back to... and AFTER Initialize...
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        // IDestructible
        public virtual void Destroy()
        {
        }
    }

}

