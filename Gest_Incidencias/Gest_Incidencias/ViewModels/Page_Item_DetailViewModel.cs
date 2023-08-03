﻿using Gest_Incidencias.Models;
using Gest_Incidencias.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gest_Incidencias.ViewModels
{
    public class Page_Item_DetailViewModel : BaseViewModel /*BindableBase*/
    {
        #region Variables
        private readonly Services.IMessageService _messageService;
        private readonly INavigationService _navigationService; //public INavigation Navigation { get; set; }
        #endregion

        #region Properties
        public string Tipo { get; set; }

        private int _id;
        public int Id {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _title;
        public string Title {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private string _description;
        public string Description {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private DateTime _dateCreation;
        public DateTime DateCreation
        {
            get { return _dateCreation; }
            set { SetProperty(ref _dateCreation, value); }
        }
        private string _dateModification;
        public string DateModification
        {
            get { return _dateModification; }
            set { SetProperty(ref _dateModification, value); }
        }
        private string _dateStarting;
        public string DateStarting
        {
            get { return _dateStarting; }
            set { SetProperty(ref _dateStarting, value); }
        }
        private string _dateFinish;
        public string DateFinish
        {
            get { return _dateFinish; }
            set { SetProperty(ref _dateFinish, value); }
        }
        private string _dateDeleted;
        public string DateDeleted
        {
            get { return _dateDeleted; }
            set { SetProperty(ref _dateDeleted, value); }
        }
        /*private bool _isDeletedProperty = false;
        public bool IsDeletedProperty
        {
            get => _isDeletedProperty;
            set => SetProperty(ref _isDeletedProperty, value);
        }*/
        private bool _isDeleted;
        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { SetProperty(ref _isDeleted, value); }
        }
        public bool IsDeletedStateElement
        {
            get
            {
                Console.WriteLine("IsDeletedStateElement? " + IsDeleted);
                if (IsDeleted) return true;
                else return false;
            }
        }
        private bool _isAvailable;
        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { SetProperty(ref _isAvailable, value); }
        }
        private bool _isAvailableProperty = false;
        public bool IsAvailableProperty
        {
            get => _isAvailableProperty;
            set => SetProperty(ref _isAvailableProperty, value);
        }
        #endregion


        #region Commands
        public DelegateCommand IniciarCommand { private set; get; }
        public DelegateCommand FinalizarCommand { private set; get; }
        public DelegateCommand RenovarCommand { private set; get; }
        public DelegateCommand DeleteCommand { private set; get; }
        public DelegateCommand CancelCommand { private set; get; }
        public DelegateCommand SaveCommand { private set; get; }

        private DelegateCommand _navigationCommand;
        public DelegateCommand NavigateCommand =>
            _navigationCommand ?? (_navigationCommand = new DelegateCommand(ExecuteNavigationCommand));
        #endregion


        #region Constructor
        public Page_Item_DetailViewModel(INavigationService navigationService) : base(navigationService)
        //public Page_Item_DetailViewModel(INavigationService navigationService) : base(navigationService,"")
        {
            Title = " VIEW Page_Item_DetailViewModel";
            _navigationService = navigationService;
            _messageService = DependencyService.Get<Services.IMessageService>();

            /*
            _messageService = DependencyService.Get<Services.IMessageService>();

            IniciarCommand = new DelegateCommand(Iniciar);
            FinalizarCommand = new DelegateCommand(Finalizar);
            RenovarCommand = new DelegateCommand(Renovar);
            DeleteCommand = new DelegateCommand(Delete);
            CancelCommand = new DelegateCommand(Return);
            SaveCommand = new DelegateCommand(Save);

            Title = Parameters.EditingNote.Title;
            Description = Parameters.EditingNote.Description;
            IsAvailable = Parameters.EditingNote.IsAvailable;
            DateCreation = Parameters.EditingNote.DateCreation;
            DateModification = Parameters.EditingNote.DateModification;
            DateStarting = Parameters.EditingNote.DateStarting;
            DateFinish = Parameters.EditingNote.DateFinish;
            DateDeleted = Parameters.EditingNote.DateDeleted;

            Tipo = Tipo;
            Parameters.EditingNote.IsSelected = false;
            */
        }
        /*public Page_Item_DetailViewModel(INavigation navigation)
        {
            Navigation = navigation;
            _messageService = DependencyService.Get<Services.IMessageService>();

            IniciarCommand = new DelegateCommand(Iniciar);
            FinalizarCommand = new DelegateCommand(Finalizar);
            RenovarCommand = new DelegateCommand(Renovar);
            DeleteCommand = new DelegateCommand(Delete);
            CancelCommand = new DelegateCommand(Return);
            SaveCommand = new DelegateCommand(Save);

            Title = Parameters.EditingNote.Title;
            Description = Parameters.EditingNote.Description;
            IsAvailable = Parameters.EditingNote.IsAvailable;
            DateCreation = Parameters.EditingNote.DateCreation;
            DateModification = Parameters.EditingNote.DateModification;
            DateStarting = Parameters.EditingNote.DateStarting;
            DateFinish = Parameters.EditingNote.DateFinish;
            DateDeleted = Parameters.EditingNote.DateDeleted;

            Tipo = Tipo;
            Parameters.EditingNote.IsSelected = false;
        }*/
        public override void Initialize(INavigationParameters parameters)
        {
            Console.WriteLine(" VIEW Page_Item_DetailViewModel");
            base.Initialize(parameters);
        }
        #endregion


        #region CommandsFunctions
        async void ExecuteNavigationCommand()
        {
            Console.WriteLine("Click Boton NAVIGATE");
            await _navigationService.NavigateAsync("ViewB");
        }

        async void Iniciar()
        {
            // hay nota, y no esta ya iniciada
            if (Parameters.EditingNote != null
                && Parameters.EditingNote.InProgress != true
                //&& Parameters.EditingNote.IsAvailable
                )
            {
                Console.WriteLine(" INICIADO");
                IsAvailableProperty = false;
                Parameters.EditingNote.IsAvailable = false;
                Parameters.EditingNote.IsFinished = false;
                Parameters.EditingNote.InProgress = true;
                Parameters.EditingNote.Tipo = "Iniciadas";
                Parameters.EditingNote.DateStarting = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");
                Console.WriteLine(Parameters.EditingNote.DateStarting);

                await App.Database.SaveNoteAsync(Parameters.EditingNote);
                //await Navigation.PushAsync(new MainPage());
            }
            else
            {
                Console.WriteLine("NOTA YA Iniciada");
            }
        }

        async void Finalizar()
        {
            // hay nota, y no esta ya finalizada
            if (Parameters.EditingNote != null
                /*&& Parameters.EditingNote.IsFinished != true*/
                //&& Parameters.EditingNote.IsAvailable
                )
            {
                Console.WriteLine(" FINALIZADO");
                IsAvailableProperty = true;
                //Parameters.EditingNote.IsSelected = false;
                Parameters.EditingNote.IsAvailable = false;
                Parameters.EditingNote.InProgress = false;
                Parameters.EditingNote.IsFinished = true;
                Parameters.EditingNote.IsDeleted = true;
                Parameters.EditingNote.Tipo = "Finalizadas";
                Parameters.EditingNote.DateFinish = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

                await App.Database.SaveNoteAsync(Parameters.EditingNote);
                //await Navigation.PushAsync(new MainPage());
            }
            else
            {
                Console.WriteLine("NOTA Finalizar - Parameters.EditingNote == NULL ");
            }
        }

        async void Renovar()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(Parameters.EditingNote.Title) && !string.IsNullOrWhiteSpace(Parameters.EditingNote.Description))
                {
                    Parameters.EditingNote.IsAvailable = true;
                    Parameters.EditingNote.IsDeleted = false;
                    Parameters.EditingNote.IsFinished = false;
                    Parameters.EditingNote.InProgress = false;
                    Parameters.EditingNote.DateDeleted = "Renovado";
                    Parameters.EditingNote.Tipo = "Disponibles";

                    try
                    {
                        await App.Database.SaveNoteAsync(Parameters.EditingNote);
                        await NavigationService.NavigateAsync("MainPage");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    await _messageService.ShowAsync(message: "Rellene Titulo y Descripcion");
            }
            catch (Exception ex)
            {
                await _messageService.ShowAsync(message: "Excepcion: " + ex);
            }
            //await Navigation.PopAsync(); // no deja la lista actualizada
            await NavigationService.NavigateAsync("MainPage");
        }

        async void Delete()
        {
            Console.WriteLine("BORRAR: " + Parameters.EditingNote.Title);

            Parameters.EditingNote.IsAvailable = false;
            Parameters.EditingNote.IsDeleted = true;
            Parameters.EditingNote.Tipo = "Borradas";
            //Parameters.EditingNote.IsFinished = true;
            Parameters.EditingNote.DateDeleted = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

            try {
                if (!string.IsNullOrWhiteSpace(Parameters.EditingNote.Title) && !string.IsNullOrWhiteSpace(Parameters.EditingNote.Description)) {
                    try {
                        await App.Database.SaveNoteAsync(Parameters.EditingNote);
                        await NavigationService.NavigateAsync("MainPage");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    await _messageService.ShowAsync(message: "Titulo o Descripcion vacios");
            }
            catch (Exception ex)
            {
                await _messageService.ShowAsync(message: "Excepcion: " + ex);
            }
            //await Navigation.PopAsync(); // no deja la lista actualizada
            await NavigationService.NavigateAsync("MainPage");
        }

        async void Return()
        {
            //await Navigation.PopAsync();
            await NavigationService.GoBackAsync();
            //await NavigationService.NavigateAsync("MainPage");
        }

        async void Save()
        {
            Console.WriteLine("Guardar: " + Title);

            Parameters.EditingNote.Title = Title;
            Parameters.EditingNote.Description = Description;
            Parameters.EditingNote.IsAvailable = IsAvailable;
            //Parameters.EditingNote.DateCreation = DateCreation;
            //Parameters.EditingNote.Tipo = "Disponibles";
            Parameters.EditingNote.DateModification = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");
            ////Parameters.EditingNote.DateModification = DateTime.UtcNow.ToString();
            Parameters.EditingNote.IsSelected = false; //ConstructorLista=> Notes.ForEach(note => note.IsSelected = false);

            try {
                if (!string.IsNullOrWhiteSpace(Parameters.EditingNote.Title) &&
                    !string.IsNullOrWhiteSpace(Parameters.EditingNote.Description)) {
                    try {
                        await App.Database.SaveNoteAsync(Parameters.EditingNote);
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    await _messageService.ShowAsync(message: "Titulo o Descripcion vacios");
            }
            catch (Exception ex)
            {
                await _messageService.ShowAsync(message: "Excepcion: "+ex);
            }
            //await Navigation.PopAsync(); // no deja la lista actualizada
            await NavigationService.NavigateAsync("MainPage");
        }
        #endregion

    }
}
