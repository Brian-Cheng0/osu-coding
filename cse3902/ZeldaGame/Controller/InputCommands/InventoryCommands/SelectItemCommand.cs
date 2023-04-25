﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeldaGame.Objects;

namespace ZeldaGame
{

    public class SelectItem : ICommand
    {
        public SelectItem()
        {

        }

        public void Execute()
        {
            UIManager.Instance.selectableItemDisplay.SelectItem();
        }
    }
}
