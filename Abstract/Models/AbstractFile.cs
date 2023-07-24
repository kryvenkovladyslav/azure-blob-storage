using System;

namespace Abstract.Models
{
    public abstract class AbstractFile
    {
        public Guid ID { get; private set; }

        public string Type { get; private set; }

        public string Uri { get; private set; }

        public string Name { get; private set; }

        public AbstractFile(string type, string uri, string name, Guid? id = null)
        {
            this.ID = id.HasValue ? id.Value : Guid.NewGuid();
            this.Type = type;
            this.Uri = uri;
            this.Name = name;
        }
    }
}