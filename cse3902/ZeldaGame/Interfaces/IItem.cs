using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ZeldaGame
{
    public interface IItem
    {
        int Price { get; set; }
        Boolean InUse { get; set; }
        void CollectItem();
        void Use();
        void Impact();
        
    }
}