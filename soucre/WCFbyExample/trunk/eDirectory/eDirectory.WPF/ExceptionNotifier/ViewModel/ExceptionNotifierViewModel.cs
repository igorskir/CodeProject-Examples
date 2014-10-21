using System;
using System.Text;
using System.Windows.Forms;
using eDirectory.Common.Message;
using eDirectory.WPF.ExceptionNotifier.View;
using eDirectory.WPF.Util;

namespace eDirectory.WPF.ExceptionNotifier.ViewModel
{
    public class ExceptionNotifierViewModel
    {
        #region Constructors

        public ExceptionNotifierViewModel(Exception e)
            : this(GetExceptionMessage(e), e.StackTrace, BusinessExceptionEnum.Default)
        {

        }

        public ExceptionNotifierViewModel(BusinessExceptionDto e)
            : this(e.Message, e.StackTrace, e.ExceptionType)
        {

        }

        public ExceptionNotifierViewModel(string message, string stackTrace, BusinessExceptionEnum type)
        {
            View = new ExceptionNotifierView();
            ExceptionMsg = message;
            ExceptionCallStack = stackTrace;
            ExceptionType = type;
            View.DataContext = this;
            ImageFilePath = GetImagePath(type);
            TitleMessage = GetTitleMessage(type);
            View.ShowDialog();
        }

        #endregion
        #region Private Fields

        private readonly ExceptionNotifierView View;        

        #endregion
        #region Public Static Helper Methods

        public static string GetExceptionMessage(Exception exception)
        {
            var builder = new StringBuilder();
            var currentException = exception;
            while (currentException != null)
            {
                builder.AppendLine(currentException.Message);
                currentException = currentException.InnerException;
            }
            return builder.ToString();
        }

        #endregion Public Static Helper Methods
        #region Private Helper Methods
        
        private static string GetTitleMessage(BusinessExceptionEnum type)
        {
            switch (type)
            {
                case BusinessExceptionEnum.Default:
                case BusinessExceptionEnum.Operational:
                    return "Exception Message";
                case BusinessExceptionEnum.Validation:
                    return "Warning/Validation Message";
                default:
                    throw new ApplicationException("Invalid exception type was passed, image could not be found");
            }
        }

        private static string GetImagePath(BusinessExceptionEnum type)
        {
            switch (type)
            {
                case BusinessExceptionEnum.Default:
                case BusinessExceptionEnum.Operational:
                    return @"\Resources\Images\Exception.png";
                case BusinessExceptionEnum.Validation:
                    return @"\Resources\Images\Warning.png";
                default:
                    throw new ApplicationException("Invalid exception type was passed, image could not be found");
            }
        }

        private string FullExceptionDescription()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Message:");
            builder.AppendLine(ExceptionMsg);
            builder.AppendLine();
            builder.AppendLine("Stack Trace:");
            builder.AppendLine(ExceptionCallStack);
            return builder.ToString();
        }

        #endregion Private Helper Methods
        #region Public Properties

        public string TitleMessage { get; set; }
        public string ImageFilePath { get; set; }
        public string ExceptionMsg { get; private set; }
        public string ExceptionCallStack { get; private set; }
        public BusinessExceptionEnum ExceptionType { get; private set; }

        #endregion Public Properties
        #region Relay Commands

        private RelayCommand CloseCommandInstance;        
        public RelayCommand CloseCommand
        {
            get
            {
                if (CloseCommandInstance != null) return CloseCommandInstance;
                CloseCommandInstance = new RelayCommand(param => View.Close());
                return CloseCommandInstance;
            }
        }

        private RelayCommand CopyCommandInstance;
        public RelayCommand CopyCommand
        {
            get
            {
                if (CopyCommandInstance != null) return CopyCommandInstance;
                CopyCommandInstance = new RelayCommand(param => Clipboard.SetText(FullExceptionDescription()));
                return CopyCommandInstance;
            }
        }

        #endregion Relay Commands                
    }

}
