using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NasaApiExplorer.Behaviors
{
    public class DateChangedTriggerBehavior : Trigger<CalendarDatePicker>
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(CalendarDatePicker), new PropertyMetadata(0));

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Date = DateTimeOffset.Now;
            AssociatedObject.DateChanged += AssociatedObject_DateChanged;
        }

        private void AssociatedObject_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            // Invoke the command with the new chosen date
            if (Command != null)
                Command.Execute(args.NewDate);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.DateChanged -= AssociatedObject_DateChanged;
        }
    }
}
