﻿using CtrlVAF.Events.Commands;
using CtrlVAF.Events.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrlVAF.Tests.CommandTests
{
    public class AfterCheckInChangesFailureHandler : IEventHandler<AfterCheckInChangesCommand<Configuration>>
    {
        public void Handle(AfterCheckInChangesCommand<Configuration> command)
        {
            throw new NotImplementedException();
        }
    }
}
