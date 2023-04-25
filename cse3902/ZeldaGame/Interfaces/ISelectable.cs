using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeldaGame
{
    public interface ISelectable : IUpdatable, IDrawable
    {
        public Boolean hasBeenCollected { get; set; }
        public Boolean isInUse { get; set; }
        void ChangeSelectionLocation(int x, int y);
        void SelectItem();
        void DeselectItem();
    }
}
