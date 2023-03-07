namespace FFMpegCore.Arguments
{
    public class MapMetaDataArgument : IInputArgument, IDynamicArgument
    {
        private readonly string _metaDataContent;
        private readonly string _tempFileName = Path.Combine(GlobalFFOptions.Current.TemporaryFilesFolder, $"metadata_{Guid.NewGuid()}.txt");

        public MapMetaDataArgument(string metaDataContent)
        {
            _metaDataContent = metaDataContent;
        }

        public string Text => GetText(null);

        public Task During(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public void Pre() => File.WriteAllText(_tempFileName, _metaDataContent);

        public void Post() => File.Delete(_tempFileName);

        public string GetText(IEnumerable<IArgument>? arguments)
        {
            arguments ??= Enumerable.Empty<IArgument>();

            var index = arguments
                .TakeWhile(x => x != this)
                .OfType<IInputArgument>()
                .Count();

            return $"-i \"{_tempFileName}\" -map_metadata {index}";
        }
    }

    public class MetaDataArgument : IInputArgument
    {
        private readonly string _metaDataContent;

        public MetaDataArgument(string metaDataContent)
        {
            _metaDataContent = metaDataContent;
        }

        public string Text => GetText(); 

        public Task During(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public void Pre() { }

        public void Post() { }

        public string GetText()
        {
            return $"-metadata {_metaDataContent}";
        }
    }
}
