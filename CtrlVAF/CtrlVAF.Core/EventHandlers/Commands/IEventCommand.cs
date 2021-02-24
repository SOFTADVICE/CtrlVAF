﻿using CtrlVAF.Models;

using MFiles.VAF.Common;
using MFilesAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlVAF.Events.Commands
{
    public interface IEventCommand<T>: ICtrlVAFCommand
    {
        EventHandlerEnvironment Env { get; set; }
        T Configuration { get; set; }
    }

}
