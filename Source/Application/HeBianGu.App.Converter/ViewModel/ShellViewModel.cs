using FFMpegCore;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using HeBianGu.Control.PropertyGrid;
using HeBianGu.Domain.Converter;
using HeBianGu.Service.AppConfig;
using HeBianGu.Systems.Print;
using HeBianGu.Systems.Project;
using HeBianGu.Systems.WinTool;
using HeBianGu.Systems.XmlSerialize;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Serialization;

namespace HeBianGu.App.Converter
{
    internal class ShellViewModel : NotifyPropertyChanged
    {
        public ShellViewModel()
        {
            var types = typeof(GroupBase).Assembly.GetTypes().Where(x => typeof(GroupBase).IsAssignableFrom(x)).Where(x => x.GetCustomAttribute<DisplayerAttribute>() != null);
            this.Collection = types.Where(x => x.IsClass && !x.IsAbstract).Select(x => Activator.CreateInstance(x)).OfType<GroupBase>().OrderBy(x => x.Order).ToObservable();
            this.SelectedItem = this.Collection.FirstOrDefault();
            this.Collection.Add(new CrawlerConverterGroup());
        }
        private ObservableCollection<GroupBase> _collection = new ObservableCollection<GroupBase>();
        /// <summary> 说明  </summary>
        public ObservableCollection<GroupBase> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged();
            }
        }


        private GroupBase _selectedItem;
        /// <summary> 说明  </summary>
        public GroupBase SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }
    }
}
