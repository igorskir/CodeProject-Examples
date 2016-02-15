using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using eDirectory.Common.Message;
using Hardcodet.Wpf.TaskbarNotification;

namespace eDirectory.WPF.Services
{
    class BusinessWarningManager
        : IBusinessWarningManager
    {
        private TaskbarIcon WarningNotifierInstance;

        public BusinessWarningManager()
        {
            WarningNotifierInstance = new TaskbarIcon { Icon = new Icon(@"./Resources/Icons/Warning.ico") };
        }

        public void HandleBusinessWarning(IEnumerable<BusinessWarning> warnings)
        {
            var message = string.Join(Environment.NewLine, warnings.Select(w => w.Message));
            WarningNotifierInstance.ShowBalloonTip("eDirectory - Warning",
                                                   message,
                                                   BalloonIcon.Info);
        }
    }
}
