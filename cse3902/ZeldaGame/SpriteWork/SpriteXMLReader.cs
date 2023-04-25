using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace ZeldaGame
{
    public class SpriteXMLReader
    {
        public XmlReader reader;
        public XmlReaderSettings settings;

        public SpriteXMLReader()
        {
            settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            String dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            reader = XmlReader.Create(dir + "//Content//SpriteXML.xml", settings);
        }

        public void populateAllSpriteLists()
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Text)
                {
                    string[] spriteInfo = reader.Value.Split(' ');

                    SpriteFactory.Instance.textureNames.Add(spriteInfo[0]);
                    int numberOfFrames = Convert.ToInt32(spriteInfo[1]);

                    List<Rectangle> frames = new List<Rectangle>();

                    int counter = 0;
                    int[] frame = new int[4];
                    int idx = 0;

                    for (int i = 2; i < (numberOfFrames * 4) + 2; i++)
                    {
                        frame[counter] = Convert.ToInt32(spriteInfo[i]);
                        counter++;
                        if (counter == 4)
                        {
                            frames.Add(new Rectangle(frame[0], frame[1], frame[2], frame[3]));
                            counter = 0;
                        }
                        if (i == (numberOfFrames * 4) + 1)
                        {
                            idx = i + 1;
                        }
                    }
                    SpriteFactory.Instance.arraysOfFrames.Add(frames);
                    SpriteFactory.Instance.delays.Add(Convert.ToInt32(spriteInfo[idx]));
                    SpriteFactory.Instance.scales.Add(Convert.ToInt32(spriteInfo[idx + 1]));
                }
            }
        }
    } 
}