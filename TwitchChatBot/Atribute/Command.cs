﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchChatBot.Atribute
{
    public class Command : Attribute
    {
        string NameCommand = "";

        public Command(string name)
        {
            NameCommand = name;
        }
    }
}