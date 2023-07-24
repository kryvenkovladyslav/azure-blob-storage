using System;

namespace Abstract.Models
{
    public class Image : AbstractFile
    {
        public Image(string type, string uri, string name, Guid? id = null) : base(type, uri, name, id)
        { }
    }
}