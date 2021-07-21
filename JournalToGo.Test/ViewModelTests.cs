using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using FakeItEasy;
using Google.Apis.Util.Store;
using JournalToGo.DailyEntries;
using JournalToGo.Login;
using JournalToGo.Test.Mocks;
using JournalToGo.NewEntries;

namespace JournalToGo.Test
{
    public class ViewModelTests
    {
        private IPlatformServices _platformServicesFake;
        private IDataStore _dataStore;

        public ViewModelTests(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [SetUp]
        public void Setup()
        {
            _platformServicesFake = A.Fake<IPlatformServices>();
            Device.PlatformServices = _platformServicesFake;
        }

        [Test]
        public void When_login_selected_go_to_entriespage()
        {
            var _loginViewModel = Substitute.For<LoginViewModel>();
            Assert.IsTrue(_loginViewModel.LoginCommand.CanExecute(null));
        }

        [Test]
        public async Task Check_if_new_entry_is_created()
        {
            var _viewModel = Substitute.For<NewEntryViewModel>();

            _dataStore = new MockDataStore();
            var entriesBefore = await _dataStore.GetEntriesAsync();

            Assert.That(entriesBefore.Count(), Is.EqualTo(6));

            _viewModel.SaveCommand.Execute(null);

            var entriesAfter = await _dataStore.GetEntriesAsync();

            Assert.That(entriesAfter.Count(), Is.EqualTo(7));
        }

        [Test]
        public async Task Check_if_entry_was_updated()
        {
            var viewModel = Substitute.For<DailyEntryViewModel>();
            _dataStore = new MockDataStore();

            var allEntries = await _dataStore.GetEntriesAsync();
            var firstEntry = allEntries.FirstOrDefault();

            Assert.That(firstEntry.Day, Is.EqualTo("01.05.2021"));
            Assert.That(firstEntry.Headline, Is.EqualTo("Glückstag"));
            Assert.That(firstEntry.DailyThoughtsText, Is.EqualTo("Dies ist mein Glückstag."));

            viewModel.ItemId = firstEntry.Id;
            viewModel.Day = new DateTime(2021, 6, 1).ToShortDateString();
            viewModel.Headline = "test";
            viewModel.DailyThoughtsText = "test test";

            viewModel.SaveEntryCommand.Execute(null);

            var firstEntryNew = await _dataStore.GetEntryAsync(viewModel.ItemId);
            
            Assert.That(firstEntryNew.Day, Is.EqualTo(viewModel.Day));
            Assert.That(firstEntryNew.Headline, Is.EqualTo("test"));
            Assert.That(firstEntryNew.DailyThoughtsText, Is.EqualTo("test test"));
        }
    }
}
