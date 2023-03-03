﻿using HeBianGu.Base.WpfBase;

namespace HeBianGu.App.Converter
{
    internal class DataSourceLocator
    {
        public DataSourceLocator()
        {
            ServiceRegistry.Instance.Register<ShellViewModel>();
        }

        public ShellViewModel ShellViewModel => ServiceRegistry.Instance.GetInstance<ShellViewModel>();
    }

}
