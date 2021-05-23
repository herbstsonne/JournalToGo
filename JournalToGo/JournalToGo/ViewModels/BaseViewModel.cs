using JournalToGo.Models;
using JournalToGo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace JournalToGo.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged, IDisposable
    {
        private IDataStore<JournalEntry> _dataStore;
        protected readonly JournalingContext _journalContext;

        public BaseViewModel()
        {
            _journalContext = new JournalingContext();
        }

        //public IDataStore<Models.JournalEntry> DataStore 
        //{
        //    get { return _dataStore == null ? _dataStore = DependencyService.Get<IDataStore<Models.JournalEntry>>() : _dataStore; }
        //    set { _dataStore = value; }
        //}

    bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public void Dispose()
        {
            _journalContext.Dispose();
        }
    }
}
