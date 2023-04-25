using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ZeldaGame
{
    public class SoundXMLReader
    {
        public XmlReader reader;
        public XmlReaderSettings settings;

        public SoundXMLReader()
        {
            settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            String dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            reader = XmlReader.Create(dir + "//Content//SoundXML.xml", settings);
        }

        public void populateAllSoundLists()
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    string[] spriteInfo = reader.Value.Split(' ');
                    SoundFactory.Instance.soundNames.Add(spriteInfo[0]);                    
                }
            }
        }
    } 
}