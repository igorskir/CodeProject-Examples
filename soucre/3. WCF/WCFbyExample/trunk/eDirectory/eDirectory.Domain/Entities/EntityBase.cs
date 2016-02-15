using System.ComponentModel.DataAnnotations;
using eDirectory.Common.Message;
using eDirectory.Domain.Repository;

namespace eDirectory.Domain.Entities
{
    /// <remarks>
    /// version 0.2 Chapter: Repository
    /// </remarks>
    public abstract class EntityBase
        :IEntity
    {
        [Key]
        public virtual long Id { get; protected set; }

        protected static void ValidateOperation(IRepositoryLocator locator, ValidatorDtoBase operation)
        {
            if (operation.IsValid()) return;
            throw new BusinessException(BusinessExceptionEnum.Validation, operation.Error);
        }
    }
}
