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
            
            bindable.ItemAppearing += this.Bindable_ItemAppearing;
        }

        protected override void OnDetachingFrom(XListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.ItemAppearing -= this.Bindable_ItemAppearing;
        }

        private async void Bindable_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (this.isLoading)
            {
                return;
            }

            var command = InfiniteScroll.GetLoadMoreCommand(this.listView);

            if (command is null)
                return;

            var itemsSource = this.listView.ItemsSource as IEnumerable<object>;
            var appearingItem = e.Item;
            var lastItem = itemsSource.LastOrDefault();

            if (appearingItem.Equals(lastItem))
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
