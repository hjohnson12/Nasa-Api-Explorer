using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NasaDataExplorer.Helpers
{
    public class DateChangedTriggerBehavior : Trigger<CalendarDatePicker>
    {
        public static readonly DependencyProperty ChangeDateCommandProperty =
            DependencyProperty.RegisterAttached(
                "ChangeDateCommand", typeof(ICommand),
                typeof(CalendarDatePicker), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(ChangeDateCommandProperty); }
            set { SetValue(ChangeDateCommandProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            
            AssociatedObject.Date = DateTimeOffset.Now.AddDays(-1);
            AssociatedObject.DateChanged += AssociatedObject_DateChanged;
        }

        private void AssociatedObject_DateChanged(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            // Invoke the command with the new chosen date
            var newDate = args.NewDate;
            var photoDate = 
                newDate.Value.Year.ToString() + "-" + newDate.Value.Month.ToString() + "-" + newDate.Value.Day.ToString();
            if (Command != null)
                Command.Execute(photoDate);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.DateChanged -= AssociatedObject_DateChanged;
        }
    }
}
