using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Commands.Photos.Add
{
    public class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommandRequest, Result<Photo>>
    {
        private readonly DataContext _context;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessor _userAccessor;
        public AddPhotoCommandHandler(DataContext context, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _photoAccessor = photoAccessor;
            _context = context;
        }

        public async Task<Result<Photo>> Handle(AddPhotoCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.Getusername());

            if (user == null) return null;

            var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

            var photo = new Photo
            {
                Id = photoUploadResult.PublicId,
                Url = photoUploadResult.Url
            };

            if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

            user.Photos.Add(photo);

            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Result<Photo>.Success(photo);

            return Result<Photo>.Failure("Problem adding Photo");
        }
    }
}