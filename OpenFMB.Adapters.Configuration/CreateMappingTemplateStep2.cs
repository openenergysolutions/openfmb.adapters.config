using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateMappingTemplateStep2 : UserControl
    {
        private ProfileModel _profileModel;

        public ProfileModel ProfileModel
        {
            get { return _profileModel; }
            set
            {
                if (_profileModel != value)
                {
                    _profileModel = value;

                    LoadGrid();
                }
            }
        }

        public List<Data> SelectedData
        {
            get
            {
                var ds = dataBindingSource.DataSource as BindingList<Data>;

                if (ds != null)
                {
                    return ds.Where(x => x.Selected).ToList();
                }
                return new List<Data>();
            }
        }

        public CreateMappingTemplateStep2()
        {
            InitializeComponent();           
        }

        private void LoadGrid()
        {
            var list = _profileModel?.Topics.Select(x => x.Attributes).Select(y => new Data()
            {
                Label = y.Label,
                Path = y.Path
            }).ToList();

            dataBindingSource.DataSource = new BindingList<Data>(list);
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = filterTextBox.Text.Trim();

                var list = _profileModel.Topics.Where(x => x.Attributes.Name.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0 || x.Attributes.Path.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0).Select(a => a.Attributes);

                var dataList = list.Select(y => new Data()
                {
                    Label = y.Label,
                    Path = y.Path
                });

                dataBindingSource.DataSource = new BindingList<Data>(dataList.ToList());

            }
            catch { }
        }
    }

    public class Data
    {
        public bool Selected { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
    }
}
