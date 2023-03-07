using FFMpegCore;
using FFMpegCore.Arguments;
using FFMpegCore.Builders.MetaData;
using FFMpegCore.Enums;
using HeBianGu.Base.WpfBase;

namespace HeBianGu.Domain.Converter
{
    public class MetaDataConverterItem : VideoConverterItemBase
    {
        public MetaDataConverterItem(string filePath) : base(filePath)
        {

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
