﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace ResourceLibrary
{
    public class CustomResources 
    {
        public static ComponentResourceKey SadTileBrushKey
        {
            get
            {
                return new ComponentResourceKey(
                  typeof(CustomResources), "SadTileBrush");
            }
        }
    }
}
