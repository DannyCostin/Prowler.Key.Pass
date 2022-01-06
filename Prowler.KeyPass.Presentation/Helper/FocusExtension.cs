using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Prowler.KeyPass.Presentation.Helper
{
    public static class FocusExtension
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused", typeof(bool), typeof(FocusExtension),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var fe = d as FrameworkElement;
            
            bool.TryParse(e.NewValue?.ToString(), out bool value);

            if(fe != null && value)
            {
                fe.Focus();
                Keyboard.ClearFocus();
                Keyboard.Focus(fe);                                

                var tb = fe as TextBoxBase;
                if (tb != null)
                {
                    // Select all text to be ready for replacement.
                    tb.SelectAll();
                }
            }           
        }
    }
}
