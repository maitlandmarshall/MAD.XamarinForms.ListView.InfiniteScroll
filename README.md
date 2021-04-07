# MAD.XamarinForms.ListView.InfiniteScroll

Implements a Xamarin Forms Behavior which allows a ListView to request more data as it reaches near the end of its scroll view.

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MAD.XamarinForms.ListView.InfiniteScroll.TestApp.MainPage"
             xmlns:scroll="clr-namespace:MAD.XamarinForms.ListView.InfiniteScroll;assembly=MAD.XamarinForms.ListView.InfiniteScroll">

    <ListView ItemsSource="{Binding Source}"
              CachingStrategy="RecycleElement"
              scroll:InfiniteScroll.LoadMoreCommand="{Binding LoadMoreCommand}"
              RowHeight="50"/>

</ContentPage>
```

```cs
public partial class MainPage : ContentPage
    {
        private int currentPage = 0;

        public MainPage()
        {
            InitializeComponent();

            this.Source = new ObservableCollection<string>(this.GetPageData());
            this.LoadMoreCommand = this.OnLoadMore;

            this.Content.BindingContext = this;
        }

        public ICollection<string> Source { get; }
        public Func<Task> LoadMoreCommand { get; }

        private IEnumerable<string> GetPageData()
        {
            var src = new List<string>();

            for (int i = 50 * currentPage; i < (currentPage + 1) * 50; i++)
            {
                src.Add(i.ToString());
            }

            return src;
        }

        private async Task OnLoadMore()
        {
            this.currentPage++;

            var newPageData = this.GetPageData();
            
            foreach (var pd in newPageData)
            {
                await Task.Delay(10);
                this.Source.Add(pd);
            }
        }
    }
```
