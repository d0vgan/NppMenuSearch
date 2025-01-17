﻿using System;
using NppPluginNET;

namespace NppMenuSearch
{
    public class MenuItem : HierarchyItem
    {
        public string Shortcut;
        public uint CommandId;
        public IntPtr NativeIcon;

        public MenuItem(IntPtr hmenu)
        {
            Text = "";
            Shortcut = "";
            CommandId = 0;

            int count = Win32.GetMenuItemCount(hmenu);
            for (int i = 0; i < count; ++i)
            {
                string text = Win32.GetMenuItemString(hmenu, (uint)i, true);
                if (text == null)
                    continue;

                uint id = Win32.GetMenuItemId(hmenu, (uint)i, true);
                IntPtr sub = Win32.GetSubMenu(hmenu, (uint)i, true);

                MenuItem item = new MenuItem(sub);
                item.Text = text.Before("\t");
                item.Shortcut = text.After("\t");
                item.CommandId = id;
                item.NativeIcon = Win32.GetMenuItemBitmap(hmenu, (uint)i, true);
                AddItem(item);
            }
        }

        public override string ToString()
        {
            string result = base.ToString();

            if (Shortcut.Length > 0)
            {
                result += " (" + Shortcut + ")";
            }

            return result;
        }
    }
}
