using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace eDirectory.Common.Message
{
    public abstract class ValidatorDtoBase
        : DtoBase, IDataErrorInfo 
    {
        private DataErrorInfo<ValidatorDtoBase> DataErrorValidator;

        #region Implementation of IDataErrorInfo

        public string this[string propertyName]
        {
            get { return GetDataErrorInfo()[propertyName]; }
        }

        public string Error
        {
            get { return GetDataErrorInfo().Error; }
        }

        #endregion

        public bool IsValid()
        {
            return string.IsNullOrEmpty(Error);
        }

        private DataErrorInfo<ValidatorDtoBase> GetDataErrorInfo()
        {
            if (DataErrorValidator != null) return DataErrorValidator;
            DataErrorValidator = new DataErrorInfo<ValidatorDtoBase>(this);
            return DataErrorValidator;
        }
    }
}