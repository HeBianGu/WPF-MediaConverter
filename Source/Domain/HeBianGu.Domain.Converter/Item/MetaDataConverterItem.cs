using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Builders.MetaData;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;
using System;

namespace HeBianGu.Domain.Converter
{
    public class MetaDataConverterItem : VideoConverterItemBase
    {
        public MetaDataConverterItem(string filePath, Action<ConverterItemBase> builder) : base(filePath, builder)
        {
            this.UseOutToolCommadNames = $"{nameof(PlayOutputCommand)},{nameof(SetMetaDataCommand)},{nameof(OpenCommand)},{nameof(DeleteCommand)},{nameof(ViewArgumentsCommand)},{nameof(DeleteFileCommand)}";
        }

        protected override FFMpegArgumentProcessor CreateProcessor()
        {
            var metaData = OutputMediaInfo.Meta.CreateMetaData();
            var text = MetaDataSerializer.Instance.Serialize(metaData);
            var arg = new MetaDataArgument(text);
            return FFMpegArguments.FromFileInput(FilePath).AddMetaData(metaData).OutputToFile(OutputPath, true, x =>
            {
                x.CopyChannel(Channel.All);
                //x.WithArgument(arg);
            });
        }
    }
}
