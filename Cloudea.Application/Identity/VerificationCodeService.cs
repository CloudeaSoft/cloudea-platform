using Cloudea.Application.Utils;
using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.Identity.Entities;
using Cloudea.Domain.Identity.Enums;
using Cloudea.Domain.Identity.Repositories;
using MediatR;

namespace Cloudea.Application.Identity
{
    public class VerificationCodeService
    {
        private readonly ISender _sender;
        private readonly IVerificationCodeRepository _verificationCodeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VerificationCodeService(ISender sender, IVerificationCodeRepository verificationCodeRepository, IUnitOfWork unitOfWork)
        {
            _sender = sender;
            _verificationCodeRepository = verificationCodeRepository;
            _unitOfWork = unitOfWork;
        }

        const int EXPIRE_TIME = 5;

        /// <summary>
        /// 检查验证码
        /// </summary>
        /// <param name="email"></param>
        /// <param name="codeType"></param>
        /// <param name="code"></param>
        /// <param name="delete"></param>
        /// <returns></returns>
        public async Task<Result> CheckVerCodeEmail(string email, VerificationCodeType codeType, string code, bool delete = false, CancellationToken cancellationToken = default)
        {
            VerificationCode? entity = await _verificationCodeRepository.GetByEmailAndCodeTypeAsync(email, codeType);
            if (entity is null) {
                return new Error("验证码错误");
            }

            if (entity.VerCode == code) {
                if (entity.VerCodeValidTime >= DateTimeOffset.UtcNow) {
                    if (delete) {
                        _verificationCodeRepository.Delete(entity);
                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }

                    return Result.Success();
                }
                else {
                    return new Error("验证码已过期请重新获取");
                }
            }
            else {
                return new Error("验证码错误");
            }
        }

        /// <summary>
        /// 发送邮箱验证码
        /// </summary>
        /// <returns></returns>
        public async Task<Result> SendVerCodeEmail(string email, VerificationCodeType codeType, CancellationToken cancellationToken = default)
        {
            // 生成验证码 并记录
            string verCode = GenerateVerificationCode();
            VerificationCode? entity = await _verificationCodeRepository.GetByEmailAndCodeTypeAsync(email, codeType);
            // Create new one if not found.
            if (entity is null) {
                entity = VerificationCode.Create(
                    email,
                    verCode,
                    codeType,
                    EXPIRE_TIME);
                if (entity is null) {
                    return new Error("VerificationCode.InvalidParam");
                }
                _verificationCodeRepository.Add(entity);
            }
            // Update
            else {
                // 如果时间还没超过一分钟又要发了  退回
                if (entity.VerCodeValidTime.AddMinutes(-1 * (EXPIRE_TIME - 1)) >= DateTimeOffset.UtcNow) {
                    return new Error("VerificationCode.TooManyRequest", "请稍后再试");
                }

                // 更新
                entity.Update(verCode, codeType);
                entity.SetValidTime(EXPIRE_TIME);
                _verificationCodeRepository.Update(entity);
            }
            // 生成邮件
            var codeEmail = new SendEmailRequest() {
                To = new() { email },
                Subject = "验证码",
                Body = $"[验证码] 验证码:{verCode}"
            };

            // 发送邮件
            var res = await _sender.Send(codeEmail, cancellationToken);
            if (res.IsFailure) {
                return res;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }

        /// <summary>
        /// 随机生成验证码
        /// </summary>
        /// <returns></returns>
        private static string GenerateVerificationCode()
        {
            Random rd = new Random();
            return rd.Next(100000, 999999).ToString();
        }
    }
}
