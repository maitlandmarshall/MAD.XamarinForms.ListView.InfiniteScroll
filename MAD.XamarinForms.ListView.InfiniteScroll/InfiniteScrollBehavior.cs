using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

using XListView = Xamarin.Forms.ListView;

namespace MAD.XamarinForms.ListView.InfiniteScroll
{
    public class InfiniteScrollBehavior : Behavior<XListView>
    {
        private XListView listView;
        private bool isLoading = false;

        protected override void OnAttachedTo(XListView bindable)
        {
            base.OnAttachedTo(bindable);

            this.listView = bindable;      
            
            bindable.Scrolled += this.Bindable_Scrolled;
        }

        protected override void OnDetachingFrom(XListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.Scrolled -= this.Bindable_Scrolled;
        }

        private async void Bindable_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            var command = InfiniteScroll.GetLoadMoreCommand(this.listView);

            if (command is null)
                return;

            var itemsSource = this.listView.ItemsSource as IEnumerable<object>;
            var itemsSourceCount = itemsSource.Count();

            var maxScrollY = this.listView.RowHeight * itemsSourceCount - this.listView.Height;

            if (e.ScrollY >= maxScrollY * 0.85)
            {
                this.isLoading = true;

                try
                {
                    await command();
                }
                finally
                {
                    this.isLoading = false;
                }
            }
        }
    }
}
