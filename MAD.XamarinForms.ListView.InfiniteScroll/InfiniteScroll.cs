using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MAD.XamarinForms.ListView.InfiniteScroll
{
    public static class InfiniteScroll
    {
        public static BindableProperty LoadMoreCommandProperty = BindableProperty.CreateAttached("LoadMoreCommand", typeof(Func<Task>), typeof(InfiniteScroll), null, propertyChanged: OnLoadMoreCommandChanged);

        public static Func<Task> GetLoadMoreCommand(BindableObject bindableObject)
        {
            return bindableObject.GetValue(LoadMoreCommandProperty) as Func<Task>;
        }

        public static void SetLoadMoreCommand(BindableObject bindableObject, Func<Task> value)
        {
            bindableObject.SetValue(LoadMoreCommandProperty, value);
        }

        private static void OnLoadMoreCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;

            if (view is null)
                return;

            view.Behaviors.Add(new InfiniteScrollBehavior());
        }
    }
}
