using HouseSource.Behaviors;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Markup;

namespace HouseSource.Controls
{
    public class MyPicker : Picker
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(Command), typeof(MyPicker), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MyPicker), null);

        public Command Command
        {
            get => (Command)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public MyPicker()
        {
            this.HorizontalOptions = LayoutOptions.Center;
            this.VerticalOptions = LayoutOptions.Center;
            this.FontSize = 12;

            this.Behaviors.Add(new EventToCommandBehavior()
            {
                Command = Command,
                CommandParameter = CommandParameter,
                EventName = "SelectedIndexChanged"
            });

        }

    }
}
