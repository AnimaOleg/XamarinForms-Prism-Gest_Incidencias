﻿using Gest_Incidencias.ViewModels;
using Prism.Ioc;
using Prism.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gest_Incidencias.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Creation : ContentPage
    {
        #region Variables
        private INavigationService _navigationService;
        #endregion


        #region Constructor
        public Creation()
        {
            this._navigationService = ContainerLocator.Container.Resolve<INavigationService>();
            InitializeComponent();
        }
        #endregion


        #region OnAppearing
        protected override void OnAppearing()
        {
            BindingContext = new Creation_ViewModel(_navigationService);
            base.OnAppearing();
        }
        #endregion


    }
}