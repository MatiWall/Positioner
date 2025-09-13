
using Positioner.Repositories;
using Positioner.Serives;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Positioner.Repositories;
using Positioner.Models;
namespace Positioner.View.UserControls
{
    
    public partial class TemplateTable : UserControl
    {
        private TemplateItem _selectedItem;
        private Dispatcher dispatcher = new Dispatcher();
        private FileEntityRepository entityRepository;
        public TemplateTable()
        {
            InitializeComponent();
            ObservableCollection<TemplateItem> items = new ObservableCollection<TemplateItem>();
            dispatcher.Register(new BrowserSetupServiceV1());

            entityRepository = new FileEntityRepository("./Data");

            List <IEntity> entities = entityRepository.GetAll();

            foreach (IEntity entity in entities) {
                items.Add(
                    new TemplateItem{ 
                        Name = entity.Metadata.Name, 
                        CreatedTimestamp = entity.Metadata.CreatedTimestamp, 
                        UpdatedTimestamp = entity.Metadata.UpdatedTimestamp,
                        Id = entity.Metadata.Id
                    }
                    );
            }

            TemplateDataGrid.ItemsSource = items;



        }

        private void TemplateDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TemplateDataGrid.SelectedItem is TemplateItem item) { 
                _selectedItem = item;
            }
            else
            {
                _selectedItem = null;
                TemplateUseBtn.IsEnabled = false;
            }
        }

        private void TemplateUseBtn_Click(object sender, RoutedEventArgs e) {
            if (_selectedItem == null)
            {
                MessageBox.Show("Please chose a template");
            }

            dispatcher.Dispatch(
                entityRepository.Get(_selectedItem.Id)
             );
        }
    }

    public class TemplateItem
    {
        public string Name { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public string Id { get; set; }
    }

}
