using Cloudea.Application.Abstractions;
using Cloudea.Domain.Common.Repositories;
using Cloudea.Domain.Common.Shared;
using Cloudea.Domain.System.Entities;
using Cloudea.Domain.System.Repositories;
using Cloudea.Domain.System.ValueObjects;

namespace Cloudea.Application.System;

public class AnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;
    private readonly IAnnouncementTranslationRepository _announcementTranslationRepository;
    private readonly ICurrentUser _currentUser;

    private readonly IUnitOfWork _unitOfWork;

    public AnnouncementService(IAnnouncementRepository announcementRepository, ICurrentUser currentUser, IUnitOfWork unitOfWork, IAnnouncementTranslationRepository announcementTranslationRepository)
    {
        _announcementRepository = announcementRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
        _announcementTranslationRepository = announcementTranslationRepository;
    }

    public async Task<Result<Announcement>> CreateAnnouncement(string title, string content, CancellationToken cancellationToken = default)
    {
        var userId = await _currentUser.GetUserIdAsync(cancellationToken);

        var announcement = Announcement.Create(title, content, userId);
        if (announcement is null)
        {
            return new Error("Annoucement.InvalidParam");
        }

        _announcementRepository.Add(announcement);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return announcement;
    }

    public async Task<Result<Announcement>> GetAnnouncement(Guid announcementId, CancellationToken cancellationToken = default)
    {
        var announcement = await _announcementRepository.GetByIdAsync(announcementId, cancellationToken);
        if (announcement is null)
        {
            return new Error("Announcement.NotFound");
        }

        return announcement;
    }

    public async Task<Result<AnnouncementTranslation>> CreateAnnouncementTranslation(
        Guid announcementId,
        string language,
        string region,
        string title,
        string content,
        CancellationToken cancellationToken = default)
    {
        var userId = await _currentUser.GetUserIdAsync(cancellationToken);
        var announcement = await _announcementRepository.GetByIdAsync(announcementId, cancellationToken);
        if (announcement is null)
        {
            return new Error("Announcement.NotFound");
        }

        var codeRes = LanguageCode.Create(language, region);
        if (codeRes.IsFailure)
        {
            return codeRes.Error;
        }

        var code = codeRes.Data;
        if (announcement.Translations.Where(x => x.LanguageCode.ToString() == code.ToString()).FirstOrDefault() is not null)
        {
            return new Error("Announcement.Translation.Exists");
        }

        var translation = announcement.AddTranslation(code, title, content, userId);
        if (translation is null)
        {
            return new Error("Annoucement.Translation.InvalidParam");
        }

        _announcementTranslationRepository.Add(translation);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return translation;
    }
}
