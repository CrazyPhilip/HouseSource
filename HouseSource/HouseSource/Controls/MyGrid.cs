using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HouseSource.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyGrid : Grid
    {

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(MyGrid));
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(MyGrid));
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(MyGrid), null);

        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(MyGrid), default(IEnumerable<object>), propertyChanged: OnItemsSourcePropertyChanged);


        private int _maxColumns = 2;
        private float _tileHeight = 100;

        public MyGrid()
        {
            //InitializeComponent();
            for (var i = 0; i < MaxColumns; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }
        }



        public static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((MyGrid)bindable).UpdateTiles();
        }



        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public int MaxColumns
        {
            get { return _maxColumns; }
            set { _maxColumns = value; }
        }

        public float TileHeight
        {
            get { return _tileHeight; }
            set { _tileHeight = value; }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public async Task BuildTiles(IEnumerable tiles)
        {
            // Wipe out the previous row definitions if they're there.
            if (RowDefinitions.Any())
            {
                RowDefinitions.Clear();
            }
            Children.Clear();
            if (tiles == null)
            {
                return;
            }
            var enumerable = tiles as IList ?? tiles.Cast<object>().ToArray();
            var numberOfRows = Math.Ceiling(enumerable.Count / (float)MaxColumns);
            HeightRequest = TileHeight * numberOfRows;
            InvalidateLayout();
            for (var i = 0; i < numberOfRows; i++)
            {
                RowDefinitions.Add(new RowDefinition { Height = TileHeight });
            }
            int ItemCount = enumerable.Count;
            int size = (int)numberOfRows * MaxColumns;

            for (var index = 0; index < size; index++)
            {
                var column = index % MaxColumns;
                var row = (int)Math.Floor(index / (float)MaxColumns);
                if (index < ItemCount)
                {
                    var tile = await BuildTile(enumerable[index]);
                    Children.Add(tile, column, row);
                }
                else
                {
                    var tile = await BuildEmptyTile();
                    Children.Add(tile, column, row);
                }
            }
        }

        public async void UpdateTiles()
        {
            await BuildTiles(ItemsSource);
        }

        private async Task<Xamarin.Forms.View> BuildEmptyTile()
        {
            return await Task.Run(() =>
            {
                var content = ItemTemplate?.CreateContent();
                if (!(content is Xamarin.Forms.View) && !(content is ViewCell)) throw new Exception(content.GetType().ToString());
                var buildTile = (content is Xamarin.Forms.View) ? content as Xamarin.Forms.View : ((ViewCell)content).View;
                return buildTile;
            });

        }

        private async Task<Xamarin.Forms.View> BuildTile(object item)
        {
            return await Task.Run(() =>
            {
                var content = ItemTemplate?.CreateContent();
                if (!(content is Xamarin.Forms.View) && !(content is ViewCell)) throw new Exception(content.GetType().ToString());
                var buildTile = (content is Xamarin.Forms.View) ? content as Xamarin.Forms.View : ((ViewCell)content).View;
                buildTile.BindingContext = item;
                var tapGestureRecognizer = new TapGestureRecognizer
                {
                    Command = Command,
                    CommandParameter = item
                };
                buildTile.GestureRecognizers.Add(tapGestureRecognizer);
                return buildTile;
            });

        }

    }
}