using System.ComponentModel;

namespace eDirectory.Common.Message
{
    public abstract class ValidatorDtoBase
        : DtoBase, IDataErrorInfo
    {
        private readonly DataErrorInfo<ValidatorDtoBase> DataErrorValidator;

        protected ValidatorDtoBase()
        {
            DataErrorValidator = new DataErrorInfo<ValidatorDtoBase>(this);
        }

        #region Implementation of IDataErrorInfo

        public string this[string propertyName]
        {
            get { return DataErrorValidator[propertyName]; }
        }

        public string Error
        {
            get { return DataErrorValidator.Error; }
        }

        #endregion

        public bool IsValid()
        {
            return string.IsNullOrEmpty(Error);
        }
    }
}