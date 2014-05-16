using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading.Tasks;
using MapReduce.Storage;
using MapReduce.Storage.Configurations;


namespace MapReduce.Infrastructure
{
    /// <summary>
    /// Хранилище текстовых блоков данных
    /// </summary>
    public abstract class TextBlockRepository : MapReduce.Storage.BaseRepository<MapReduce.Storage.TextBlock>
    {
        protected TextBlockRepository(JobConfiguration configuration)
        {
            Contract.Requires<ArgumentNullException>(configuration != null);
            Contract.Requires<ArgumentNullException>(configuration.StorageConfiguration != null);
            Contract.Requires<ArgumentNullException>(configuration.StorageConfiguration.InputFolder != null);
            Contract.Requires<ArgumentNullException>(configuration.StorageConfiguration.OutputFolder != null);

            _configuration = configuration;
        }

        private readonly JobConfiguration _configuration;

        protected virtual string FileMask
        {
            get { return "*"; }
        }


        public override IEnumerable<TextBlock> Get()
        {
            IEnumerable<TextBlock> blocks = null;

            IEnumerable<string> files = GetFiles(_configuration.StorageConfiguration.InputFolder, FileMask);
            if (files != null)
                blocks = ReadBlocks(files);

            return blocks;
        }


        private static IEnumerable<String> GetFiles(string path, string fileMask)
        {
            Contract.Requires<ArgumentNullException>(path != null);
            Contract.Requires<ArgumentNullException>(fileMask != null);

            return Directory.GetFiles(path, fileMask);
        }

        private static IEnumerable<TextBlock> ReadBlocks(IEnumerable<String> files)
        {
            Contract.Requires<ArgumentNullException>(files != null);

            var blocks = new ConcurrentBag<TextBlock>();
            Parallel.ForEach(files,
                             file => blocks.Add(new TextBlock(file, File.ReadAllText(file))));

            return blocks;
        }
    }
}