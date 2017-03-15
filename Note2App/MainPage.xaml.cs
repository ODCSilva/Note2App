using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Note2App {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            SystemNavigationManager current = SystemNavigationManager.GetForCurrentView();
            current
                .AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Event delegate called when the aboutButton is clicked.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private void OnAboutButtonClicked(object sender, RoutedEventArgs e) {
            //ContentDialog aboutDialog = new ContentDialog() {
            //    Title = "About Note2: The App",
            //    Content = "Author: Omar Silva",
            //    PrimaryButtonText = "OK",
            //};

            //ContentDialogResult result = await aboutDialog.ShowAsync();

            Frame.Navigate(typeof(AboutPage));
        }

        /// <summary>
        /// Event delegate called when the exitButton is clicked.
        /// </summary>
        /// <param name="sender">The object that triggered the event.</param>
        /// <param name="e">Event arguments.</param>
        private async void OnExitButtonClicked(object sender, RoutedEventArgs e) {
            ContentDialog exitDialog = new ContentDialog() {
                Title = "Really Exit?",
                Content = "Are you sure you want to exit?",
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "Cancel"
            };

            ContentDialogResult result = await exitDialog.ShowAsync();

            if (result == ContentDialogResult.Primary) {
                CoreApplication.Exit();
            }
        }

        private void OnSearchBoxTextChanged(object sender, TextChangedEventArgs e) {
            //PageDataContext context = (PageDataContext)this.DataContext;

            //if (!string.IsNullOrEmpty(searchBox.Text)) {
                
            //    ObservableCollection<NoteModel> filtered = new ObservableCollection<NoteModel>();

            //    foreach (NoteModel note in context.Notes) {
            //        if (note.Title.Contains(searchBox.Text)) {
            //            filtered.Add(note);
            //        }
            //    }

            //    notesListView.ItemsSource = filtered;
            //}
            //else {
            //    notesListView.ItemsSource = context.Notes;
            //}
        }
    }
}
