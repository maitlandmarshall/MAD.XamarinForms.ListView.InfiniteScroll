using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MAD.XamarinForms.ListView.InfiniteScroll.TestApp
{
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
}
