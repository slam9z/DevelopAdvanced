using System;

using System.Linq;
using WebShopCommon.Dto;
using WebShopCommon.Enums;
using WebShopCommon.Models;
using RebuCommon.Utils;

using System.Collections.Generic;

namespace WebShopCommon.Business
{
    public class AccountBusiness : BusinessBase
    {
        public Account GetAccountByName(string userName)
        {
            return (from a in _unitOfWork.AccountRepository
                    where a.UserName == userName
                    select a
                ).FirstOrDefault();
        }

        public Account GetAccountByUserId(Guid userId)
        {
            return _unitOfWork.AccountRepository.Where(a => a.UserId == userId).FirstOrDefault();
        }

        public CommonOperationStatus CreateAccount(Account account, bool isSaveChange = true)
        {
            if (account.CreatedTime == DateTime.MinValue)
            {
                account.CreatedTime = DateTime.Now;
            }

            if (account.UpdatedTime == DateTime.MinValue)
            {
                account.UpdatedTime = DateTime.Now;
            }
            account.Id = Guid.NewGuid();
            account.UserId = Guid.NewGuid();

            account.Password = PasswordUtility.EncryptPassword(account.Password);

            _unitOfWork.AccountRepository.Insert(account);
            if (isSaveChange)
            {
                _unitOfWork.Save();
            }

            return CommonOperationStatus.Success;
        }

        public CommonOperationStatus DeleteAccount(Guid Id, bool isSaveChange = true)
        {
            _unitOfWork.AccountRepository.Delete(Id);
            if (isSaveChange)
            {
                _unitOfWork.Save();
            }

            return CommonOperationStatus.Success;
        }


        public AccountValidateStatus CheckAccount(AccountDto accountDto)
        {
            var account = this.GetAccountByName(accountDto.UserName);
            if (account == null)
            {
                return AccountValidateStatus.AccountNotExist;
            }

            var password = PasswordUtility.EncryptPassword(accountDto.Password);
            if (password != account.Password)
            {
                return AccountValidateStatus.UserNamePasswordNotMatch;
            }

            accountDto.UserId = account.UserId;



            return AccountValidateStatus.Success;
        }


    }
}